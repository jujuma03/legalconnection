using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ApplicationRole> GetRoleByName(string name);
    }
}
