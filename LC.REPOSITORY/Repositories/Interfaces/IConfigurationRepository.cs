using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IConfigurationRepository : IRepository<Configuration>
    {
        Task<Configuration> GetByKey(string key);
    }
}
