using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.SERVICE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Report.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Area("Report")]
    [Route("reportes/casos-legales")]
    public class LegalCaseController : Controller
    {
        private readonly ILegalCaseService _legalCaseService;
        private readonly IDataTableService _dataTableService;

        public LegalCaseController(
            ILegalCaseService legalCaseService,
            IDataTableService dataTableService
            )
        {
            _legalCaseService = legalCaseService;
            _dataTableService = dataTableService;
        }

        public IActionResult Finished()
            => View();

        public async Task<IActionResult> GetLegalCaseFinished(string searchValue)
        {
            var parameters = _dataTableService.GetSentParameters();
            var result = await _legalCaseService.GetLegalCasesDatatable(parameters, ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED);
            return Ok(result);
        }            
    }
}
