using Hangfire;
using Hangfire.States;
using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using LC.HANGFIRE.Models.DelayedTask.LegalCase;
using LC.HANGFIRE.Services.DelayedTask.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.HANGFIRE.Areas.DelayedTask.Controllers
{
    [ApiController]
    [Route("api/delayedtask/[controller]/[action]")]
    public class LegalCaseController : ControllerBase
    {
        private readonly LegalConnectionContext _context;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IDelayedTaskLegalCaseService _delayedTaskLegalCaseService; 

        public LegalCaseController(
            LegalConnectionContext context,
            IBackgroundJobClient backgroundJobClient,
            IDelayedTaskLegalCaseService delayedTaskLegalCaseService
            )
        {
            _context= context;
            _backgroundJobClient = backgroundJobClient;
            _delayedTaskLegalCaseService = delayedTaskLegalCaseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateModel model)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == model.LegalCaseId).FirstOrDefaultAsync();

            if (legalCase is null)
                return BadRequest();

            var maxHourToLawyersPostulate = Convert.ToInt32(await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE).Select(x => x.Value).FirstOrDefaultAsync());

            var job = BackgroundJob.Schedule(() => _delayedTaskLegalCaseService.EndApplicationTime(model.LegalCaseId), legalCase.ValidationDate.Value.AddHours(maxHourToLawyersPostulate));

            var legalCaseJob = new LegalCaseDelayedTask
            {
                Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.END_APPLICATION_TIME,
                HangfireJobId = job,
                LegalCaseId = legalCase.Id
            };

            await _context.LegalCaseDelayedTasks.AddAsync(legalCaseJob);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDirect([FromBody]CreateModel model)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == model.LegalCaseId).FirstOrDefaultAsync();

            if (legalCase is null)
                return BadRequest();

            var maxHourToLawyerAcceptCase = Convert.ToInt32(await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_ACCEPT_DIRECT).Select(x => x.Value).FirstOrDefaultAsync());
            var job = BackgroundJob.Schedule(() => _delayedTaskLegalCaseService.EndTimeToLawyerAcceptDirect(model.LegalCaseId), legalCase.ValidationDate.Value.AddHours(maxHourToLawyerAcceptCase));

            var legalCaseJob = new LegalCaseDelayedTask
            {
                Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.END_TIME_TO_LAWYER_ACCEPT,
                HangfireJobId = job,
                LegalCaseId = legalCase.Id
            };

            await _context.LegalCaseDelayedTasks.AddAsync(legalCaseJob);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Execute(ExecuteModel model)
        {
            var delayedTask = await _context.LegalCaseDelayedTasks.Where(x => x.LegalCaseId == model.LegalCaseId && x.Task == model.Task).FirstOrDefaultAsync();
            _backgroundJobClient.ChangeState(delayedTask.HangfireJobId, new EnqueuedState(), ScheduledState.StateName);

            if (delayedTask == null)
                return BadRequest($"El caso legal no tiene un job con la tarea {model.Task}. Contactar al administrador.");
            
            return Ok();
        }
    }
}
