using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTests
{
    public class HttpClientTests
    {
        private Uri _uri = new Uri("https://www.baidu.com");

        [Test]
        public async Task HttpClientBasicTest()
        {
            HttpClient client = new HttpClient();
            var message = new HttpRequestMessage(HttpMethod.Get, _uri);
            var response = await client.SendAsync(message);
            Console.WriteLine(response.StatusCode);

            string content = await response.Content.ReadAsStringAsync();//JObject json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine(content);
        }

        [Test]
        public async Task HttpClientHandlerTest()
        {
            HttpClient client = new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });
            var message = new HttpRequestMessage(HttpMethod.Get, _uri);
            var response = await client.SendAsync(message);
            Console.WriteLine(response.StatusCode);
        }
    }
}
