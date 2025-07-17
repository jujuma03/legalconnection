using LC.CORE.Helpers;
using LC.WEB.Services.Hangfire.Interfaces;
using LC.WEB.Services.Hangfire.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LC.WEB.Services.Hangfire.Implementations
{
    public class HangfireService : IHangfireService
    {
        private const string CREATE_LEGAL_CASE_TASKS = "api/delayedtask/legalcase/create";
        private const string CREATE_DIRECT_LEGAL_CASE_TASKS = "api/delayedtask/legalcase/createdirect";
        private const string EXECUTE_LEGAL_CASE_TASK = "api/delayedtask/legalcase/execute";

        public async Task CreateLegalCaseDelayedTask(CreateLegalCaseTask model)
        {
            await PostAsync(model, CREATE_LEGAL_CASE_TASKS);
        }

        public async Task CreateDirectLegalCaseDelayedTask(CreateLegalCaseTask model)
        {
            await PostAsync(model, CREATE_DIRECT_LEGAL_CASE_TASKS);
        }

        public async Task ExecuteLEgalCaseDelayedTask(ExecuteLegalCaseTask model)
        {
            await PostAsync(model, EXECUTE_LEGAL_CASE_TASK);
        }

        #region Helpers
        private static async Task PostAsync<T>(T data, string uri)
        {
            try
            {
                //var pl = GetToken();
                //var token = pl is null ? "" : pl.Token;
                using (var client = new HttpClient { BaseAddress = new Uri(ConstantHelpers.GENERAL.HANGFIRE_URI_BASE) })
                {

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var serializedEntity = JsonConvert.SerializeObject(data);
                    var content = new StringContent(serializedEntity, Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync(uri, content))
                    {
                        var serviceResponse = await response.Content.ReadAsStringAsync();

                        if (!response.IsSuccessStatusCode)
                        {
                            //Implementation to success status
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
         
        }
        public static async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                using (var client = new HttpClient { BaseAddress = new Uri(ConstantHelpers.GENERAL.HANGFIRE_URI_BASE) })
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var response = await client.GetAsync(uri))
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(responseString);
                    }

                }
            }
            catch (Exception)
            {
                return default;
            }
        }
        #endregion

    }
}
