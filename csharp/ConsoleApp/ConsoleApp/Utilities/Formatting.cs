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
        public static string Convert<T>(T[] nums)
        {
            if (nums is null || nums.Length == 0) return "[]";
            string str = "[";
            for (int i = 0; i < nums.Length; i++)
            {
                str += nums[i].ToString() + ",";
            }
            return str.Substring(0, str.Length - 1) + "]";
        }

        public static string Convert<T>(T[] nums, int length)
        {
            if (nums is null || nums.Length == 0) return "[]";
            string str = "[";
            for (int i = 0; i < length; i++)
            {
                str += nums[i].ToString() + ",";
            }
            return str.Substring(0, str.Length - 1) + "]";
        }

        public static string Convert<T>(IList<T> list)
        {
            if (list is null || list.Count == 0) return "[]";
            List<T> oblist = new List<T>(list);
            T[] objects = oblist.ToArray();
            string str = "[";
            for (int i = 0; i < objects.Length; i++)
            {
                str += objects[i].ToString() + ",";
            }
            return str.Substring(0, str.Length - 1) + "]";
        }

        public static List<string> ConvertMultipleList<T>(List<List<T>> list)
        {
            if(list is null || list.Count ==0)
                return new List<string>();
            List<string> result = new List<string>();
            foreach (var item in list)
            {
                result.Add(Convert(item));
            }
            return result;
        }
    }
}
