using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonOrderbyTool.Helper
{
    internal static class FileHelper
    {
        public static JObject GetFileContent(string path)
        {
            StreamReader sr = new StreamReader(path);
            string str = "";
            while (!sr.EndOfStream)
            {
                str += sr.ReadLine();
            }
            JObject json = JObject.Parse(str);
            return json;
        }

        public static void OutputContent(string path, string content)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(content);
            }
        }
    }
}
