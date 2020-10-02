using System;
using System.Collections.Generic;
using System.Text;

namespace Rest_api_test_xml_1._1
{
    public class BaseData
    {
        public static string FIELDname = "err_desc";

        public static string rundomLetter = RandomNumbers();
        public static string rundomNumber = RandomLetters(5);
        public string word = "Название Name " + rundomLetter + rundomNumber + "";

        public static string RandomNumbers()
        {
            Random run = new Random();
            var result = run.Next(0, 5).ToString();
            return result;
        }

        public static string RandomLetters(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
                sb.Append((char)rnd.Next(97, 123));
            //sb.Append((char)rnd.Next(97, 123));
            return sb.ToString();
        }

    }
}
