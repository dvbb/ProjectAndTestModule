using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTests
{
    public class WebClientTests
    {
        [Test]
        public void GetImagesTest()
        {
            string filePath = "d:\\haha.jpg";
            Console.WriteLine($"{filePath} does exists?  "+File.Exists(filePath));

            Uri uri = new Uri("http://www.baidu.com/img/flexible/logo/pc/result.png");
            WebClient webClient = new WebClient();
            webClient.DownloadFile(uri, filePath);
            Console.WriteLine("success");
            Console.WriteLine($"{filePath} does exists?  "+File.Exists(filePath));

            File.Delete(filePath);
        }
    }
}
