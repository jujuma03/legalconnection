using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.SocialNetwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/redes-sociales")]
    [Area("Admin")]
    public class SocialNetworkController : Controller
    {
        private readonly IDataTableService _dataTablesService;
        private readonly ISocialNetworkService _socialNetworkService;
        public SocialNetworkController(IDataTableService dataTablesService,
            ISocialNetworkService socialNetworkService)
        {
            _dataTablesService=dataTablesService;
            _socialNetworkService=socialNetworkService;
        }
        public async Task<IActionResult> Index()
        {
            var viewmodel = new SocialNetworkViewModel();

            viewmodel.ListStatus = new SelectList(ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES, "Key", "Value");
            viewmodel.ListTypes = new SelectList(ConstantHelpers.ENTITIES.SOCIAL_NETWORK.TYPES.VALUES, "Key", "Value");
            return View(viewmodel);
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get(byte type, byte? status)
        {
            var sentParameters = _dataTablesService.GetSentParameters();
            var result = await _socialNetworkService.GetAllDatatable(sentParameters, type, status.HasValue ? status.Value : (byte)0);
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var social = await _socialNetworkService.Get(id);
            var model = new SocialNetworkViewModel
            {
                Id=social.Id,
                Status = social.Status==ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE,
                Type = social.Type,
                UrlDirection= social.UrlDirection,
            };

            return Ok(model);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Create(SocialNetworkViewModel model)
        {
            var sectionItem = new SocialNetwork
            {
                Status = ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE,
                Type = model.Type,
                UrlDirection = model.UrlDirection,
            };

            await _socialNetworkService.Insert(sectionItem);

            return Ok();
        }

        [HttpPost("editar")]
        public async Task<IActionResult> Update(SocialNetworkViewModel model)
        {
            var social = await _socialNetworkService.Get(model.Id);

            social.Id=model.Id;
            social.Status = model.Status
                ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
                : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            social.Type = model.Type;
            social.UrlDirection = model.UrlDirection;

            await _socialNetworkService.Update(social);

            return Ok();
        }

        [HttpPost("{id}/cambiar-estado/post")]
        public async Task<IActionResult> ChangeStatus(Guid id, bool status)
        {
            var social = await _socialNetworkService.Get(id);

            social.Status = status
                ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
                : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            await _socialNetworkService.Update(social);
            return Ok();
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var social = await _socialNetworkService.Get(id);
            await _socialNetworkService.Delete(social);

            return Ok();
        }
    }
}