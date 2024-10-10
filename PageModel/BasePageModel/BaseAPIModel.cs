
namespace MochaHomeAccounting.PageModel.BasePageModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using RestSharp;
    using MochaHomeAccounting.Enums;
    using MochaHomeAccounting.Utilities;

    /// <summary>
    /// Base implementation for creating API Models.
    /// </summary>
    public abstract class BaseApiModel : BaseModel
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiModel"/> class.
        /// </summary>
        /// <param name="baseTextContext">Object of type BaseTestContext.</param>
        protected BaseApiModel(BaseTestContext baseTextContext)
            : base(baseTextContext)
        {
        }

        /// <summary>
        /// Gets or Sets the RestClient object.
        /// </summary>
        public RestClient RestClient { get; set; }

        /// <summary>
        /// Gets or Sets the RestRequest object.
        /// </summary>
        public RestRequest RestRequest { get; set; }

        /// <summary>
        /// Gets or Sets the RestResponse object.
        /// </summary>
        public RestResponse RestResponse { get; set; }

        /// <summary>
        /// Gets or Sets the EndPointUrl value.
        /// </summary>
        public string EndPointUrl { get; set; }

        /// <summary>
        /// Gets or Sets the OAuthToken value.
        /// </summary>
        public string OAuthToken { get; set; }

        /// <summary>
        ///  Gets or Sets the MessageID value.
        /// </summary>
        public string MessageID { get; set; }

        /// <summary>
        /// Gets or Sets the ObjectModel object.
        /// </summary>
        public Object ObjectModel { get; set; }

        /// <summary>
        /// Gets or Sets the API Key value.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or Sets the Description value.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Base Implementation for SendRequest for Send Request method.
        /// </summary>
        /// <param name="objectModel">ObjectModel to be used for the Request Model.</param>
        /// <param name="method">HTTP Method to be performed.</param>
        /// <param name="sessionTokenId">Session Id for the request.</param>
        public void SendRequest(object objectModel, Method method, string sessionTokenId = null)
        {
            try
            {
                this.RestRequest = RestAPIHelperLibrary.GetRestRequest(this.EndPointUrl, method, this.OAuthToken, this.ApiKey, this.MessageID.Trim(), objectModel, sessionTokenId);
                this.RestResponse = RestAPIHelperLibrary.GetRestResponse(this.RestRequest, method);
                this.LogAPIData(this.RestRequest, this.RestResponse);
                this.LogInfoMessage(Log, this.RestResponse.Content);
            }
            catch (Exception ex)
            {
                this.LogFailureMessage(Log, ex.ToString());
            }
        }

        /// <summary>
        /// Validate the response code equals to 201.
        /// </summary>
        /// <param name="responseStatusCode">Value of the response code to be validated.</param>
        public virtual void ValidateResponseStatus(string responseStatusCode)
        {
            Assert.That(responseStatusCode, Is.True, APIStatusCode.Created.ToString());
        }

        /// <summary>
        /// Validate the description value received as a part of the response.
        /// </summary>
        /// <param name="actualString">Value of the actual response description received.</param>
        public virtual void ValidateResponseDescription(string actualString)
        {
            Assert.That(this.Description, Is.True, actualString);
            this.LogSuccessMessage(Log, "Excepted Description :" + this.Description + " Actual Description :" + actualString);
        }

        /// <summary>
        /// Validate the headers value received as a part of the response.
        /// </summary>
        /// <param name="response">Response object containing the headers to be validated.</param>
        /// <param name="baseTestContext">Instance of the BaseTestConext Object.</param>
        public void ValidateResponseHeader(RestResponse response, BaseTestContext baseTestContext)
        {
            Dictionary<string, string> headersToValidate = new Dictionary<string, string>();
            headersToValidate.Add("cache-control", "no-store, no-cache");

            var headers = this.RestResponse.Headers;

            this.LogInfoMessage(Log, "******************* Validate Response Headers *********************");
            SoftAssert softAssert = new SoftAssert(baseTestContext);

            foreach (string key in headersToValidate.Keys)
            {
                string expectedValue = headersToValidate[key];
                string actualValue = (from header in headers
                                      where header.Name == key
                                      select header.Value.ToString()).FirstOrDefault();

                softAssert.AreEqual(key, expectedValue, actualValue);
            }

            softAssert.AssertAll();
        }

        /// <summary>
        /// Log the API Request & Response data.
        /// </summary>
        /// <param name="request">Rest Request object to tbe logged.</param>
        /// <param name="response">Rest Response object to be logged.</param>
        private void LogAPIData(RestRequest request, RestResponse response)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString(),
                }),
                method = request.Method.ToString(),
            };

            var responseToLog = new
            {
                statusCode = response.StatusCode,
                content = response.Content,
                headers = response.Headers.Select(header => new
                {
                    name = header.Name,
                    value = header.Value,
                    type = header.Type.ToString(),
                }),
                responseUri = response.ResponseUri,
                errorMessage = response.ErrorMessage,
            };

            this.LogInfo(Log, string.Format("Request completed, Request: {0}, Response:{1}", UtilityLibrary.GetSerializedString(requestToLog), UtilityLibrary.GetSerializedString(responseToLog)));
        }
    }
}
