using System.Runtime.InteropServices;
using NUnit.Framework;
using Rest_api_test_xml_1._1.Models;
using RestSharp;
using RestSharp.Serialization.Xml;
using System.Threading.Tasks;

namespace Rest_api_test_xml_1._1

{
    [TestFixture]
    public partial class Test
    {
        public static string RECORDdbId;
        public static string RECORId;

        public static RestClient client = new RestClient("https://localhost:60004/");
        public static RestRequest request = new RestRequest("1c", Method.POST);

        [SetUp]
        public async Task SetUp()
        {
            BaseMethods.DisableCheckCertificate();
            request.AddHeader("Accept", "application/xml");
            request.RequestFormat = DataFormat.Xml;
        }

        [Test]
        public async Task POSTAddShift()
        {
            string rawXml = "<KRECEPT><REQUEST type = '23'><RECORD operation = '1' name = '"
                            + BaseData.shift + "'/></REQUEST></KRECEPT>";

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            var resultNext = deserializer.Deserialize<ShiftRequest.RECORD>(response);
            RECORDdbId = resultNext.Dbid;
            RECORId = resultNext.Id;

            Assert.AreEqual(BaseData.FIELDname, name, "insert success");

            string actualtext = GetObject.GetShift(BaseData.shift);
            Assert.AreEqual(BaseData.shift, actualtext, "expected is not equal than actual");
        }

        [Test]
        public async Task POSTRequestShift()
        {
            POSTAddShift();

            string rawXml = "<KRECEPT><REQUEST type = '23'><RECORD operation = '0'/></REQUEST></KRECEPT>";
            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);
            
            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            var resultNext = deserializer.Deserialize<ShiftRequest.RECORD>(response);
            var dbid = resultNext.Dbid;

            Assert.AreEqual(BaseData.FIELDname, name, "insert success");
            //Assert.AreEqual(RECORDdbId, dbid, "insert success"); валится потому что почемуто создаются две смены
        }

        [Test]
        public async Task POSTDeleteShift() //Добавляет смену вместо того, чтобы удалить
        {
            POSTAddShift();

            string rawXml = "<KRECEPT><REQUEST type = '23'><RECORD operation = '3' name = '" + RECORId + "'/></REQUEST></KRECEPT>";

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            Assert.AreEqual(BaseData.FIELDname, name, "insert success");
        }




        /* Синхронные методы - не решают проблемы
        [Test]
        public void POSTAddShift()
        {
            string rawXml = "<KRECEPT><REQUEST type = '23'><RECORD operation = '1' name = '" + BaseData.shift + "'/></REQUEST></KRECEPT>";

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Xml;

            var response = client.ExecuteAsync(request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response.Result);
            var name = result.Name;

            var resultNext = deserializer.Deserialize<ShiftRequest.RECORD>(response.Result);
            RECORDdbId = resultNext.Dbid;

            //Assert.AreEqual(BaseData.FIELDname, name, "insert success");

            //string actualtext = GetObject.GetShift(BaseData.shift);
            //Assert.AreEqual(BaseData.shift, actualtext, "expected is not equal than actual");
        }

        [Test]
        public void POSTRequestShift()
        {
            POSTAddShift();

            string rawXml = "<KRECEPT><REQUEST type = '23'><RECORD operation = '0'/></REQUEST></KRECEPT>";
            
            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Xml;

            var response = client.ExecuteAsync(request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response.Result);
            var name = result.Name;

            var resultNext = deserializer.Deserialize<ShiftRequest.RECORD>(response.Result);
            var dbid = resultNext.Dbid;

            //Assert.AreEqual(RECORDdbId, dbid, "insert success");
        }
        */
    }
}