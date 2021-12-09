using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace WebArticle.Utilities
{
    public static class HttpHelper
    {
        public static string HttpGet(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    return httpClient.GetStringAsync(url).Result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string HttpPost(string requestUrl, Dictionary<string, string> pairs)
        {
            string str = JsonConvert.SerializeObject(pairs);
            var content = new StringContent(str);
            using (HttpClient client = new HttpClient())
            {
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                var response = client.PostAsync(requestUrl, content).Result;
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    return null;
                }
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}