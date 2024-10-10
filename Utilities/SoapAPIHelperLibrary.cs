namespace MochaHomeAccounting.Utilities
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Xml;
    using System.Xml.Linq;
    using log4net;

    /// <summary>
    /// Helper class containing the implementation for the executing common operations required for running SOAP API's.
    /// </summary>
    public class SoapAPIHelperLibrary
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Create a SOAPRequest object with the specified details.
        /// </summary>
        /// <param name="baseURL">Base URL of the endpoint against which the request is to be executed.</param>
        /// <param name="requestModel">RequestBody Content.</param>
        /// <param name="soapAction">SOAP Action to be performed.</param>
        /// <param name="method">HTTP Action to be performed.</param>
        /// <returns>Object of the type HttpWebRequest built using specified details.</returns>
        public static HttpWebRequest GetSOAPRequest(string baseURL, object requestModel, string soapAction, MethodAccessException method)
        {
            XmlDocument reqBody = new XmlDocument();
            reqBody.LoadXml(UtilityLibrary.GetSerializedXMLString(requestModel));
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL);
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.ContentType = "text/xml;charset=UTF-8";
            request.Headers.Add("SOAPAction", soapAction);
            request.Accept = "text/xml";
            request.Method = method.ToString();

            using (Stream stream = request.GetRequestStream())
            {
                reqBody.Save(stream);
            }

            return request;
        }

        /// <summary>
        /// Retrieve the Response for the executed SOAP Request.
        /// </summary>
        /// <param name="request">HTTP WebRequest for which the response is to be retrieved.</param>
        /// <returns>Object of the type HttpWebResponse containing the response for the executed request.</returns>
        public static HttpWebResponse GetSoapRespone(HttpWebRequest request)
        {
            return (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// Parse the provided HTTPWebResponseObject and return it in a string format.
        /// </summary>
        /// <param name="response">HTTPWebResponse objec to be parsed & converted into string.</param>
        /// <returns>String containing the HTTPWebResponse in a string format.</returns>
        internal static string GetResponseAsString(HttpWebResponse response)
        {
            string responseText;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseText = reader.ReadToEnd();
            }

            try
            {
                XDocument document = XDocument.Parse(responseText);
                var nodes = document.Descendants();
                string responseCode = (from node in nodes
                                       where node.Name.LocalName == "ResponseCode"
                                       select node).FirstOrDefault().Value;

                return responseCode + "~" + document.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message + responseText;
            }
        }
    }
}
