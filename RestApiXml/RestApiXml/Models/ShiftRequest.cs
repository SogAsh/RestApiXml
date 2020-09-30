using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Rest_api_test_xml_1._1.Models
{
    public class ShiftRequest
    {
        [XmlRoot(ElementName = "FIELD")]
        public class FIELD
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "RECORD")]
        public class RECORD
        {
            [XmlElement(ElementName = "FIELD")]
            public FIELD FIELD { get; set; }
            [XmlAttribute(AttributeName = "err")]
            public string Err { get; set; }
            [XmlAttribute(AttributeName = "dbid")]
            public string Dbid { get; set; }
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
        }

        [XmlRoot(ElementName = "answer")]
        public class Answer
        {
            [XmlElement(ElementName = "RECORD")]
            public RECORD RECORD { get; set; }
            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
        }

        [XmlRoot(ElementName = "KRECEPT")]
        public class KRECEPT
        {
            [XmlElement(ElementName = "answer")]
            public Answer Answer { get; set; }
        }
    }
}