using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.PAYMENT.Services.Culqi;
using LC.PAYMENT.Services.Culqi.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Lawyer.Models.Plan;
using LC.WEB.Controllers;
using LC.WEB.Filters.Lawyer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Lawyer.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.LAWYER)]
    [PlanFilterAttribute]
    [Area("Lawyer")]
    [Route("abogado/planes")]
    public class PlanController : LawyerBaseController
    {
        private readonly IPlanService _planService;
        private readonly ICulqiService _culqiService;
        private readonly ILawyerCardService _lawyerCardService;
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly IBenefitService _benefitService;
        private readonly ILawyerService _lawyerService;
        private readonly IPaginationService _paginationService;
        private readonly ILawyerPlanDetailService _lawyerPlanDetailService;

        public PlanController(
            IPlanService planService,
            ICulqiService culqiService,
            ILawyerCardService lawyerCardService,
            IPaymentService paymentService,
            IUserService userService,
            IBenefitService benefitService,
            ILawyerService lawyerService,
            IPaginationService paginationService,
            ILawyerPlanDetailService lawyerPlanDetailService
            )
        {
            _planService = planService;
            _culqiService = culqiService;
            _lawyerCardService = lawyerCardService;
            _paymentService = paymentService;
            _userService = userService;
            _benefitService = benefitService;
            _lawyerService = lawyerService;
            _paginationService = paginationService;
            _lawyerPlanDetailService = lawyerPlanDetailService;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _planService.GetAll();
            var benefits = await _benefitService.GetAll();
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var modelPlans = new List<PlanViewModel>();

            if (!ConstantHelpers.GENERAL.SHOW_TEST_PLAN)
            {
                plans = plans.Where(x => x.TrialDays != 1).ToList();
            }

            foreach (var plan in plans.OrderBy(x=>x.Amount))
            {
                var planModel = new PlanViewModel
                {
                    Amount = plan.Amount,
                    Description = plan.Description,
                    Id = plan.Id,
                    Name = plan.Name
                };

                planModel.Benefits = benefits
                    .Select(x => new BenefitViewModel
                    {
                        Description = x.Description,
                        Assigned = plan.PlanBenefits.Any(y => y.BenefitId == x.Id)
                    })
                    .ToList();

                modelPlans.Add(planModel);
            }

            var currentPlan = await _lawyerPlanDetailService.Get(lawyer.Id);
            if(currentPlan is null)
            {
                var plan = await _planService.GetFreePlan();
                currentPlan = new LawyerPlanDetail
                {
                    LawyerId = lawyer.Id,
                    PlanId = plan.Id,
                };

                await _lawyerPlanDetailService.Insert(currentPlan);
            }

            var model = new UserPlanViewModel
            {
                CurrentPlanId = currentPlan.PlanId,
                Plans = modelPlans
            };

            return View(model);
        }

        [HttpPost("cambiar-plan")]
        public async Task<IActionResult> ChangePlanDetail(ChangePlanViewModel model)
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _lawyerService.GetByUserId(user.Id);
            var currentPlan = await _lawyerPlanDetailService.Get(lawyer.Id);
            var freePlan = await _planService.GetFreePlan();

            if (currentPlan.PlanId == model.PlanId)
                return BadRequest("Ya se encuentra en el plan seleccionado.");

            currentPlan.PlanId = model.PlanId;

            if (freePlan.Id == model.PlanId)
            {
                currentPlan.TempStartDate = null;
                currentPlan.TempEndDate = null;
                lawyer.FreeUser = true;
            }
            else
            {
                var cardsQuantity = await _lawyerCardService.GetLawyerCardQuantity(lawyer.Id);
                if (cardsQuantity == 0)
                    return BadRequest("No tiene tarjetas asociadas a su cuenta.");

                var cardDefault = await _lawyerCardService.GetDefaultLawyerCard(lawyer.Id);

                if(cardDefault is null)
                    return BadRequest("No tiene asignada una tarjeta por defecto.");

                var result = await _culqiService.CreateSubscription(new CreateSubscription
                {
                    PlanId = model.PlanId,
                    CardId = cardDefault.Id
                });

                if (result.StatusCode != System.Net.HttpStatusCode.Created)
                    return BadRequest(result.UserMessage);

                currentPlan.SubscriptionId = result.Id; 
                currentPlan.TempStartDate = DateTime.UtcNow;
                currentPlan.TempEndDate = DateTime.UtcNow.AddMonths(1);
                currentPlan.LawyerCardId = cardDefault.Id;
                lawyer.FreeUser = false;
            }

            await _lawyerPlanDetailService.Update(currentPlan);
            return Ok();
        }
    }
}
