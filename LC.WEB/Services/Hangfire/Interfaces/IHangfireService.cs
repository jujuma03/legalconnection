using LC.WEB.Services.Hangfire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Services.Hangfire.Interfaces
{
    public interface IHangfireService
    {
        Task CreateLegalCaseDelayedTask(CreateLegalCaseTask model);
        Task CreateDirectLegalCaseDelayedTask(CreateLegalCaseTask model);
        Task ExecuteLEgalCaseDelayedTask(ExecuteLegalCaseTask model);
    }
}
