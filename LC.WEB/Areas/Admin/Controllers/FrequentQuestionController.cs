using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.FrequentQuestion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/preguntas-frecuentes")]
    [Area("Admin")]
    public class FrequentQuestionController : Controller
    {
        private readonly IDataTableService _dataTablesService;
        private readonly IFrequentQuestionService _frequentQuestionService;
        public FrequentQuestionController(IDataTableService dataTablesService,
            IFrequentQuestionService frequentQuestionService)
        {
            _dataTablesService=dataTablesService;
            _frequentQuestionService=frequentQuestionService;
        }
        public async Task<IActionResult> Index()
        {
            var viewmodel = new FrequentQuestionViewModel();

            viewmodel.ListStatus = new SelectList(ConstantHelpers.ENTITIES.BANNER.STATUS.VALUES, "Key", "Value");
            viewmodel.ListTypes = new SelectList(ConstantHelpers.ENTITIES.FREQUENT_QUESTION.TYPES.VALUES, "Key", "Value");
            viewmodel.ListIcons = new SelectList(ConstantHelpers.ENTITIES.FREQUENT_QUESTION.ICONS.VALUES, "Key", "Value");
            return View(viewmodel);
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get(string headline, byte? status)
        {
            var sentParameters = _dataTablesService.GetSentParameters();
            var result = await _frequentQuestionService.GetAllDatatable(sentParameters, headline, status.HasValue ? status.Value : (byte)0);
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var frequent = await _frequentQuestionService.Get(id);
            var model = new FrequentQuestionViewModel
            {
                Id=frequent.Id,
                Description=frequent.Description,
                Headline=frequent.Title,
                Status = frequent.Status==ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE,
                Type = frequent.Type,
                Icon= frequent.Icon,
            };

            return Ok(model);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Create(FrequentQuestionViewModel model)
        {
            var sectionItem = new FrequentQuestion
            {
                Title = model.Headline,
                Status = ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE,
                Description = model.Description,
                Type = model.Type,
                Icon = model.Icon
            };

            await _frequentQuestionService.Insert(sectionItem);

            return Ok();
        }

        [HttpPost("editar")]
        public async Task<IActionResult> Update(FrequentQuestionViewModel model)
        {
            var frequent = await _frequentQuestionService.Get(model.Id);

            frequent.Id=model.Id;
            frequent.Title = model.Headline;
            frequent.Status = model.Status
                ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
                : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            frequent.Description = model.Description;
            frequent.Type = model.Type;
            frequent.Icon = model.Icon;

            await _frequentQuestionService.Update(frequent);

            return Ok();
        }

        [HttpPost("{id}/cambiar-estado/post")]
        public async Task<IActionResult> ChangeStatus(Guid id, bool status)
        {
            var frequent = await _frequentQuestionService.Get(id);

            frequent.Status = status
                ? ConstantHelpers.ENTITIES.BANNER.STATUS.ACTIVE
                : ConstantHelpers.ENTITIES.BANNER.STATUS.HIDDEN;
            await _frequentQuestionService.Update(frequent);
            return Ok();
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var frequent = await _frequentQuestionService.Get(id);
            await _frequentQuestionService.Delete(frequent);

            return Ok();
        }
    }
}