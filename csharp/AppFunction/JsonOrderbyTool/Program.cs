using JsonOrderbyTool.Helper;
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
            //SortPaths();

            //SortDefinition();

            //FilterOpenApiByPartialPaths();

            SortTwoDefinitionBasedOnSwagger();
        }

        protected static void AddVerb(ref string verbs, string? verb)
        {
            if (verb != null)
            {
                verbs += verb + ",\n";
            }
        }

        protected static void FilterOpenApiByPartialPaths()
        {
            JObject partialPathsJson = FileHelper.GetFileContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\partialPaths.json"));
            JObject openapiJson = FileHelper.GetFileContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\openaip.json"));

            // Select tsp paths
            JObject outputJson = new JObject();
            var selectedName = partialPathsJson.Properties().Select(item => item.Name);
            Console.WriteLine("Partial paths number: " + selectedName.Count());
            foreach (var name in selectedName)
            {
                var path = openapiJson.Properties().Where(item => item.Name == name);
                outputJson.Add(path);
            }
            Console.WriteLine("Selected paths number: " + outputJson.Properties().Count());

            // Output
            FileHelper.OutputContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\Output.json"), outputJson.ToString());
        }

        protected static void SortPaths()
        {
            JObject swagger = FileHelper.GetFileContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\OriginalSwagger.json"));
            JObject orderedJson = new JObject();

            // order [paths] by descending
            var orderedNames = swagger.Properties().Select(item => item.Name).OrderBy(n => n);
            Console.WriteLine("Paths number: " + orderedNames.Count());
            Console.WriteLine("Paths name: ");
            foreach (var name in orderedNames)
            {
                Console.WriteLine(name);
                orderedJson.Add(swagger.Properties().Where(item => item.Name == name));
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
            FileHelper.OutputContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\Output.json"), finalResult);
        }

        protected static void SortDefinition()
        {
            JObject swagger = FileHelper.GetFileContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\OriginalSwagger.json"));
            JObject orderedJson = JsonHelper.Sort(swagger);
            FileHelper.OutputContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\Output.json"), orderedJson.ToString());
        }

        protected static void SortTwoDefinitionBasedOnSwagger()
        {
            JObject swagger = FileHelper.GetFileContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\OriginalSwagger.json"));
            JObject openapi = FileHelper.GetFileContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\openaip.json"));

            JObject sortedSwagger = new JObject();
            JObject filteredOpenapi = new JObject();

            // sort swagger
            var orderedNames = swagger.Properties().Select(item => item.Name).OrderBy(n => n);
            Console.WriteLine("Total: " + orderedNames.Count());
            sortedSwagger = JsonHelper.Sort(swagger);

            // filter
            var selectedName = sortedSwagger.Properties().Select(item => item.Name);
            foreach (var name in orderedNames)
            {
                var model = openapi.Properties().Where(item => item.Name == name);
                filteredOpenapi.Add(model);
            }

            // output
            FileHelper.OutputContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\Model_Swagger.json"), sortedSwagger.ToString());
            FileHelper.OutputContent(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\asset\Model_Openapi.json"), filteredOpenapi.ToString());
        }
    }
}
