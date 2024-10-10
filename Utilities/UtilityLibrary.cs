namespace MochaHomeAccounting.Utilities
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using MochaHomeAccounting.Utilities.TestDataModels;

    /// <summary>
    /// Utility functions for serializing and deserializing JSON objects.
    /// </summary>
    public class UtilityLibrary
    {
        /// <summary>
        /// Deserilize object from JSON string.
        /// </summary>
        /// <typeparam name="T">Generic return type T.</typeparam>
        /// <param name="responseString">String to be deserialized.</param>
        /// <returns></returns>
        public static T GetDeSerializedObject<T>(string responseString)
        {
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <param name="obj">Object to be serialized.</param>
        /// <returns>Serialized JSON Object.</returns>
        public static string GetSerializedString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Serializes the specified object to XML string.
        /// </summary>
        /// <param name="envelope">Object to be serialized.</param>
        /// <returns>Serialized XML string.</returns>
        public static string GetSerializedXMLString(object envelope)
        {
            try
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("soapenv", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("v2", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("v4", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("arr", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("xsi", "https://schemas.xmlsoap.org/soap/envelope/");
                ns.Add("xsd", "https://schemas.xmlsoap.org/soap/envelope/");

                XmlSerializer xmlSerializer = new (envelope.GetType());

                var settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                string xmlString;

                using (var stringWriter = new StringWriter())
                {
                    using XmlWriter writer = XmlWriter.Create(stringWriter, settings);
                    xmlSerializer.Serialize(writer, envelope, ns);
                    xmlString = stringWriter.ToString();
                }

                return xmlString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Retrieve test data from JSON file.
        /// </summary>
        /// <param name="fileName">Name of the file from which data needs to be retrieved, without the file extension.</param>
        /// <returns>JObject containing data retrieved from the JSON file.</returns>
        public static string GetJsonObjectOfTestFile(string fileName)
        {
            string testJsonFileName = Path.Combine(Directory.GetCurrentDirectory(), "TestData", fileName + ".json");
            return File.ReadAllText(testJsonFileName);
        }

        /// <summary>
        /// Retrieve customer data from the specified JSON file and deserialize the data into object for use.
        /// </summary>
        /// <param name="fileName">Name of the JSON file to be read.</param>
        /// <returns>Object of Type BaseCustomerData populated with data.</returns>
        public static BaseCustomerData GetCustomerData(string fileName)
        {
            var contentRetrievedFromFile = GetJsonObjectOfTestFile(fileName);
            return CreateCustomerDataFromJson(contentRetrievedFromFile);
        }

        /// <summary>
        /// Deserialize the provided JSON string into Customer Data object for consumption in the tests.
        /// </summary>
        /// <param name="testDataFileContent">String content retrieved from the test data file.</param>
        /// <returns>Deserialized object of BaseCustomerData.</returns>
        private static BaseCustomerData CreateCustomerDataFromJson(string testDataFileContent)
        {
            BaseCustomerData customerData = GetDeSerializedObject<BaseCustomerData>(testDataFileContent);
            return customerData;
        }
    }
}
