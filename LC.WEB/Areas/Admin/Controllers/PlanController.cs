using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.PAYMENT.Services.Culqi;
using LC.PAYMENT.Services.Culqi.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.Plan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/planes")]
    [Area("Admin")]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        private readonly IDataTableService _dataTableService;
        private readonly ICulqiService _culqiService;
        private readonly IBenefitService _benefitService;

        public PlanController(
            IPlanService planService,
            IDataTableService dataTableService,
            ICulqiService culqiService,
            IBenefitService benefitService

            )
        {
            _planService = planService;
            _dataTableService = dataTableService;
            _culqiService = culqiService;
            _benefitService = benefitService;
        }

        public IActionResult Index()
            => View();

        [HttpGet("get")]
        public async Task<IActionResult> GetPlansDatatable(string searchValue)
        {
            var sentparameters = _dataTableService.GetSentParameters();
            var result = await _planService.GetPlansDatatable(sentparameters, searchValue);
            return Ok(result);
        }

        [HttpGet("agregar")]
        public IActionResult Add()
            => View();

        [HttpPost("agregar-plan")]
        public async Task<IActionResult> AddPlan(PlanViewModel model)
        {
            if (model.Amount == 0M)
            {
                if (await _planService.ExistFreePlan())
                    return BadRequest("Ya existe un plan gratuito registrado.");

                var planFree = new Plan
                {
                    Name = model.Name,
                    Amount = 0,
                    Description = model.Description,
                    Interval = 0,
                    Id = Guid.NewGuid().ToString(),
                    TrialDays = 0,
                    IntervalCount = 0,
                    DescriptionLC = model.DescriptionLC
                };

                await _planService.Insert(planFree);
            }
            else
            {
                var planModel = new CreatePlanModel
                {
                    Amount = (int)(model.Amount * 100),
                    Interval = ConstantHelpers.ENTITIES.PLAN.INTERVAL.VALUES[model.Interval],
                    IntervalCount = model.IntervalCount,
                    Name = model.Name,
                    TrialDays = model.TrialDays
                };

                var result = await _culqiService.CreatePlan(planModel);

                if (result.StatusCode == HttpStatusCode.BadRequest)
                    return BadRequest(result.UserMessage);

                var planEntity = new Plan
                {
                    Name = model.Name,
                    Amount = model.Amount,
                    Description = model.Description,
                    Interval = model.Interval,
                    Id = result.Id,
                    TrialDays = model.TrialDays,
                    IntervalCount = model.IntervalCount,
                    DescriptionLC = model.DescriptionLC
                };

                await _planService.Insert(planEntity);
            }

            return Ok();
        }

        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var plan = await _planService.Get(id);
            var benefits = await _benefitService.GetAll();
            var planBenefits = await _planService.GetPlanBenefits(id);
            var model = new PlanViewModel
            {
                Amount = plan.Amount,
                Description = plan.Description,
                Id = plan.Id,
                Interval = plan.Interval,
                IntervalCount = plan.IntervalCount,
                Name = plan.Name,
                TrialDays = plan.TrialDays,
                DescriptionLC = plan.DescriptionLC,
                Benefits = benefits.Select(x =>
                new Models.Plan.Benefit
                {
                    Id = x.Id,
                    Description = x.Description,
                    Assigned = planBenefits.Any(y => y.BenefitId == x.Id)
                })
                .ToList()
            };

            return View(model);
        }

        [HttpPost("eliminar-plan")]
        public async Task<IActionResult> DeletePlan(string id)
        {
            if (await _planService.AnyLawyerByPlan(id))
                return BadRequest("Existen abogados asociados al plan.");

            var entity = await _planService.Get(id);
            var planRequest = await _culqiService.DeletePlan(id);

            if (planRequest.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await _planService.Delete(entity);
            }
            else
            {
                return BadRequest(planRequest.UserMessage);
            }

            return Ok();
        }

        [HttpGet("{planId}/beneficios")]
        public async Task<IActionResult> Benefit(string planId)
        {
            var plan = await _planService.Get(planId);
            var benefits = await _benefitService.GetAll();
            var planBenefits = await _planService.GetPlanBenefits(planId);

            var model = new PlanViewModel
            {
                Name = plan.Name,
                Description = plan.Description,
                Amount = plan.Amount,
                Id = plan.Id,
                Benefits = benefits.Select(x =>
                new Models.Plan.Benefit
                {
                    Id = x.Id,
                    Description = x.Description,
                    Assigned = planBenefits.Any(y => y.BenefitId == x.Id)
                })
                .ToList()
            };

            return View(model);
        }

        [HttpPost("beneficios-actualizar")]
        public async Task<IActionResult> UpdateBenefits(PlanViewModel model)
        {
            var planBenefits = model.Benefits?.Where(x => x.Assigned)
                .Select(x => new PlanBenefit
                {
                    PlanId = model.Id,
                    BenefitId = x.Id
                })
                .ToList();
            var plan = await _planService.Get(model.Id);

            plan.DescriptionLC = model.DescriptionLC;
            await _planService.Update(plan);
            if (planBenefits!= null)
            {
                await _planService.UpdatePlanBenefits(planBenefits, model.Id);
            }
            return Ok();
        }
    }
}
