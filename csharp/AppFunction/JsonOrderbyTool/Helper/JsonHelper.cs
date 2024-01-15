using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonOrderbyTool.Helper
{
    internal static class JsonHelper
    {
        public static JObject Sort(JObject json)
        {
            JObject orderedJson = new JObject();
            var orderedNames = json.Properties().Select(item => item.Name).OrderBy(n => n);
            foreach (var name in orderedNames)
            {
                orderedJson.Add(json.Properties().Where(item => item.Name == name));
            }
            return orderedJson;
        }
    }
}
