using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LC.PAYMENT.Models
{
    public class WebRequestResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string JsonResponse { get; set; }
        public string ErrorResponse { get; set; }
    }
}
