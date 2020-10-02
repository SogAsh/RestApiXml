using System.Runtime.InteropServices;
using NUnit.Framework;
using Rest_api_test_xml_1._1.Models;
using RestSharp;
using RestSharp.Serialization.Xml;
using System.Threading.Tasks;
using System;

namespace Rest_api_test_xml_1._1

{
    [TestFixture]
    public partial class Test
    {
        public static string CompanyId;
        public static string CompanyName;

        public static string DepartmentName;
        public static string DepartmentId;

        public static string ShiftName;
        public static string ShiftDdid;
        public static string ShiftId;

        //public static RestClient client = new RestClient("https://localhost:60004/");
        //public static RestRequest request = new RestRequest("1c", Method.POST);

        [SetUp]
        public async Task SetUp()
        {
            BaseMethods.DisableCheckCertificate();
            //request.AddHeader("Accept", "application/xml");
            //request.RequestFormat = DataFormat.Xml;
        }

        [Test]
        public async Task RequestCompany()
        {
            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '6'>" +
                            "<RECORD operation = '0'/>" +
                            "</REQUEST>" +
                            "</KRECEPT>";
            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            string actualtext = GetObject.GetCompany(name);
            Assert.AreEqual(name, actualtext, "expected is not equal than actual");
        }

        [Test]
        public async Task AddCompany()
        {
            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;
            
            BaseData randomShift = new BaseData();
            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '6'>" +
                            "<RECORD operation = '1' name = '" + randomShift.word + "'/>"+
                            "</REQUEST>"+
                            "</KRECEPT>";
            
            CompanyName = randomShift.word;

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;
            Assert.AreEqual(BaseData.FIELDname, name, "insert success");

            var companyId = deserializer.Deserialize<ShiftRequest.RECORD>(response);
            CompanyId = companyId.Id;

            string actualtext = GetObject.GetCompany(randomShift.word);
            Assert.AreEqual(randomShift.word, actualtext, "expected is not equal than actual");
        }

        [Test]
        public async Task ChangeCompany()
        {
            AddCompany();

            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            BaseData randomShift = new BaseData();
            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '6'>" +
                            "<RECORD operation = '2' id = '" + CompanyId + "' name = '" + randomShift.word + "'/>" +
                            "</REQUEST>" +
                            "</KRECEPT>";

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;
            Assert.AreEqual(BaseData.FIELDname, name, "insert success");

            var result2 = deserializer.Deserialize<ShiftRequest.RECORD>(response);

            string actualtext = GetObject.GetCompany(randomShift.word);
            Assert.AreEqual(randomShift.word, actualtext, "expected is not equal than actual");
        }
        
        [Test]
        public async Task DeleteCompany()
        {
            AddCompany();

            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            BaseData randomShift = new BaseData();
            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '6'>" +
                            "<RECORD operation = '3' id = '" + CompanyId + "'/>" +
                            "</REQUEST>" +
                            "</KRECEPT>";

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;
            Assert.AreEqual(BaseData.FIELDname, name, "insert success");

            string actualtext = GetObject.GetDeletedCompany(CompanyName);
            Assert.AreNotEqual(CompanyName, actualtext, "expected is not equal than actual");
        }

        [Test]
        public async Task RequestDepartment()
        {
            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '8'>" +
                            "<RECORD operation = '0'/>" +
                            "</REQUEST>" +
                            "</KRECEPT>";
            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            string actualtext = GetObject.GetDepartment(name);
            Assert.AreEqual(name, actualtext, "expected is not equal than actual");
        }

        [Test]
        public async Task AddDepartment()
        {
            AddCompany();

            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            BaseData randomShift = new BaseData();
            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '8'>" +
                            "<RECORD operation = '1' name = '" + randomShift.word + "' org_id = '" + CompanyId +"'/>" +
                            "</REQUEST>" +
                            "</KRECEPT>";
                
            DepartmentName = randomShift.word;

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;
            Assert.AreEqual(BaseData.FIELDname, name, "insert success");

            var companyId = deserializer.Deserialize<ShiftRequest.RECORD>(response);
            DepartmentId = companyId.Id;

            string actualtext = GetObject.GetCompany(randomShift.word);
            Assert.AreEqual(randomShift.word, actualtext, "expected is not equal than actual");
        }

        //--------------------------------------------------------------------------------------------------
        [Test]
        public async Task AddShift()
        {
            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            BaseData randomShift = new BaseData();

            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '23'>" +
                            "<RECORD operation = '1' name = '"
                            + randomShift.word + "'/>" +
                            "</REQUEST></KRECEPT>";

            ShiftName = randomShift.word;

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            var resultNext = deserializer.Deserialize<ShiftRequest.RECORD>(response);
            ShiftDdid = resultNext.Dbid;
            ShiftId = resultNext.Id;

            Assert.AreEqual(BaseData.FIELDname, name, "insert success");

            string actualtext = GetObject.GetShift(randomShift.word);
            Assert.AreEqual(randomShift.word, actualtext, "expected is not equal than actual");
        }

        [Test]
        public async Task RequestShift()
        {
            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            AddShift();

            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '23'>" +
                            "<RECORD operation = '0'/>" +
                            "</REQUEST>" +
                            "</KRECEPT>";
            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            string actualtext = GetObject.GetShift(name);
            Assert.AreEqual(name, actualtext, "expected is not equal than actual");
        }

        [Test]
        public async Task ChangeShift() 
        {
            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            AddShift();
            BaseData randomShift = new BaseData();
            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '23'>" +
                            "<RECORD operation = '2' id = '" + ShiftId + "' name = '" + randomShift.word + "'/>" +
                            "</REQUEST>" +
                            "</KRECEPT>";
            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            Assert.AreEqual(BaseData.FIELDname, name, "insert success");

            string actualtext = GetObject.GetShift(randomShift.word);
            Assert.AreEqual(randomShift.word, actualtext, "expected is not equal than actual");
        }
        
        [Test]
        public async Task DeleteShift()
        {
            var client = new RestClient("https://localhost:60004/");
            var request = new RestRequest("1c", Method.POST);

            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            AddShift();

            string rawXml = "<KRECEPT>" +
                            "<REQUEST type = '23'>" +
                            "<RECORD operation = '3' id = '" + ShiftId + "'/>" +
                            "</REQUEST>" +
                            "</KRECEPT>";
            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            Assert.AreEqual(BaseData.FIELDname, name, "insert success");

            string actualtext = GetObject.GetDeletedShift(ShiftName);
            Assert.AreNotEqual(ShiftName, actualtext, "expected is not equal than actual");
        }

    }
}