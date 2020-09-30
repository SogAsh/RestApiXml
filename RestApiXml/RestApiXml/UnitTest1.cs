using NUnit.Framework;
using Npgsql;
using System.Collections;
using System.Data.Common;
using System.IO;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;

using RestSharp;
using RestSharp.Serialization.Json;
using RestSharp.Authenticators;
using System.Xml.Serialization;
using RestSharp.Deserializers;
using System.Xml;
using RestSharp.Serialization.Xml;

using System.Threading.Tasks;
using Rest_api_test_xml_1._1.Models;

namespace Rest_api_test_xml_1._1

{
    [TestFixture]
    public partial class Test
    {
        public static int i = 0;
        public static int time = 600;
        public static string FIELDname = "err_desc";
        public static string RECORDdbid;
        public static string shift = "Новая смена 1";

        private void Init()
        {
            // Your recording specific initialization code goes here.
        }

        [Test]
        public async Task POSTAddShift()
        {
            var client = new RestClient("https://localhost:60004/");
            BaseMethods.DisableCheckCertificate();

            var request = new RestRequest("1c", Method.POST);
            request.AddHeader("Accept", "application/xml");

            request.RequestFormat = DataFormat.Xml;

            string rawXml = "<KRECEPT><REQUEST type = '23'><RECORD operation = '1' name = '" + shift + "'/></REQUEST></KRECEPT>";

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            var resultNext = deserializer.Deserialize<ShiftRequest.RECORD>(response);
            RECORDdbid = resultNext.Dbid;

            Assert.AreEqual(FIELDname, name, "insert success");
        }

        [Test]
        public async Task POSTRequestShift()
        {
            var client = new RestClient("https://localhost:60004/");
            BaseMethods.DisableCheckCertificate();

            var request = new RestRequest("1c", Method.POST);
            request.AddHeader("Accept", "application/xml");

            request.RequestFormat = DataFormat.Xml;

            string rawXml = "<KRECEPT><REQUEST type = '23'><RECORD operation = '0'/></REQUEST></KRECEPT>";

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            POSTAddShift();
            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            var resultNext = deserializer.Deserialize<ShiftRequest.RECORD>(response);
            var dbid = resultNext.Dbid;

            Assert.AreEqual(RECORDdbid, dbid, "insert success");
        }

        [Test]
        public async Task POSTChangeShift()
        {
            var client = new RestClient("https://localhost:60004/");
            BaseMethods.DisableCheckCertificate();

            var request = new RestRequest("1c", Method.POST);
            request.AddHeader("Accept", "application/xml");

            request.RequestFormat = DataFormat.Xml;

            string rawXml = "<KRECEPT><REQUEST type = '23'><RECORD operation = '1' name = '" + shift + "'/></REQUEST></KRECEPT>";

            request.AddParameter("application/xml", rawXml, ParameterType.RequestBody);

            var response = await BaseMethods.GetResponse(client, request);

            XmlAttributeDeserializer deserializer = new XmlAttributeDeserializer();
            var result = deserializer.Deserialize<ShiftRequest.FIELD>(response);
            var name = result.Name;

            var resultNext = deserializer.Deserialize<ShiftRequest.RECORD>(response);
            RECORDdbid = resultNext.Dbid;

            Assert.AreEqual(FIELDname, name, "insert success");
        }

    }
}
