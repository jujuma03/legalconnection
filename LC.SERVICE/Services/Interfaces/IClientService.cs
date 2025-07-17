using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IClientService
    {
        Task Insert(Client client);
        Task<Client> GetByUserId(string id);
        Task<Client> Get(Guid clientId);
    }
}
