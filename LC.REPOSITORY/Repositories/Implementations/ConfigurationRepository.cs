using LC.CORE.Helpers;
using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class ConfigurationRepository : Repository<Configuration> , IConfigurationRepository
    {
        public ConfigurationRepository(LegalConnectionContext context) : base(context) { }

        public async Task<Configuration> GetByKey(string key)
        {
            var configuration = await _context.Configurations.Where(x => x.Key == key).FirstOrDefaultAsync();
            if(configuration == null)
            {
                configuration = new Configuration
                {
                    Key = key,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[key]
                };

                await _context.AddAsync(configuration);
                await _context.SaveChangesAsync();
            }

            return configuration;
        }

    }
}
