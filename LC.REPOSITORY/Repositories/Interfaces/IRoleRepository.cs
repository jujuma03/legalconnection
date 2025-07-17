using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<ApplicationRole> GetRoleByName(string name);
    }
}
