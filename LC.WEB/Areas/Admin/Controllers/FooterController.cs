using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Implementations;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.Shortcut;
using LC.WEB.Areas.Admin.Models.SocialNetwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/footer")]
    [Area("Admin")]
    public class FooterController : Controller
    {
        private readonly IDataTableService _dataTablesService;
        private readonly IShortcutService _shortcutService;
        public FooterController(IDataTableService dataTablesService,
            IShortcutService shotcutService)
        {
            _dataTablesService=dataTablesService;
            _shortcutService=shotcutService;
        }
        public async Task<IActionResult> Index()
        {
            var viewmodel = new ShortcutViewModel();

            viewmodel.ListStatus = new SelectList(ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES, "Key", "Value");
            viewmodel.ListTypes = new SelectList(ConstantHelpers.ENTITIES.SHORTCUT.TYPES.VALUES, "Key", "Value");
            return View(viewmodel);
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get(byte type, byte? status)
        {
            var sentParameters = _dataTablesService.GetSentParameters();
            var result = await _shortcutService.GetAllDatatable(sentParameters, type, status.HasValue ? status.Value : (byte)0);
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var social = await _shortcutService.Get(id);
            var model = new ShortcutViewModel
            {
                Id=social.Id,
                Title=social.Title,
                Status = social.Status==ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE,
                Type = social.Type,
                UrlDirection= social.UrlDirection,
            };

            return Ok(model);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Create(ShortcutViewModel model)
        {
            var sectionItem = new Shortcut
            {
                Title = model.Title,
                Status = ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE,
                Type = model.Type,
                UrlDirection = model.UrlDirection,
            };

            await _shortcutService.Insert(sectionItem);

            return Ok();
        }

        [HttpPost("editar")]
        public async Task<IActionResult> Update(ShortcutViewModel model)
        {
            var social = await _shortcutService.Get(model.Id);

            social.Id=model.Id;
            social.Status = model.Status
                ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
                : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            social.Title = model.Title;
            social.Type = model.Type;
            social.UrlDirection = model.UrlDirection;

            await _shortcutService.Update(social);

            return Ok();
        }

        [HttpPost("{id}/cambiar-estado/post")]
        public async Task<IActionResult> ChangeStatus(Guid id, bool status)
        {
            var social = await _shortcutService.Get(id);

            social.Status = status
                ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
                : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            await _shortcutService.Update(social);
            return Ok();
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var social = await _shortcutService.Get(id);
            await _shortcutService.Delete(social);

            return Ok();
        }
    }
}