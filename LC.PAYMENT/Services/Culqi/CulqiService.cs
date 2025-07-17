using LC.PAYMENT.Models;
using LC.PAYMENT.Services.Culqi.Helpers;
using LC.PAYMENT.Services.Culqi.Models;
using LC.PAYMENT.Services.Culqi.Models.Base;
using LC.PAYMENT.Services.Culqi.Models.Base.Card;
using LC.PAYMENT.Services.Culqi.Models.Base.Chargue;
using LC.PAYMENT.Services.Culqi.Models.Base.Customer;
using LC.PAYMENT.Services.Culqi.Models.Base.Plan;
using LC.PAYMENT.Services.Culqi.Models.Base.Supscription;
using LC.PAYMENT.Utilities.RequestManager;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LC.PAYMENT.Services.Culqi
{
    public class CulqiService : ICulqiService
    {
        private readonly IPaymentRequestManager _paymentRequestManager;

        public CulqiService(
            IPaymentRequestManager paymentRequestManager
            )
        {
            _paymentRequestManager = paymentRequestManager;
        }

        //CHARGUE
        public async Task<PaymentRequestTracker<ChargueModel>> CreateChargue(CreateChargueModel model)
        {
            var result = new PaymentRequestTracker<ChargueModel>();

            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Post, CulqiHelpers.URL.CREATECHARGUE());
            _paymentRequestManager.LoadRequestBody(request, model);
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if(webResult.StatusCode == HttpStatusCode.Created)
            {
                var modelResponse = JsonConvert.DeserializeObject<ChargueModel>(webResult.JsonResponse);

                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
                result.UserMessage = "Pago realizado satisfactoriamente.";
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        } 

        public async Task<PaymentRequestTracker<ChargueModel>> GetChargue(string id)
        {
            var result = new PaymentRequestTracker<ChargueModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Get, CulqiHelpers.URL.GETCHARGUE(id));
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);
            if (webResult.StatusCode == HttpStatusCode.OK)
            {
                var modelResponse = JsonConvert.DeserializeObject<ChargueModel>(webResult.JsonResponse);
                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        //CUSTOMER
        public async Task<PaymentRequestTracker<CustomerModel>> CreateCustomer(CreateCustomerModel model)
        {
            var result = new PaymentRequestTracker<CustomerModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Post, CulqiHelpers.URL.CREATECUSTOMER());
            _paymentRequestManager.LoadRequestBody(request, model);
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if (webResult.StatusCode == HttpStatusCode.Created)
            {
                var modelResponse = JsonConvert.DeserializeObject<CustomerModel>(webResult.JsonResponse);

                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
                result.UserMessage = "Cliente creado satisfactoriamente.";
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        public async Task<PaymentRequestTracker<CustomerModel>> GetCustomer(string id)
        {
            var result = new PaymentRequestTracker<CustomerModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Get, CulqiHelpers.URL.GETCUSTOMER(id));
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);
            if (webResult.StatusCode == HttpStatusCode.OK)
            {
                var modelResponse = JsonConvert.DeserializeObject<CustomerModel>(webResult.JsonResponse);
                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        public async Task<PaymentRequestTracker<CustomerModel>> DeleteCustomer(string id)
        {
            var result = new PaymentRequestTracker<CustomerModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Delete, CulqiHelpers.URL.DELETECUSTOMER(id));
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);
            if (webResult.StatusCode == HttpStatusCode.OK)
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = "Cliente eliminado satisfactoriamente.";
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        //CARD
        public async Task<PaymentRequestTracker<CardModel>> CreateCard(CreateCardModel model)
        {
            var result = new PaymentRequestTracker<CardModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Post, CulqiHelpers.URL.CREATECARD());
            _paymentRequestManager.LoadRequestBody(request, model);
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if (webResult.StatusCode == HttpStatusCode.Created)
            {
                var modelResponse = JsonConvert.DeserializeObject<CardModel>(webResult.JsonResponse);

                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
                result.UserMessage = "Tarjeta creada satisfactoriamente.";
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        public async Task<PaymentRequestTracker<CardModel>> GetCard(string id)
        {
            var result = new PaymentRequestTracker<CardModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Get, CulqiHelpers.URL.GETCARD(id));
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);
            if (webResult.StatusCode == HttpStatusCode.OK)
            {
                var modelResponse = JsonConvert.DeserializeObject<CardModel>(webResult.JsonResponse);
                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        public async Task<PaymentRequestTracker<CardModel>> DeleteCard(string id)
        {
            var result = new PaymentRequestTracker<CardModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Delete, CulqiHelpers.URL.DELETECARD(id));
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);
            if (webResult.StatusCode == HttpStatusCode.OK)
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = "Tarjeta eliminada satisfactoriamente.";
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        //PLANS
        public async Task<PaymentRequestTracker<PlanModel>> CreatePlan(CreatePlanModel model)
        {
            var result = new PaymentRequestTracker<PlanModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Post, CulqiHelpers.URL.CREATEPLAN());
            _paymentRequestManager.LoadRequestBody(request, model);
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if (webResult.StatusCode == HttpStatusCode.Created)
            {
                var modelResponse = JsonConvert.DeserializeObject<PlanModel>(webResult.JsonResponse);

                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
                result.UserMessage = "Plan creado satisfactoriamente.";
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        public async Task<PaymentRequestTracker<PlanModel>> GetPlan(string id)
        {
            var result = new PaymentRequestTracker<PlanModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Get, CulqiHelpers.URL.GETPLAN(id));
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if (webResult.StatusCode == HttpStatusCode.OK)
            {
                var modelResponse = JsonConvert.DeserializeObject<PlanModel>(webResult.JsonResponse);

                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        public async Task<PaymentRequestTracker<PlanModel>> DeletePlan(string id)
        {
            var result = new PaymentRequestTracker<PlanModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Delete, CulqiHelpers.URL.DELETEPLAN(id));
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if (webResult.StatusCode == HttpStatusCode.OK)
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = "Plan eliminado satisfactoriamente.";
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        public async Task<PaymentRequestTracker<DataListModel<PlanModel>>> GetListPlans()
        {
            var result = new PaymentRequestTracker<DataListModel<PlanModel>>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Get, CulqiHelpers.URL.GETLIST());
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if (webResult.StatusCode == HttpStatusCode.Created)
            {
                var modelResponse = JsonConvert.DeserializeObject<DataListModel<PlanModel>>(webResult.JsonResponse);

                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        //SUBSCRIPTIONS

        public async Task<PaymentRequestTracker<SupscriptionModel>> CreateSubscription(CreateSubscription model)
        {
            var result = new PaymentRequestTracker<SupscriptionModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Post, CulqiHelpers.URL.CREATESUBSCRIPTION());
            _paymentRequestManager.LoadRequestBody(request, model);
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if (webResult.StatusCode == HttpStatusCode.Created)
            {
                var modelResponse = JsonConvert.DeserializeObject<SupscriptionModel>(webResult.JsonResponse);

                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
                result.UserMessage = "Subscripción creada satisfactoriamente.";
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        public async Task<PaymentRequestTracker<SupscriptionModel>> GetSubscription(string id)
        {
            var result = new PaymentRequestTracker<SupscriptionModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Get, CulqiHelpers.URL.GETSUBSCRIPTION(id));
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if (webResult.StatusCode == HttpStatusCode.OK)
            {
                var modelResponse = JsonConvert.DeserializeObject<SupscriptionModel>(webResult.JsonResponse);

                result.StatusCode = webResult.StatusCode;
                result.ModelResponse = modelResponse;
                result.Id = modelResponse.Id;
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }

        public async Task<PaymentRequestTracker<SupscriptionModel>> CancelSubscription(string id)
        {
            var result = new PaymentRequestTracker<SupscriptionModel>();
            var request = _paymentRequestManager.CreateWebRequest(HttpMethod.Delete, CulqiHelpers.URL.CANCELSUBSCRIPTION(id));
            var webResult = await _paymentRequestManager.GetWebRequestResponse(request);

            if (webResult.StatusCode == HttpStatusCode.OK)
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = "Subscripción cancelada satisfactoriamente.";
            }
            else
            {
                result.StatusCode = webResult.StatusCode;
                result.UserMessage = webResult.ErrorResponse;
            }

            return result;
        }
    }
}
