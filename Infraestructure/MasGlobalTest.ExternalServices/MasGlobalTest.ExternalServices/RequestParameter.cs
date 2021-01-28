using System.Net.Http;

namespace MasGlobalTest.ExternalServices
{
    public class RequestParameter
    {
        public HttpMethod HttpVerb { get; set; }
        public string EndpointUrl { get; set; }

        public RequestParameter(HttpMethod httpVerb, string endpointUrl)
        {
            HttpVerb = httpVerb;
            EndpointUrl = endpointUrl;
        }
    }
}
