using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.HowItWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AKDEMIC.SISCO.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/como-funciona")]
    [Area("Admin")]
    public class HowItWorksController : Controller
    {
        private readonly IDataTableService _dataTablesService;

        private readonly ICloudStorageService _cloudStorageService;
        private readonly IHowItWorksStepService _howItWorksStepService;

        public HowItWorksController(IDataTableService dataTablesService,
            ICloudStorageService cloudStorageService,
            IHowItWorksStepService howItWorksStepService)
        {
            _cloudStorageService=cloudStorageService;
            _howItWorksStepService = howItWorksStepService;
            _dataTablesService = dataTablesService;
        }

        public async Task<IActionResult> Index()
        {
            var viewmodel = new HowItWorksViewModel();

            viewmodel.ListStatus = new SelectList(ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES, "Key", "Value");
            return View(viewmodel);
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get(string headline, byte? status)
        {
            var sentParameters = _dataTablesService.GetSentParameters();
            var result = await _howItWorksStepService.GetAllDatatable(sentParameters, headline, status.HasValue ? status.Value : (byte)0);
            return Ok(result);
        }
        [HttpGet("get-ordenes/{type}")]
        public async Task<IActionResult> GetSequenceOrder(byte type)
        {
            var sequences = await _howItWorksStepService.GetAvailableOrdersAndListSequenceOrder(type);
            var test = new SelectList(sequences, "Key", "Value").Select(x => new
            {
                id = x.Value,
                text = x.Text,
                selected = x.Selected
            })
                .ToList();
            return Ok(test);
        }
        [HttpGet("registrar")]
        public async Task<IActionResult> Create()
        {
            ViewBag.ListTypes = new SelectList(ConstantHelpers.ENTITIES.HOW_IT_WORKS.TYPE.VALUES, "Key", "Value");
            return View();
        }
        [HttpGet("editar/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var banners = await _howItWorksStepService.GetAllActive();
            ViewBag.ListTypes = new SelectList(ConstantHelpers.ENTITIES.HOW_IT_WORKS.TYPE.VALUES, "Key", "Value");

            var viewmodel = new CreateHowItWorksViewModel();
            var entity = await _howItWorksStepService.Get(id);
            var sequences = await _howItWorksStepService.GetAvailableOrdersAndListSequenceOrder(entity.Type);

            var availableOrders = ConstantHelpers.SEQUENCE_ORDER.VALUES
              .Where(x => !banners.Where(b => b.Id != id).Any(b => b.Order == x.Key));
            ViewBag.ListOrder = new SelectList(availableOrders, "Key", "Value");

            viewmodel.Title =entity.Title;
            viewmodel.Description =entity.Content;
            viewmodel.Order =entity.Order;
            viewmodel.Type =entity.Type;
            viewmodel.Summary =entity.Summary;
            viewmodel.UrlImage=entity.UrlImage;
            viewmodel.Status=entity.Status == ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE;

            return View(viewmodel);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Create(CreateHowItWorksViewModel model)
        {
            var mission = new HowItWorksStep()
            {
                Title = model.Title,
                Content = model.Description,
                Order=model.Order,
                Type=model.Type,
                Summary=model.Summary,
                Status = ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
            };
            if (!string.IsNullOrEmpty(model.UrlCropImg))
            {
                var imgArray1 = model.UrlCropImg.Split(";");
                var imgArray2 = imgArray1[1].Split(",");

                var newImage = Convert.FromBase64String(imgArray2[1]);
                using (var stream = new MemoryStream(newImage))
                {
                    var extension = Path.GetExtension(model.Image.FileName);
                    mission.UrlImage = await _cloudStorageService.UploadFile(stream, ConstantHelpers.CLOUD_CONTAINERS.PORTAL, $"{Guid.NewGuid()}", extension);
                }

                await _howItWorksStepService.Insert(mission);
            }
            return Ok();
        }

        [HttpPost("editar/{id}")]
        public async Task<IActionResult> Update(Guid id, CreateHowItWorksViewModel model)
        {
            var entity = await _howItWorksStepService.Get(model.Id);
            entity.Title = model.Title;
            entity.Content = model.Description;
            entity.Order=model.Order;
            entity.Type=model.Type;
            entity.Summary=model.Summary;
            entity.Status=model.Status ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;

            if (!string.IsNullOrEmpty(model.UrlCropImg))
            {
                if (!model.Image.ContentType.Contains("image"))
                    return BadRequest("El archivo adjunto no es de formato ");

                var imgArray1 = model.UrlCropImg.Split(";");
                var imgArray2 = imgArray1[1].Split(",");

                var newImage = Convert.FromBase64String(imgArray2[1]);
                using (var stream = new MemoryStream(newImage))
                {
                    if (!string.IsNullOrEmpty(entity.UrlImage))
                        await _cloudStorageService.TryDelete(entity.UrlImage.Split('/').Last(), ConstantHelpers.CLOUD_CONTAINERS.MISSION_VISION);

                    var extension = Path.GetExtension(model.Image.FileName);
                    entity.UrlImage = await _cloudStorageService.UploadFile(stream, ConstantHelpers.CLOUD_CONTAINERS.MISSION_VISION, $"{Guid.NewGuid()}", extension);
                }
            }

            await _howItWorksStepService.Update(entity);
            return Ok();
        }
        [HttpPost("{id}/cambiar-estado/post")]
        public async Task<IActionResult> ChangeStatus(Guid id, bool status)
        {
            var bannerActive = await _howItWorksStepService.GetAllActive();

            //if (bannerActive.Count() >= 5 && status == true)
            //    return BadRequest("No se puede activar más banners, son 5 como máximo");

            var banner = await _howItWorksStepService.Get(id);
            if (status == false)
            {
                banner.Order = 0;
            }
            var sequenceOrder = await _howItWorksStepService.GetAvailableOrdersAndListSequenceOrder(banner.Type);
            if (status && banner.Order == 0)
            {
                banner.Order=Convert.ToByte(sequenceOrder.FirstOrDefault().Key);
            }

            banner.Status = status
                ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
                : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            await _howItWorksStepService.Update(banner);
            return Ok();
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var banner = await _howItWorksStepService.Get(id);

            if (!string.IsNullOrEmpty(banner.UrlImage))
                await _cloudStorageService.TryDelete(banner.UrlImage.Split('/').Last(), ConstantHelpers.CLOUD_CONTAINERS.PORTAL);

            await _howItWorksStepService.Delete(banner);

            return Ok();
        }
        [HttpGet("seccionOrden/{type}")]
        public async Task<ActionResult> SequenceSelect2(byte type)
        {
            var banner = await _howItWorksStepService.GetAvailableOrdersAndListSequenceOrder(type);
            var result = banner.Select(x => new
            {
                id = x.Key,
                text = x.Value,
            }).ToList();
            return Ok(new { items = result });
        }

        [HttpPost("{id}/cambiar-orden/{sequenceOrderId}/post")]
        public async Task<ActionResult> ChangeSequenceOrder(string id, int sequenceOrderId)
        {
            var banner = await _howItWorksStepService.Get(Guid.Parse(id));
            var sequenceOrder = ConstantHelpers.SEQUENCE_ORDER.VALUES.FirstOrDefault(x => x.Key == sequenceOrderId);
            if (sequenceOrderId == 0)
            {
                banner.Order = 0;
                banner.Status = 2;
            }
            else
            {
                banner.Order = Convert.ToByte(sequenceOrder.Key);
            }

            await _howItWorksStepService.Update(banner);
            return Ok();
        }
    }
}