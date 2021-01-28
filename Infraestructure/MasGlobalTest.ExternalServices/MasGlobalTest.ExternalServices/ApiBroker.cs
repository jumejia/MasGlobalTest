using MasGlobalTest.Common.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MasGlobalTest.ExternalServices
{
    public class ApiBroker : IApiBroker
    {
        //private readonly IApiBroker _apiBroker;
        //public ApiBroker(IApiBroker iApiBroker) => (_apiBroker) = (iApiBroker);

        public async Task<T> GetServiceObjectResponseAsync<T>(RequestParameter requestParameter) where T : class
        {
            var response = await GetServiceHttpResponseAsync(requestParameter).ConfigureAwait(false);
            var resultContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(resultContent);
        }

        private async Task<HttpResponseMessage> GetServiceHttpResponseAsync(RequestParameter requestParameters)
        {
            var request = new HttpRequestMessage(requestParameters.HttpVerb, requestParameters.EndpointUrl);

            using HttpClient client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.SendAsync(request).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new UnavailableExternalServiceException("Something wrong happens with One Source API.");
                }

                return response;
            }
            catch (Exception exception) when (!(exception is UnavailableExternalServiceException))
            {
                throw exception;
            }
        }
    }
}
