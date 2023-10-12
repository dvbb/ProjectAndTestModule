using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AzureHelper
{
    public static class BaseClientExtension
    {
        public static Random _random => new Random();

        public static async Task<List<T>> ToEnumerableAsync<T>(this IAsyncEnumerable<T> asyncEnumerable)
        {
            List<T> list = new List<T>();
            await foreach (T item in asyncEnumerable)
            {
                list.Add(item);
            }
            return list;
        }


        public static string CreateRandomName(string resource)
        {
            return $"{resource}{_random.Next(9999)}";
        }
    }
}
