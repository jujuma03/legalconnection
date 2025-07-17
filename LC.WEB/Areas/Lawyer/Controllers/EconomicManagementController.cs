using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Lawyer.Models.EconomicManagement;
using LC.WEB.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.LAWYER)]
    [Area("Lawyer")]
    [Route("abogado/finanzas")]
    public class EconomicManagementController : LawyerBaseController
    {
        private readonly IUserService _userService;
        private readonly ILawyerService _lawyerService;
        private readonly ILegalCaseService _legalCaseService;
        private readonly IPaymentService _paymentService;
        private readonly IDataTableService _dataTableService;
        private readonly IConfigurationService _configurationService;
        private readonly ILawyerWithdrawalRequestService _withdrawalRequestService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly IPaginationService _paginationService;
        private readonly ILawyerWithdrawalInfoService _lawyerWithdrawalInfoService;

        public EconomicManagementController(
            IUserService userService,
            ILawyerService lawyerService,
            ILegalCaseService legalCaseService,
            IPaymentService paymentService,
            IDataTableService dataTableService,
            IConfigurationService configurationService,
            ILawyerWithdrawalRequestService withdrawalRequestService,
            ICloudStorageService cloudStorageService,
            IPaginationService paginationService,
            ILawyerWithdrawalInfoService lawyerWithdrawalInfoService
            )
        {
            _userService = userService;
            _lawyerService = lawyerService;
            _legalCaseService = legalCaseService;
            _paymentService = paymentService;
            _dataTableService = dataTableService;
            _configurationService = configurationService;
            _withdrawalRequestService = withdrawalRequestService;
            _cloudStorageService = cloudStorageService;
            _paginationService = paginationService;
            _lawyerWithdrawalInfoService = lawyerWithdrawalInfoService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var model = await _lawyerService.GetEconomicManagementLawyerCustomModel(lawyer.Id);
            return View(model);
        }
        [HttpGet("movimientos")]
        public async Task<IActionResult> Movements()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            ViewBag.LawyerId = lawyer.Id;
            return View();
        }
        [HttpGet("retiro-efectivo")]
        public async Task<IActionResult> CashWithdrawal()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var configuration = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WITHDRAWAL_REQUEST_DAY);
            var canWithdrawalRequest = false;
            var model = new WithdrawalRequestViewModel();
            var withdrawalRequestDay = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WITHDRAWAL_REQUEST_DAY);


            if (configuration.Value == $"{DateTime.UtcNow.ToDefaultTimeZone().DayOfWeek}")
                canWithdrawalRequest = true;
            
            ViewBag.LawyerId = lawyer.Id;
            ViewBag.CanWithdrawalRequest = canWithdrawalRequest;
            ViewBag.WithdrawalRequestDay = ConstantHelpers.WEEKDAY.GetWeekDayName(configuration.Value);
            if ($"{DateTime.UtcNow.ToDefaultTimeZone().DayOfWeek}" == withdrawalRequestDay.Value)
            {
                var lawyerInfo = await _lawyerWithdrawalInfoService.Get(lawyer.Id);

                if (lawyerInfo == null)
                {
                    lawyerInfo = new LawyerWithdrawalInfo
                    {
                        BankAccount = "",
                        FinancialEntity = ConstantHelpers.ENTITIES.LAWYER_WITHDRAWAL_INFO.FINANCIAL_ENTITY.NONE,
                        Dni = user.Document,
                        FullName = $"{user.Surnames}, {user.Name}",
                        InterbankAccount = "",
                        LawyerId = lawyer.Id
                    };

                    await _lawyerWithdrawalInfoService.Insert(lawyerInfo);
                }

                model.FinancialEntity = lawyerInfo.FinancialEntity;
                model.BankAccount = lawyerInfo.BankAccount;
                model.InterbankAccount = lawyerInfo.InterbankAccount;
                model.Dni = lawyerInfo.Dni;
                model.FullName = lawyerInfo.FullName;
                model.CanRequest = true;

            }
            else
            {
                model.CanRequest = false;
            }

            return View(model);
        }

        [HttpGet("get-saldo-disponible")]
        public async Task<IActionResult> GetAvailableBalance()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var result = await _lawyerService.GetAvailableBalance(lawyer.Id);
            return Ok(result);
        }
        [HttpGet("get-saldo-proceso")]
        public async Task<IActionResult> GetInProcessBalance()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var result = await _lawyerService.GetInProcessBalance(lawyer.Id);
            return Ok(result);
        }

        [HttpGet("get-saldo-posible-disponible")]
        public async Task<IActionResult> GetPossibleAvailableBalance()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var result = await _lawyerService.GetPossibleAvailableBalance(lawyer.Id);
            return Ok(result);
        }
        [HttpGet("get-saldos")]
        public async Task<IActionResult> GetBalance()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var result = await _lawyerService.GetBalance(lawyer.Id);
            return Ok(result);
        }

        [HttpGet("get-casos-concluidos")]
        public async Task<IActionResult> GetLegalCaseFinished()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var parameters = _paginationService.GetSentParameters();
            var result = await _legalCaseService.GetLegalCasesFinishedToLawyer(parameters,lawyer.Id);
            return PartialView("Partials/Views/LegalCaseFinalizedPartialView", result);
        }

        [HttpGet("get-solicitudes-retiro")]
        public async Task<IActionResult> GetWithdrawalRequests()
        {
            var user = await _userService.GetUserByClaim(User);
            var parameters = _dataTableService.GetSentParameters();
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var data = await _withdrawalRequestService.GetWithdrawalRequestDatatable(parameters, null, lawyer.Id);
            return Ok(data);
        }

        [HttpGet("get-solicitudes-retiro-partial")]
        public async Task<IActionResult> GetWithdrawalRequestsPartial()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var parameters = _paginationService.GetSentParameters();
            var result = await _withdrawalRequestService.GetWithdrawalRequest(parameters,null, lawyer.Id);
            return PartialView("Partials/Views/MovementsPartialView", result);
        }

        [HttpGet("solicitud-retiro")]
        public async Task<IActionResult> WithdrawalRequest()
        {
            var model = new WithdrawalRequestViewModel();
            var withdrawalRequestDay = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WITHDRAWAL_REQUEST_DAY);

            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            if($"{DateTime.UtcNow.ToDefaultTimeZone().DayOfWeek}" == withdrawalRequestDay.Value)
            {
                var lawyerInfo = await _lawyerWithdrawalInfoService.Get(lawyer.Id);

                if (lawyerInfo == null)
                {
                    lawyerInfo = new LawyerWithdrawalInfo
                    {
                        BankAccount = "",
                        FinancialEntity = ConstantHelpers.ENTITIES.LAWYER_WITHDRAWAL_INFO.FINANCIAL_ENTITY.NONE,
                        Dni = user.Document,
                        FullName = $"{user.Surnames}, {user.Name}",
                        InterbankAccount = "",
                        LawyerId = lawyer.Id
                    };

                    await _lawyerWithdrawalInfoService.Insert(lawyerInfo);
                }

                model.FinancialEntity = lawyerInfo.FinancialEntity;
                model.BankAccount = lawyerInfo.BankAccount;
                model.InterbankAccount = lawyerInfo.InterbankAccount;
                model.Dni = lawyerInfo.Dni;
                model.FullName = lawyerInfo.FullName;
                model.CanRequest = true;

            }
            else
            {
                model.CanRequest = false;
            }

            return View(model);
        }

        [HttpPost("ingresar-solicitud-retiro")]
        public async Task<IActionResult> SaveWithdrawalRequest(WithdrawalRequestViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var lawyerInfo = await _lawyerWithdrawalInfoService.Get(lawyer.Id);
            var availableBalance = await _lawyerService.GetAvailableBalance(lawyer.Id);

            if(model.Amount > availableBalance)
                return BadRequest($"No cuentas con los fondos suficientes para solicitar la cantidad de {model.Amount} soles.");

            if (model.FinancialEntity == ConstantHelpers.ENTITIES.LAWYER_WITHDRAWAL_INFO.FINANCIAL_ENTITY.NONE)
                return BadRequest("Es necesario seleccionar un banco válido.");

            lawyerInfo.BankAccount = model.BankAccount;
            lawyerInfo.Dni = model.Dni;
            lawyerInfo.FinancialEntity = model.FinancialEntity;
            lawyerInfo.FullName = model.FullName;
            lawyerInfo.InterbankAccount = model.InterbankAccount;

            var request = new LawyerWithdrawalRequest
            {
                Amount = model.Amount,
                LawyerId = lawyer.Id,
                BankAccount = model.BankAccount,
                Dni = model.Dni,
                FinancialEntity = model.FinancialEntity,
                FullName = model.FullName,
                InterbankAccount = model.InterbankAccount
            };

            var extension = Path.GetExtension(model.ReceiptFileForFees.FileName);
            request.UlrReceiptFileForFees = await _cloudStorageService.UploadFile(
                model.ReceiptFileForFees.OpenReadStream(),
                ConstantHelpers.CLOUD_CONTAINERS.WITHDRAWAL_REQUEST,
                 $"{lawyer.Id}-{Guid.NewGuid()}",
                extension
                );

            await _withdrawalRequestService.Insert(request);
            return Ok();
        }
    }
}
