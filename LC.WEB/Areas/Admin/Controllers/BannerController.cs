using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.Banner;
using LC.WEB.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/banners-inicio")]
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly IDataTableService _dataTablesService;

        public BannerController(
            IBannerService bannerService,
            ICloudStorageService cloudStorageService,
            IDataTableService dataTablesService)
        {
            _bannerService = bannerService;
            _cloudStorageService = cloudStorageService;
            _dataTablesService = dataTablesService;
        }


        public async Task<IActionResult> Index()
        {
            var sequences = await _bannerService.GetAvailableOrdersAndListSequenceOrder();
            
            var viewmodel = new BannerViewModel();

            viewmodel.ListStatus = new SelectList(ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES, "Key", "Value");
            viewmodel.ListSequenceOrder =new SelectList(sequences, "Key", "Value");
            return View(viewmodel);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetBanners(string headline, byte? status)
        {
            var sentParameters = _dataTablesService.GetSentParameters();
            var result = await _bannerService.GetAllBannerDatatable(sentParameters, headline, status.HasValue ? status.Value : (byte)0);
            return Ok(result);
        }

        [HttpGet("registrar")]
        public async Task<IActionResult> Create()
        {
            var banner = await _bannerService.GetAvailableOrdersAndListSequenceOrder();
            var model = new BannerViewModel
            {
                ListSequenceOrder = new SelectList(banner, "Key", "Value")
            };
            return View(model);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Create(BannerViewModel model)
        {
            var status = 0;
            var statusDirection = 0;
            var routeType = 0;

            var bannerActive = await _bannerService.GetAllBannersActive();

            if (bannerActive.Count() >= 5)
            {
                status = 2;
            }
            else
            {
                if (model.Status == "false")
                    status = 2;
                else
                    status = 1;
            }

            if (model.RouteType == "false")
                routeType = 2;
            else
                routeType = 1;

            if (model.StatusDirection == "false")
                statusDirection = 2;
            else
                statusDirection = 1;

            var banner = new Banner
            {
                PublicationDate = DateTime.UtcNow,
                Headline = model.Headline,
                Status = Convert.ToByte(status),
                Description = model.Description,
                SequenceOrder = Convert.ToByte(model.SequenceOrder),
                UrlDirection = model.UrlDirection,
                StatusDirection = Convert.ToByte(statusDirection),
                NameDirection = model.NameDirection,
                RouteType = Convert.ToByte(routeType)
            };

            if (status == 2)
                banner.SequenceOrder = 0;


            if (bannerActive.Count() >= 5 && status == 1 && model.SequenceOrderId == 0)
                return BadRequest("No se puede activar más banners, son 5 como máximo");

            if (model.urlCropImg != null)
            {
                var imgArray1 = model.urlCropImg.Split(";");
                var imgArray2 = imgArray1[1].Split(",");

                var newImage = Convert.FromBase64String(imgArray2[1]);

                using (var stream = new MemoryStream(newImage))
                {
                    if (!string.IsNullOrEmpty(banner.UrlImage))
                        await _cloudStorageService.TryDelete(banner.UrlImage.Split('/').Last(), ConstantHelpers.CLOUD_CONTAINERS.PORTAL);

                    var extension = Path.GetExtension(model.Image.FileName);
                    banner.UrlImage = await _cloudStorageService.UploadFile(stream, ConstantHelpers.CLOUD_CONTAINERS.PORTAL, $"{Guid.NewGuid()}-{model.SequenceOrder}",extension);
                }
            }

            await _bannerService.Insert(banner);

            return Ok();
        }

        [HttpGet("editar/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var x = await _bannerService.Get(id);
            var banners = await _bannerService.GetAllBannersActive();
            var availableOrders = ConstantHelpers.SEQUENCE_ORDER.VALUES
              .Where(x => !banners.Where(b => b.Id != id).Any(b => b.SequenceOrder == x.Key));

            var result = new BannerViewModel
            {
                Id = x.Id,
                Headline = x.Headline,
                Description = x.Description,
                UrlImage = x.UrlImage,
                Status =x.Status==0 ? ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES[ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE] : ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES[x.Status],
                RouteType =x.RouteType==0? ConstantHelpers.ENTITIES.BANNER.TYPE.VALUES[ConstantHelpers.ENTITIES.BANNER.TYPE.INTERNAL]:  ConstantHelpers.ENTITIES.BANNER.TYPE.VALUES[x.RouteType],
                UrlDirection = x.UrlDirection,
                StatusDirection = ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES[x.StatusDirection],
                NameDirection = x.NameDirection,
                SequenceOrder = x.SequenceOrder.HasValue ? ConstantHelpers.SEQUENCE_ORDER.VALUES[x.SequenceOrder.Value] : "",
                SequenceOrderId = x.SequenceOrder.HasValue ? x.SequenceOrder.Value : 0,
                StatusDirectionId = x.StatusDirection == 1 ? true : false,
                StatusId = x.Status == 1 ? true : false,
                RouteTypeId = x.RouteType == 1 ? true : false,
                ListSequenceOrder = new SelectList(availableOrders, "Key", "Value")
            };
            return View(result);
        }

        [HttpPost("editar/post")]
        public async Task<IActionResult> Edit(Guid id, BannerViewModel model)
        {
            var status = 0;
            var statusDirection = 0;
            var routeType = 0;

            if (model.StatusId == false)
                status = 2;
            else
                status = 1;

            if (model.StatusDirectionId == false)
                statusDirection = 2;
            else
                statusDirection = 1;

            if (model.RouteTypeId == false)
                routeType = 2;
            else
                routeType = 1;

            var banner = await _bannerService.Get(id);

            banner.Status = Convert.ToByte(status);
            banner.Headline = model.Headline;
            banner.UrlDirection = model.UrlDirection;

            if (model.StatusId == false)
                banner.SequenceOrder = 0;
            else
                banner.SequenceOrder = Convert.ToByte(model.SequenceOrderId);

            banner.Description = model.Description;
            banner.NameDirection = model.NameDirection;
            banner.StatusDirection = Convert.ToByte(statusDirection);
            banner.RouteType = Convert.ToByte(routeType);

            var bannerActive = await _bannerService.GetAllBannersActive();

            if (bannerActive.Count() >= 5 && status == 1 && model.SequenceOrderId == 0)
                return BadRequest("No se puede activar más banners, son 5 como máximo");

            if (model.urlCropImg != null)
            {
                var imgArray1 = model.urlCropImg.Split(";");
                var imgArray2 = imgArray1[1].Split(",");

                var newImage = Convert.FromBase64String(imgArray2[1]);

                using (var stream = new MemoryStream(newImage))
                {
                    if (!string.IsNullOrEmpty(banner.UrlImage))
                        await _cloudStorageService.TryDelete(banner.UrlImage.Split('/').Last(), "banner");
                    var extension = Path.GetExtension(model.Image.FileName);
                    banner.UrlImage = await _cloudStorageService.UploadFile(stream, ConstantHelpers.CLOUD_CONTAINERS.PORTAL, $"{DateTime.UtcNow.Ticks}-{model.SequenceOrder}", extension);
                }
            }

            await _bannerService.Update(banner);

            return Ok();
        }

        [HttpPost("{id}/cambiar-estado/post")]
        public async Task<IActionResult> ChangeStatus(Guid id, bool status)
        {
            var bannerActive = await _bannerService.GetAllBannersActive();

            if (bannerActive.Count() >= 5 && status == true)
                return BadRequest("No se puede activar más banners, son 5 como máximo");

            var banner = await _bannerService.Get(id);
            if (status == false)
            {
                banner.SequenceOrder = 0;
            }
            var sequenceOrder = await _bannerService.GetAvailableOrdersAndListSequenceOrder();
            if (status && banner.SequenceOrder.Value == 0)
            {
                banner.SequenceOrder=Convert.ToByte(sequenceOrder.FirstOrDefault().Key);
            }
           
            banner.Status = status
                ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
                : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            await _bannerService.Update(banner);
            return Ok();
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var banner = await _bannerService.Get(id);

            if (!string.IsNullOrEmpty(banner.UrlImage))
                await _cloudStorageService.TryDelete(banner.UrlImage.Split('/').Last(), ConstantHelpers.CLOUD_CONTAINERS.PORTAL);

            await _bannerService.DeleteBanner(banner);

            return Ok();
        }

        [HttpPost("editar/update")]
        public async Task<ActionResult> SaveDropzone(IFormFile file)
        {
            var uploads = Path.Combine(@"D:\Temporales", "uploads");
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            ViewBag.File = file;
            return Json(file);
        }

        [HttpGet("seccionOrden")]
        public async Task<ActionResult> SequenceSelect2()
        {
            var banner = await _bannerService.GetAvailableOrdersAndListSequenceOrder();
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
            var banner = await _bannerService.Get(Guid.Parse(id));
            var sequenceOrder = ConstantHelpers.SEQUENCE_ORDER.VALUES.FirstOrDefault(x => x.Key == sequenceOrderId);
            if (sequenceOrderId == 0)
            {
                banner.SequenceOrder = 0;
                banner.Status = 2;
            }
            else
            {
                banner.SequenceOrder = Convert.ToByte(sequenceOrder.Key);
            }

            await _bannerService.Update(banner);
            return Ok();
        }
    }
}