using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<ResultCustomModel> ProcessPayment(Payment payment);
        Task<DataTablesStructs.ReturnedData<object>> GetClientsWithPayment(DataTablesStructs.SentParameters parameters, string searchValue);
    }
}
