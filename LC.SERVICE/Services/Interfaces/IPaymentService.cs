using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<ResultCustomModel> ProcessPayment(Payment payment);
        Task<DataTablesStructs.ReturnedData<object>> GetClientsWithPayment(DataTablesStructs.SentParameters parameters, string searchValue);
    }
}
