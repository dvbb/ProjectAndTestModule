using Newtonsoft.Json;
using System;
using System.IO;

namespace YamlTranslator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string resourcefile = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\JsonStr2.txt");
            StreamReader sr = new StreamReader(resourcefile);
            string str = "";
            while (!sr.EndOfStream)
            {
                str += sr.ReadLine();
            }
            Console.WriteLine(str);
            //var json = JsonConvert.SerializeObject(str);
            //Console.WriteLine();
            //Console.WriteLine(json);
        }
    }
}
