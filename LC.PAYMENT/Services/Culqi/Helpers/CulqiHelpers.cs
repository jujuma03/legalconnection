using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Helpers
{
    public static class CulqiHelpers
    {
        public static class URL
        {
            private static readonly string URL_BASE = "https://api.culqi.com/v2";

            //CHARGUES
            public static string CREATECHARGUE() => $"{URL_BASE}/charges";
            public static string GETCHARGUE(string id) => $"{URL_BASE}/charges/{id}";

            //CUSTOMER
            public static string CREATECUSTOMER() => $"{URL_BASE}/customers";
            public static string GETCUSTOMER(string id) => $"{URL_BASE}/customers/{id}";
            public static string DELETECUSTOMER(string id) => $"{URL_BASE}/customers/{id}";


            //CARD
            public static string CREATECARD() => $"{URL_BASE}/cards";
            public static string GETCARD(string id) => $"{URL_BASE}/cards/{id}";
            public static string DELETECARD (string id) => $"{URL_BASE}/cards/{id}";

            //PLAN
            public static string CREATEPLAN() => $"{URL_BASE}/plans";
            public static string GETPLAN(string id) => $"{URL_BASE}/plans/{id}";
            public static string DELETEPLAN(string id) => $"{URL_BASE}/plans/{id}";
            public static string GETLIST() => $"{URL_BASE}/plans";

            //SUBSCRIPTIONS
            public static string CREATESUBSCRIPTION() => $"{URL_BASE}/subscriptions";
            public static string GETSUBSCRIPTION(string id) => $"{URL_BASE}/subscriptions/{id}";
            public static string CANCELSUBSCRIPTION(string id) => $"{URL_BASE}/subscriptions/{id}";


        }
    }
}
