using LC.CORE.Structs;
using LC.ENTITIES.Custom;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<ResultCustomModel> ProcessPayment(Payment payment)
            => await _paymentRepository.ProcessPayment(payment);

        public async Task<DataTablesStructs.ReturnedData<object>> GetClientsWithPayment(DataTablesStructs.SentParameters parameters, string searchValue)
        {
            return await _paymentRepository.GetClientsWithPayment(parameters, searchValue);
        }
    }
}
