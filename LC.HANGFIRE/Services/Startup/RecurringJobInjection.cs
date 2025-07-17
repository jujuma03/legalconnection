using Hangfire;
using Hangfire.Common;
using LC.HANGFIRE.Services.RecurringTask.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.HANGFIRE.Services.Startup
{
    public static class RecurringJobInjection
    {
        public static void InjectRecurringJobs(this IRecurringJobManager recurringJobManager)
        {
            recurringJobManager.AddOrUpdate("UpdateFreeConsulting", Job.FromExpression<IRecurringTaskLawyerService>(x=>x.UpdateFreeConsulting()) , Cron.Monthly());
        }
    }
}
