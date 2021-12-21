using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTests
{
    public class HttpClientTests
    {
        [Test]
        public async Task HttpClientTest()
        {
            HttpClient client = new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });
            var url = new Uri("https://www.baidu.com");
            var message = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(message);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content);
            Console.WriteLine(response);
        }
    }
}
