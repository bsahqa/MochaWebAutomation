namespace MochaHomeAccounting.Utilities
{
    using System.Net;
    using log4net;
    using RestSharp;

    /// <summary>
    /// Helper class containing the implementation for the executing common operations required for running REST API's.
    /// </summary>
    public class RestAPIHelperLibrary
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the Authentication Token.
        /// </summary>
        public static object AuthToken { get; private set; }

        /// <summary>
        /// Create a RestRequest object with the specified details.
        /// </summary>
        /// <param name="endPointUrl">URL against which the request is to be executed.</param>
        /// <param name="method">HTTP Action to be performed.</param>
        /// <param name="authToken">Authentication Token to be used for execution.</param>
        /// <param name="apiKey">API Key required for executing the request.</param>
        /// <param name="messageID">Message Id if any required to be sent with the request.</param>
        /// <param name="objBody">Request body object.</param>
        /// <param name="sessionId">Session Id for the request, if required by the API.</param>
        /// <returns>Object of the type RestRequest built using specified details.</returns>
        public static RestRequest GetRestRequest(string endPointUrl, Method method, string authToken, string apiKey, string messageID, object objBody, string sessionId = "null")
        {
            RestRequest restRequest = new RestRequest(endPointUrl, method);
            restRequest.AddHeader("Authorization", authToken);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("x-APIKey", apiKey);
            restRequest.AddHeader("x-RequestTimeStamp", DateTime.Now.ToString("dd/mm/yyyy"));
            if (objBody != null)
            {
                restRequest.AddJsonBody(objBody, "application/json");
            }

            return restRequest;
        }

        /// <summary>
        /// Helper method for executing HTTP Post Requests.
        /// </summary>
        /// <param name="restRequest">RestRequest object containing the details of the request to be executed.</param>
        /// <returns>RestResponse object containing the response of the executed request.</returns>
        public static RestResponse PostRequest(RestRequest restRequest)
        {
            RestClient restClient = new RestClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return restClient.Post(restRequest);
        }

        /// <summary>
        /// Helper method for executing HTTP Delete Requests.
        /// </summary>
        /// <param name="restRequest">RestRequest object containing the details of the request to be executed.</param>
        /// <returns>RestResponse object containing the response of the executed request.</returns>
        public static RestResponse DeleteRequest(RestRequest restRequest)
        {
            RestClient restClient = new RestClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return restClient.Delete(restRequest);
        }

        /// <summary>
        /// Helper method for executing HTTP Put Requests.
        /// </summary>
        /// <param name="restRequest">RestRequest object containing the details of the request to be executed.</param>
        /// <returns>RestResponse object containing the response of the executed request.</returns>
        public static RestResponse PutRequest(RestRequest restRequest)
        {
            RestClient restClient = new RestClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return restClient.Put(restRequest);
        }

        /// <summary>
        /// Helper method for executing HTTP Get Requests.
        /// </summary>
        /// <param name="restRequest">RestRequest object containing the details of the request to be executed.</param>
        /// <returns>RestResponse object containing the response of the executed request.</returns>
        public static RestResponse GetRequest(RestRequest restRequest)
        {
            RestClient restClient = new RestClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return restClient.Get(restRequest);
        }

        /// <summary>
        /// Common helper method for executing various types of HTTP Requests & retrieving their responses.
        /// </summary>
        /// <param name="restRequest">RestRequest object containing the details of the request to be executed.</param>
        /// <param name="method">Type of the HTTP Method to be executed</param>
        /// <returns>RestResponse object containing the response of the executed request.</returns>
        public static RestResponse GetRestResponse(RestRequest restRequest, Method method)
        {
            return method switch
            {
                Method.Post => PostRequest(restRequest),
                Method.Get => GetRequest(restRequest),
                Method.Put => PutRequest(restRequest),
                Method.Delete => DeleteRequest(restRequest),
                _ => null,
            };
        }

        /// <summary>
        /// Retrieve OAuth Token if required to be used for authentication purposes.
        /// </summary>
        /// <returns>String containing the Authentication token.</returns>
        public static string GetOAuthToken()
        {
            RestClient client = new RestClient("AuthTokenUrl");
            RestRequest request = new RestRequest() { Method = Method.Post };
            request.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);
            request.AddParameter("Client_id", "AuthClientID", ParameterType.GetOrPost);
            request.AddParameter("Client_secret", "AuthSecretKey", ParameterType.GetOrPost);
            object authToken = UtilityLibrary.GetDeSerializedObject<object>(client.Execute(request).Content);
            return authToken.ToString();
        }
    }
}