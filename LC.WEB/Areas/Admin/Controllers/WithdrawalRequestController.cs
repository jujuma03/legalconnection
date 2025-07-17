using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.WithdrawalRequest;
using LC.WEB.Hubs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN + "," + ConstantHelpers.ROLES.ADVISER)]
    [Route("admin/solicitudes-retiro")]
    [Area("Admin")]
    public class WithdrawalRequestController : Controller
    {
        private readonly IDataTableService _dataTableService;
        private readonly IHubContext _hubContext;
        private readonly IUserService _userService;
        private readonly ILawyerService _lawyerService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly ILawyerWithdrawalInfoService _lawyerWithdrawalInfo;
        private readonly ILawyerWithdrawalRequestService _withdrawalRequestService;

        public WithdrawalRequestController(
            IDataTableService dataTableService,
            IHubContext hubContext,
            IUserService userService,
            ILawyerService lawyerService,
            ICloudStorageService cloudStorageService,
            ILawyerWithdrawalInfoService lawyerWithdrawalInfo,
            ILawyerWithdrawalRequestService withdrawalRequestService
            )
        {
            _dataTableService = dataTableService;
            _hubContext = hubContext;
            _userService = userService;
            _lawyerService = lawyerService;
            _cloudStorageService = cloudStorageService;
            _lawyerWithdrawalInfo = lawyerWithdrawalInfo;
            _withdrawalRequestService = withdrawalRequestService;
        }

        public IActionResult Index()
            => View();

        [HttpGet("get")]
        public async Task<IActionResult> GetDatatable(byte? status)
        {
            var parameters = _dataTableService.GetSentParameters();
            var result = await _withdrawalRequestService.GetWithdrawalRequestDatatable(parameters, status);
            return Ok(result);
        }

        [HttpGet("{id}/detalles")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var entity = await _withdrawalRequestService.Get(id);
            var lawyer = await _lawyerService.Get(entity.LawyerId);
            var lawyerInfo = await _lawyerWithdrawalInfo.Get(lawyer.Id);

            var model = new WithdrawalRequestViewModel
            {
                Amount = entity.Amount,
                CreatedAt = entity.CreatedAt.ToLocalDateTimeFormat(),
                DepositDate = entity.DepositDate.ToLocalDateTimeFormat(),
                Id = entity.Id,
                UrlDepositReceipt = entity.UrlDepositReceipt,
                Status = entity.Status,
                UlrReceiptFileForFees = entity.UlrReceiptFileForFees,
                Observation = entity.Observation,
                LawyerWithdrawalInfo = new LawyerWithdrawalInfoViewModel
                {
                    BankAccount = lawyerInfo.BankAccount,
                    Dni = lawyerInfo.Dni,
                    FinancialEntity = lawyerInfo.FinancialEntity,
                    FullName = lawyerInfo.FullName,
                    InterbankAccount = lawyerInfo.InterbankAccount,
                    LawyerId = lawyer.Id
                }
            };

            return View(model);
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> Update(WithdrawalRequestViewModel model)
        {
            var entity = await _withdrawalRequestService.Get(model.Id);
            var lawyer = await _lawyerService.Get(entity.LawyerId);
            var userLawyer = await _userService.Get(lawyer.UserId);

            if (model.DepositReceiptFile == null)
            {
                if (string.IsNullOrEmpty(model.Observation))
                    return BadRequest("Es necesario ingresar las observaciones.");

                entity.Status = ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.DENIED;
                entity.Observation = model.Observation;

                await _hubContext.SendNotification("Solicitud de retiro denegada", "/abogado/finanzas/retiro-efectivo", userLawyer.Id);
            }
            else
            {
                entity.Status = ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.DEPOSIT_DONE;
                var extension = Path.GetExtension(model.DepositReceiptFile.FileName);
                entity.UrlDepositReceipt = await _cloudStorageService.UploadFile(
                    model.DepositReceiptFile.OpenReadStream(),
                    ConstantHelpers.CLOUD_CONTAINERS.DEPOSIT_VOUCHERS,
                     $"{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid()}",
                    extension
                    );

                await _hubContext.SendNotification("Solicitud de retiro confirmada", "/abogado/finanzas/retiro-efectivo", userLawyer.Id);
            }

            await _withdrawalRequestService.InsertLawyerWithdrawals(new ENTITIES.Models.LawyerWithdrawal
            {
                Amount = entity.Amount,
                LawyerId = entity.LawyerId,
                WithdrawalRequestId = entity.Id,
                CreatedAt = DateTime.UtcNow
            });

            await _withdrawalRequestService.Update(entity);
            return Ok();
        }
    }
}
