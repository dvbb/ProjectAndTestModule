using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace JsonOrderbyTool
{
    public class Program
    {
        string[] Verbs => new string[4] { "get", "put", "patch", "delete" };

        static void Main(string[] args)
        {
            string outputPath;

            string resourcefile = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\JsonStr1.txt");
            StreamReader sr = new StreamReader(resourcefile);
            string str = "";
            while (!sr.EndOfStream)
            {
                str += sr.ReadLine();
            }

            JObject json = JObject.Parse(str);
            JObject orderedJson = new JObject();

            // order [paths] by descending
            var orderedNames = json.Properties().Select(item => item.Name).OrderBy(n => n);
            foreach (var name in orderedNames)
            {
                orderedJson.Add(json.Properties().Where(item => item.Name == name));
            }

            // order [http verb] by: get, put, patch, delete (descending)
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{\n");
            foreach (var path in orderedJson.Properties())
            {
                string temp = "";
                if (path.First().Count() == 1)
                {
                    temp = $"\"{path.Name}\": " + path.First() + ",\n";
                    stringBuilder.Append(temp);
                }
                else
                {
                    string verbs = "";
                    string? get = path.First().Where(i => i.ToString().Contains("\"get\":")).FirstOrDefault()?.ToString();
                    string? put = path.First().Where(i => i.ToString().Contains("\"put\":")).FirstOrDefault()?.ToString();
                    string? patch = path.First().Where(i => i.ToString().Contains("\"patch\":")).FirstOrDefault()?.ToString();
                    string? delete = path.First().Where(i => i.ToString().Contains("\"delete\":")).FirstOrDefault()?.ToString();

                    AddVerb(ref verbs, get);
                    AddVerb(ref verbs, put);
                    AddVerb(ref verbs, patch);
                    AddVerb(ref verbs, delete);
                    verbs = verbs.Substring(0, verbs.Count() - 2);

                    temp = $"\"{path.Name}\": " + "{\n" + verbs + "\n},\n";
                    stringBuilder.Append(temp);
                }
            }
            string finalResult = stringBuilder.ToString();
            finalResult = finalResult.Substring(0, finalResult.Count() - 2);
            finalResult += "\n}\n";

            // Output
            outputPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\Output.txt");
            using (StreamWriter sw = File.CreateText(outputPath))
            {
                sw.Write(finalResult);
            }
        }

        protected static void AddVerb(ref string verbs, string? verb)
        {
            if (verb != null)
            {
                verbs += verb + ",\n";
            }
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

        protected void SortTwoFiles()
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
    }
}
