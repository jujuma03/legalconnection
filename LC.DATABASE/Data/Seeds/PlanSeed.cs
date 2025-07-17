using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.DATABASE.Data.Seeds
{
    public class PlanSeed
    {
        public static async Task Seed(LegalConnectionContext context)
        {
            var result = new List<Plan>
            {
                new Plan
                {
                    Id = Guid.NewGuid().ToString(),
                    Amount = 0,
                    Name = "Plan Gratuito",
                    Interval = 0,
                    IntervalCount = 0,
                    TrialDays = 0
                },
                new Plan
                {
                    Id = "pln_test_e49S9XK30gvqHL8D",
                    Name = "Plan Gold",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the ",
                    Amount = 170.8M,
                    Interval = 3,
                    IntervalCount = 1,
                    TrialDays = 2
                },
                  new Plan
                {
                    Id = "pln_test_lh3GfhpOFlUWyWcp",
                    Name = "Plan Premium",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the ",
                    Amount = 100.0M,
                    Interval = 3,
                    IntervalCount = 1,
                    TrialDays = 1
                },
            };

            await context.Plans.AddRangeAsync(result);
            await context.SaveChangesAsync();
        }
    }
}
