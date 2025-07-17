using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.PAYMENT.Services.Culqi;
using LC.PAYMENT.Services.Culqi.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Lawyer.Models.YourAccount;
using LC.WEB.Controllers;
using LC.WEB.Models.Card;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.LAWYER)]
    [Area("Lawyer")]
    [Route("abogado/tu-cuenta")]
    public class YourAccountController : LawyerBaseController
    {
        private readonly ILawyerService _lawyerService;
        private readonly IUserService _userService;
        private readonly ILawyerPlanDetailService _lawyerPlanDetailService;
        private readonly ILawyerCardService _lawyerCardService;
        private readonly IPaginationService _paginationService;
        private readonly ICulqiService _culqiService;
        private readonly IPlanService _planService;

        public YourAccountController(
            ILawyerService lawyerService,
            IUserService userService,
            ILawyerPlanDetailService lawyerPlanDetailService,
            ILawyerCardService lawyerCardService,
            IPaginationService paginationService,
            ICulqiService culqiService,
            IPlanService planService
            )
        {
            _lawyerService = lawyerService;
            _userService = userService;
            _lawyerPlanDetailService = lawyerPlanDetailService;
            _lawyerCardService = lawyerCardService;
            _paginationService = paginationService;
            _culqiService = culqiService;
            _planService = planService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var planDetail = await _lawyerPlanDetailService.Get(lawyer.Id);
            var plan = await _planService.Get(planDetail.PlanId);
            var lawyerCard = await _lawyerCardService.Get(planDetail.LawyerCardId);

            var model = new PlanDetail
            {
                Amount = plan.Amount,
                NextBillingDate = planDetail.TempEndDate.ToLocalDateFormat(),
                NextBillingDateTime = planDetail.TempEndDate,
                Plan = plan.Name,
                Canceled = planDetail.Canceled
            };

            if(lawyerCard != null)
            {
                model.CurrentCard = new LawyerCardViewModel
                {
                    CardBrand = lawyerCard.CardBrand,
                    Id = lawyerCard.Id,
                    LastCardDigits = lawyerCard.LastCardDigits,
                    Owner = lawyerCard.Owner
                };
            }

            return View(model);
        }

        #region Billing Data

        [HttpPost("actualizar-datos-facturacion")]
        public async Task<IActionResult> UpdateBillingData([Bind(Prefix = "BillingData")]BillingDataViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            if (!string.IsNullOrEmpty(lawyer.CustomerId))
            {
                var customerResult = await _culqiService.GetCustomer(lawyer.CustomerId);
                
                if(customerResult.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest("Se encontró datos de facturación registrados previamente.");
                }
            }

            var customerData = new CreateCustomerModel
            {
                Address = model.Address,
                AddressCity = model.AddressCity,
                Email = user.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.Phone
            };

            var newCustomerResult = await _culqiService.CreateCustomer(customerData);

            if (newCustomerResult.StatusCode == HttpStatusCode.Created)
            {
                lawyer.CustomerId = newCustomerResult.Id;
                await _lawyerService.Update(lawyer);
                return Ok(newCustomerResult.UserMessage);
            }
            else
            {
                return BadRequest(newCustomerResult.UserMessage);
            }
        }

        [HttpPost("cancelar-suscripcion")]
        public async Task<IActionResult> CancelSubscription()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var planDetail = await _lawyerPlanDetailService.Get(lawyer.Id);
            planDetail.Canceled = true;

            var result = await _culqiService.CancelSubscription(planDetail.SubscriptionId);

            if (result.StatusCode != HttpStatusCode.OK)
                return BadRequest(result.UserMessage);

            await _lawyerPlanDetailService.Update(planDetail);
            return Ok();
        }

        #endregion

        #region Payment Methods

        [HttpGet("metodos-pago")]
        public async Task<IActionResult> PaymentMethods()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            var model = new LawyerViewModel
            {
                Email = user.Email
            };

            var billingModel = new BillingDataViewModel();

            if (!string.IsNullOrEmpty(lawyer.CustomerId))
            {
                var billingDataResponse = await _culqiService.GetCustomer(lawyer.CustomerId);

                if (billingDataResponse.StatusCode == HttpStatusCode.OK)
                {
                    billingModel.FirstName = billingDataResponse.ModelResponse.AntifraudDetail.FirstName;
                    billingModel.LastName = billingDataResponse.ModelResponse.AntifraudDetail.LastName;
                    billingModel.AddressCity = billingDataResponse.ModelResponse.AntifraudDetail.AddressCity;
                    billingModel.Address = billingDataResponse.ModelResponse.AntifraudDetail.Address;
                    billingModel.Phone = billingDataResponse.ModelResponse.AntifraudDetail.Phone;
                    billingModel.IsNew = false;
                }
            }

            model.BillingData = billingModel;

            return View(model);
        }

        [HttpGet("get-tarjetas")]
        public async Task<IActionResult> GetCards()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var parameters = _paginationService.GetSentParameters();
            var model = await _lawyerCardService.GetLawyerCards(parameters, lawyer.Id);
            return PartialView("Partials/Views/LawyerCardPartialView", model);
        }

        [HttpPost("validar-usuario-pago")]
        public async Task<IActionResult> ValidatePaymentUser()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            if (string.IsNullOrEmpty(lawyer.CustomerId))
                return BadRequest("El abogado no tiene asignado un usuario de pago. Por favor llenar los datos de facturación.");

            return Ok();
        }

        [HttpPost("agregar-tarjeta")]
        public async Task<IActionResult> CreateCard(CardViewModel model)
        {
            if (string.IsNullOrEmpty(model.Token) || model.Token == "undefined")
                return BadRequest("Todos los campos son obligatorios");

            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            if (string.IsNullOrEmpty(lawyer.CustomerId))
                return BadRequest("El abogado no tiene asignado un usuario de pago. Por favor contactarse con el administrador.");

            var cardModel = new CreateCardModel
            {
                CustomerId = lawyer.CustomerId,
                TokenId = model.Token
            };

            var cardResult = await _culqiService.CreateCard(cardModel);

            if(cardResult.StatusCode == HttpStatusCode.Created)
            {
                var cardsQuantity = await _lawyerCardService.GetLawyerCardQuantity(lawyer.Id);

                var lawyerCard = new LawyerCard
                {
                    CardBrand = cardResult.ModelResponse.Source.Iin.CardBrand,
                    LawyerId = lawyer.Id,
                    LastCardDigits = cardResult.ModelResponse.Source.LastFour,
                    Owner = cardResult.ModelResponse.Source.Iin.Issuer.Name,
                    Id = cardResult.Id,
                    Default = cardsQuantity == 0
                };

                await _lawyerCardService.Insert(lawyerCard);
                return Ok();
            }
            else
            {
                return BadRequest(cardResult.UserMessage);
            }
        }

        [HttpPost("eliminar-tarjeta")]
        public async Task<IActionResult> DeleteCreditCard(string cardId)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);

            var card = await _lawyerCardService.Get(cardId);
            var lawyerPlanDetail = await _lawyerPlanDetailService.Get(lawyer.Id);

            if (lawyerPlanDetail.LawyerCardId == cardId)
                return BadRequest("La tarjeta se encuentra asociada a una suscripción");

            var result = await _culqiService.DeleteCard(cardId);
            if (result.StatusCode != HttpStatusCode.OK)
                return BadRequest(result.UserMessage);

            await _lawyerCardService.Delete(card);
            return Ok();
        }

        #endregion
    }
}
