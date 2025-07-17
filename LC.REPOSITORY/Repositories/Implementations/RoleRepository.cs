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
    public class RoleRepository : Repository<ApplicationRole>, IRoleRepository
    {
        public RoleRepository(LegalConnectionContext context) : base(context) { }

        public async Task<ApplicationRole> GetRoleByName(string name)
            => await _context.Roles.Where(x => x.Name == name).FirstOrDefaultAsync();
    }
}
