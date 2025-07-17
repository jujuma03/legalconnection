using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.SectionItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/elementos-seccion")]
    [Area("Admin")]
    public class SectionItemsController : Controller
    {
        private readonly ISectionItemService _sectionItemService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly IDataTableService _dataTablesService;

        public SectionItemsController(
            ISectionItemService sectionItemService,
            ICloudStorageService cloudStorageService,
            IDataTableService dataTablesService)
        {
            _sectionItemService = sectionItemService;
            _cloudStorageService = cloudStorageService;
            _dataTablesService = dataTablesService;
        }
        public IActionResult Index()
        {
            var viewmodel = new SectionItemsViewModel();

            viewmodel.ListStatus = new SelectList(ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES, "Key", "Value");
            viewmodel.ListSection= new SelectList(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.VALUES, "Key", "Value");
            return View(viewmodel);
        }
        [HttpGet("registrar")]
        public IActionResult Create()
        {
            var viewmodel = new SectionItemsViewModel();

            viewmodel.ListStatus = new SelectList(ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES, "Key", "Value");
            viewmodel.ListSection= new SelectList(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.VALUES, "Key", "Value");
            return View(viewmodel);
        }
        [HttpGet("editar/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var sectionItem = await _sectionItemService.Get(id);

            var viewmodel = new SectionItemsViewModel();
            viewmodel.Headline = sectionItem.HeadLine;
            viewmodel.Status=sectionItem.Status == ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE;
            viewmodel.Description = sectionItem.Description;
            viewmodel.Type = sectionItem.Type;
            viewmodel.UrlImage = sectionItem.UrlImage;

            viewmodel.ListStatus = new SelectList(ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES, "Key", "Value");
            viewmodel.ListSection= new SelectList(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.VALUES, "Key", "Value");
            return View(viewmodel);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetSectionItems(string headline, byte? status)
        {
            var sentParameters = _dataTablesService.GetSentParameters();
            var result = await _sectionItemService.GetAllDatatable(sentParameters, headline, status.HasValue ? status.Value : (byte)0);
            return Ok(result);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Create(SectionItemsViewModel model)
        {
            if (model.Type == ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.LAWYER_BANNER)
            {
                var ban = await _sectionItemService.GetActiveBySection(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.LAWYER_BANNER);
                if (ban.Count()>0)
                {
                    return BadRequest("No se puede agregar otro banner");
                }
            }

            var sectionItem = new SectionItem
            {
                HeadLine = model.Headline,
                Status = ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE,
                Description = model.Description,
                Type = model.Type
            };
            
            if (!string.IsNullOrEmpty(model.UrlCropImg))
            {
                var imgArray1 = model.UrlCropImg.Split(";");
                var imgArray2 = imgArray1[1].Split(",");

                var newImage = Convert.FromBase64String(imgArray2[1]);
                using (var stream = new MemoryStream(newImage))
                {
                    var extension = Path.GetExtension(model.Image.FileName);
                    sectionItem.UrlImage = await _cloudStorageService.UploadFile(
                        stream,
                        ConstantHelpers.CLOUD_CONTAINERS.PORTAL,
                        $"{Guid.NewGuid()}",
                        extension
                        );
                }
            }

            await _sectionItemService.Insert(sectionItem);

            return Ok();
        }
        [HttpPost("actualizar")]
        public async Task<IActionResult> Update(SectionItemsViewModel model)
        {
            var sectionItem = await _sectionItemService.Get(model.Id);

            sectionItem.HeadLine = model.Headline;
            sectionItem.Status=model.Status ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            sectionItem.Description = model.Description;
            sectionItem.Type = model.Type;

            if (!string.IsNullOrEmpty(model.UrlCropImg))
            {
                var imgArray1 = model.UrlCropImg.Split(";");
                var imgArray2 = imgArray1[1].Split(",");

                var newImage = Convert.FromBase64String(imgArray2[1]);
                using (var stream = new MemoryStream(newImage))
                {
                    if (!string.IsNullOrEmpty(sectionItem.UrlImage))
                        await _cloudStorageService.TryDelete(sectionItem.UrlImage.Split('/').Last(), ConstantHelpers.CLOUD_CONTAINERS.PORTAL);

                    var extension = Path.GetExtension(model.Image.FileName);
                    sectionItem.UrlImage = await _cloudStorageService.UploadFile(
                        stream,
                        ConstantHelpers.CLOUD_CONTAINERS.PORTAL,
                        $"{Guid.NewGuid()}",
                        extension
                        );
                }
            }

            await _sectionItemService.Update(sectionItem);

            return Ok();
        }

        [HttpPost("{id}/cambiar-estado/post")]
        public async Task<IActionResult> ChangeStatus(Guid id, bool status)
        {
            var sectionitem = await _sectionItemService.Get(id);

            sectionitem.Status = status
                ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
                : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            await _sectionItemService.Update(sectionitem);
            return Ok();
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sectionItem = await _sectionItemService.Get(id);

            if (!string.IsNullOrEmpty(sectionItem.UrlImage))
                await _cloudStorageService.TryDelete(sectionItem.UrlImage.Split('/').Last(), ConstantHelpers.CLOUD_CONTAINERS.PORTAL);

            await _sectionItemService.Delete(sectionItem);

            return Ok();
        }
    }
}