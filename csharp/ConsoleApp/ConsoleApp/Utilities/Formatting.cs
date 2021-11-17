using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Utilities
{
    public static class Formatting
    {
        public static string LinkedListToString<T>(LinkedList<T> list)
        {
            if (list is null)
                return "[]";

            StringBuilder sb = new StringBuilder("[");
            foreach (var item in list)
            {
                sb.Append($"{item.ToString()},");
            }
            return sb.ToString().TrimEnd(',') + "]";
        }
    }
}
