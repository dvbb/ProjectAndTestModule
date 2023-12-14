using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace JsonOrderbyTool
{
    public class Program
    {
        string[] Verbs => new string[4] { "get", "put", "patch", "delete" };

        static void Main(string[] args)
        {
            Console.WriteLine("Please type original swagger path:");
            string originalSwaggerPath = Console.ReadLine();

            Console.WriteLine("Please type converted TSP swagger path:");
            string tspConvertedSwaggerPath = Console.ReadLine();

            if (string.IsNullOrEmpty(originalSwaggerPath) || string.IsNullOrEmpty(tspConvertedSwaggerPath))
            {
                throw new ArgumentException("Input paths cannot be null or empty");
            }

            // Get two json files
            JObject originalSwagger = Helper.ReadPath(originalSwaggerPath);
            JObject tspSwagger = Helper.ReadPath(originalSwaggerPath);

            // Convert to contrast friendly json
            var converted_origin = Helper.OutputContrastfriendlyJson(originalSwagger);
            var converted_tsp = Helper.OutputContrastfriendlyJson(tspSwagger);

            // output two json files
            string outputFolder = originalSwaggerPath;
            string directoryPath = Path.GetDirectoryName(outputFolder);

            // 去掉最后的文件名
            int index = directoryPath.LastIndexOf(Path.DirectorySeparatorChar);
            if (index != -1)
            {
                directoryPath = directoryPath.Substring(0, index);
            }

            Console.WriteLine(directoryPath);

        }



        protected void SortDefinition()
        {
            string resourcefile = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\JsonStr1.txt");
            StreamReader sr = new StreamReader(resourcefile);
            string str = "";
            while (!sr.EndOfStream)
            {
                str += sr.ReadLine();
            }

            JObject json = JObject.Parse(str);
            JObject orderedJson = new JObject();

            var orderedNames = json.Properties().Select(item => item.Name).OrderBy(n => n);
            foreach (var name in orderedNames)
            {
                orderedJson.Add(json.Properties().Where(item => item.Name == name));
            }

            string jsonText = orderedJson.ToString();
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\Output.txt");
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(jsonText);
            }
        }

    }
}
