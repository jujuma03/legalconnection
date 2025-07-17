using LC.PAYMENT.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LC.PAYMENT.Utilities.RequestManager
{
    public interface IPaymentRequestManager
    {
        HttpWebRequest CreateWebRequest(HttpMethod httpMethod, string url);
        void LoadRequestBody(HttpWebRequest webRequest, object model);
        Task<WebRequestResult> GetWebRequestResponse(HttpWebRequest request);
    }
}
