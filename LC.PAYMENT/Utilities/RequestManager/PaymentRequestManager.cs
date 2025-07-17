using LC.PAYMENT.Models;
using LC.PAYMENT.Services.Culqi.Models;
using LC.PAYMENT.Services.Culqi.Models.Base;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LC.PAYMENT.Utilities.RequestManager
{
    public class PaymentRequestManager : IPaymentRequestManager
    {
        private readonly PaymentCredentials _paymentCredentials;

        public PaymentRequestManager(IOptions<PaymentCredentials> options)
        {
            _paymentCredentials = options.Value;
        }

        public HttpWebRequest CreateWebRequest(HttpMethod httpMethod, string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = httpMethod.ToString();
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {_paymentCredentials.PrivateKey}");
            return webRequest;
        }

        public void LoadRequestBody(HttpWebRequest webRequest, object model)
        {
            var requestBody = JsonConvert.SerializeObject(model);
            using (StreamWriter stream = new StreamWriter(webRequest.GetRequestStream()))
            {
                stream.Write(requestBody);
            }
        }

        public async Task<WebRequestResult> GetWebRequestResponse(HttpWebRequest request) 
        {
            var result = new WebRequestResult();

            try
            {
                using (var response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    result.StatusCode = response.StatusCode;
                    result.JsonResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    return result;
                }
            }
            catch (WebException ex)
            {
                using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    var streamResult = reader.ReadToEnd();
                    var model = JsonConvert.DeserializeObject<ErrorModel>(streamResult);
                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.ErrorResponse = model.MerchantMessage;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.ErrorResponse = "Ocurrió un problema al intentar conectarse con Culqi. Por favor inténtalo en unos minutos.";
            }

            return result;
        }
    }
}
