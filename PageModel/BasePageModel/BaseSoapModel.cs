
namespace MochaHomeAccounting.PageModel.BasePageModel
{
    using System.Net;
    using log4net;
    using Newtonsoft.Json;
    using MochaHomeAccounting.Utilities;

    /// <summary>
    /// Base implementation for creating SOAP API Models.
    /// </summary>
    public class BaseSoapModel : BaseModel
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSoapModel"/> class.
        /// </summary>
        /// <param name="baseTextContext">Object of type BaseTestContext.</param>
        public BaseSoapModel(BaseTestContext baseTextContext)
            : base(baseTextContext)
        {
        }

        /// <summary>
        /// Gets or sets the Base URL value.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the SOAP Request object.
        /// </summary>
        public HttpWebRequest SoapRequest { get; set; }

        /// <summary>
        /// Gets or sets the SOAP Response object.
        /// </summary>
        public HttpWebResponse SoapResponse { get; set; }

        /// <summary>
        /// Gets or sets the ResponseString object.
        /// </summary>
        public string ResponseString { get; set; }

        /// <summary>
        /// Gets or sets the SOAPAction object.
        /// </summary>
        public string SoapAction { get; set; }

        /// <summary>
        /// Virtual method for the implementation to Get Response object.
        /// </summary>
        public virtual void GetResponseObject()
        {
        }

        /// <summary>
        /// Base Implementation for SendRequest for Send Request method.
        /// </summary>
        /// <param name="requestModel">ObjectModel to be used for the Request Model.</param>
        /// <param name="soapAction">SOAP action to be performed.</param>
        /// <param name="method">HTTP Method to be performed.</param>
        public void SendRequest(object requestModel, string soapAction, MethodAccessException method)
        {
            try
            {
                this.SoapRequest = SoapAPIHelperLibrary.GetSOAPRequest(this.BaseUrl, requestModel, soapAction, method);
                this.SoapResponse = SoapAPIHelperLibrary.GetSoapRespone(this.SoapRequest);

                this.ResponseString = SoapAPIHelperLibrary.GetResponseAsString(this.SoapResponse);
                string responseCode = this.ResponseString.Split('~')[0];
                this.ResponseString = this.ResponseString.Split('~')[1];

                if (responseCode.Equals("Success"))
                {
                    this.LogPassXMLBlock(Log, "Send Request Action is Successfull", this.ResponseString);
                }
                else
                {
                    this.LogFailXMLBlock(Log, "Send Request Action has Failed", this.ResponseString);
                }
            }
            catch (Exception ex)
            {
                this.LogFailureMessage(Log, ex.ToString());
            }
            finally
            {
                this.LogAPIData();
            }
        }

        /// <summary>
        /// Log the API Request & Response data.
        /// </summary>
        private void LogAPIData()
        {
            this.LogInfo(Log, string.Format("Request Complete, Request: {0}, Response: {1}", JsonConvert.SerializeObject(this.SoapRequest, Formatting.Indented), JsonConvert.SerializeObject(this.SoapResponse, Formatting.Indented)));
        }
    }
}
