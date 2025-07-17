using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.LegalCaseQuestion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN + "," + ConstantHelpers.ROLES.ADVISER)]
    [Area("Admin")]
    [Route("admin/preguntas-casos")]
    public class LegalCaseQuestionController : Controller
    {
        private readonly ILegalCaseQuestionService _legalCaseQuestionService;
        private readonly IDataTableService _dataTableService;

        public LegalCaseQuestionController(
            ILegalCaseQuestionService legalCaseQuestionService,
            IDataTableService dataTableService
            )
        {
            _legalCaseQuestionService = legalCaseQuestionService;
            _dataTableService = dataTableService;
        }

        public IActionResult Index()
            => View();

        [HttpGet("get-preguntas")]
        public async Task <IActionResult> GetLegalCaseQuestions(string searchValue)
        {
            var parameters = _dataTableService.GetSentParameters();
            var result = await _legalCaseQuestionService.GetLegalcaseQuestionsDatatable(parameters, searchValue);
            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _legalCaseQuestionService.Get(id));

        [HttpPost("agregar")]
        public async Task<IActionResult> Add(LegalCaseQuestionViewModel model)
        {
            var entity = new LegalCaseQuestion
            {
                Description = model.Description,
            };

            if (await _legalCaseQuestionService.AnyByDescription(model.Description))
                return BadRequest($"Ya existe una pregunta con la descripción : {model.Description}");

            await _legalCaseQuestionService.Insert(entity);
            return Ok();
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> Update(LegalCaseQuestionViewModel model)
        {
            if (await _legalCaseQuestionService.AnyByDescription(model.Description, model.Id))
                return BadRequest($"Ya existe una pregunta con la descripción : {model.Description}");

            var entity = await _legalCaseQuestionService.Get(model.Id.Value);

            entity.Description = model.Description;
            await _legalCaseQuestionService.Update(entity);
            return Ok();
        }

        [HttpPost("eliminar")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _legalCaseQuestionService.Get(id);
            if (await _legalCaseQuestionService.HasRespones(id))
                return BadRequest("La pregunta tiene respuestas relacionadas.");

            await _legalCaseQuestionService.Delete(entity);
            return Ok();
        }
    }
}
