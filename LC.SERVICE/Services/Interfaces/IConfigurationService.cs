using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<Configuration> GetByKey(string key);
        Task Update(Configuration configuration);
    }
}
