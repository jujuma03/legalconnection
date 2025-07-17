using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Structs;
using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class ClientRepository : Repository<Client> , IClientRepository
    {
        public ClientRepository(LegalConnectionContext context) : base(context) { }

        public async Task<Client> GetByUserId(string id)
        {
            return await _context.Clients.Include(x => x.LegalCases).Where(x => x.UserId == id).FirstOrDefaultAsync();
        }
    }
}
