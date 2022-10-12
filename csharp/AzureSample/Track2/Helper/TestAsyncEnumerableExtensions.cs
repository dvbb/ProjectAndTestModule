using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Track2.Helper
{
    public static class TestAsyncEnumerableExtensions
    {
        public static async Task<List<T>> ToEnumerableAsync<T>(this IAsyncEnumerable<T> asyncEnumerable)
        {
            List<T> list = new List<T>();
            await foreach (T item in asyncEnumerable)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
