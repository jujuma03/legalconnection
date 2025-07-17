using LC.CORE.Helpers;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.DATABASE.Data.Seeds
{
    public class ConfigurationSeed
    {
        public static async Task Seed(LegalConnectionContext context)
        {
            var result = new List<Configuration>
            {
                new Configuration
                {
                    Key = ConstantHelpers.CONFIGURATION.MAX_SPECIALITY,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_SPECIALITY]
                },
                new Configuration
                {
                    Key = ConstantHelpers.CONFIGURATION.MAX_THEME_BY_SPECIALITY,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_THEME_BY_SPECIALITY]
                },
                new Configuration
                {
                    Key = ConstantHelpers.CONFIGURATION.MAX_VACANCIES,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_VACANCIES]
                }
            };

            await context.Configurations.AddRangeAsync(result);
            await context.SaveChangesAsync();
        }
    }
}
