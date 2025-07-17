using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> Get(Guid clientId)
        {
            return await _clientRepository.Get(clientId);
        }

        public async Task<Client> GetByUserId(string id)
        {
            return await _clientRepository.GetByUserId(id);
        }

        public async Task Insert(Client client)
        {
            await _clientRepository.Insert(client);
        }
    }
}
