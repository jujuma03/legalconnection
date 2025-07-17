using LC.PAYMENT.Models;
using LC.PAYMENT.Services.Culqi.Models;
using LC.PAYMENT.Services.Culqi.Models.Base;
using LC.PAYMENT.Services.Culqi.Models.Base.Card;
using LC.PAYMENT.Services.Culqi.Models.Base.Chargue;
using LC.PAYMENT.Services.Culqi.Models.Base.Customer;
using LC.PAYMENT.Services.Culqi.Models.Base.Plan;
using LC.PAYMENT.Services.Culqi.Models.Base.Supscription;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.PAYMENT.Services.Culqi
{
    public interface ICulqiService
    {
        Task<PaymentRequestTracker<ChargueModel>> CreateChargue(CreateChargueModel model);
        Task<PaymentRequestTracker<ChargueModel>> GetChargue(string id);
        Task<PaymentRequestTracker<CustomerModel>> CreateCustomer(CreateCustomerModel model);
        Task<PaymentRequestTracker<CustomerModel>> GetCustomer(string id);
        Task<PaymentRequestTracker<CustomerModel>> DeleteCustomer(string id);
        Task<PaymentRequestTracker<CardModel>> CreateCard(CreateCardModel model);
        Task<PaymentRequestTracker<CardModel>> GetCard(string id);
        Task<PaymentRequestTracker<CardModel>> DeleteCard(string id);
        Task<PaymentRequestTracker<PlanModel>> CreatePlan(CreatePlanModel model);
        Task<PaymentRequestTracker<PlanModel>> GetPlan(string id);
        Task<PaymentRequestTracker<PlanModel>> DeletePlan(string id);
        Task<PaymentRequestTracker<DataListModel<PlanModel>>> GetListPlans();
        Task<PaymentRequestTracker<SupscriptionModel>> CreateSubscription(CreateSubscription model);
        Task<PaymentRequestTracker<SupscriptionModel>> GetSubscription(string id);
        Task<PaymentRequestTracker<SupscriptionModel>> CancelSubscription(string id);
    }
}
