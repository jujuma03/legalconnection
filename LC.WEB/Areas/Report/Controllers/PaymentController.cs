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
    [Route("reportes/pagos")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IDataTableService _dataTableService;

        public PaymentController(
            IPaymentService paymentService,
            IDataTableService dataTableService
            )
        {
            _paymentService = paymentService;
            _dataTableService = dataTableService;
        }

        public IActionResult Index()
            => View();
        [HttpGet("get")]
        public async Task<IActionResult> Get(string searchValue)
        {
            var parameters = _dataTableService.GetSentParameters();
            var result = await _paymentService.GetClientsWithPayment(parameters, searchValue);
            return Ok(result);
        }            
    }
}
