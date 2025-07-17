using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LC.PAYMENT.Models
{
    public class PaymentRequestTracker<T> where T : class
    {
        public string Id { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string UserMessage { get; set; }
        public T ModelResponse { get; set; }
    }
}
