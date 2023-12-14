using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonOrderbyTool
{
    internal static class Helper
    {
        public static JObject ReadPath(string path)
        {
            JObject result = new JObject();
            using (StreamReader sr = new StreamReader(path))
            {
                string str = "";
                while (!sr.EndOfStream)
                {
                    str += sr.ReadLine();
                }
                result = JObject.Parse(str);
            }
            return result;
        }

        public static JObject OutputContrastfriendlyJson(JObject json)
        {
            JObject newJson = new JObject();
            JObject paths;
            JObject definitions;

            foreach (var item in json.Properties())
            {
                if (item.Name == "paths" || item.Name == "definitions")
                {
                    JObject temp = (JObject)item.First();
                    temp.Sort();
                    newJson.Add(temp);
                }
            }
            return newJson;
        }

        public static void Sort(this JObject json)
        {
            JObject orderedJson = new JObject();
            var orderedNames = json.Properties().Select(item => item.Name).OrderBy(n => n);
            foreach (var name in orderedNames)
            {
                orderedJson.Add(json.Properties().Where(item => item.Name == name));
            }
            json = orderedJson;
        }
    }
}
