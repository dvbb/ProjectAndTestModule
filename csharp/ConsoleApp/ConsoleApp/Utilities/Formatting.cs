using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp.Utilities
{
    public static class Formatting
    {
        public static string Convert<T>(T[] nums)
        {
            if (nums is null)
            {
                throw new ArgumentNullException("nums cannot be null");
            }
            if (nums.Length == 0)
            {
                return "[]";
            }
            string str = "[";
            for (int i = 0; i < nums.Length; i++)
            {
                str += nums[i].ToString() + ",";
            }
            return str.Substring(0, str.Length - 1) + "]";
        }

        public static string Convert<T>(T[] nums, int length)
        {
            if (nums is null)
            {
                throw new ArgumentNullException("nums cannot be null");
            }
            if (nums.Length == 0)
            {
                return "[]";
            }
            string str = "[";
            for (int i = 0; i < length; i++)
            {
                str += nums[i].ToString() + ",";
            }
            return str.Substring(0, str.Length - 1) + "]";
        }

        public static string Convert<T>(IEnumerable<T> list)
        {
            if (list is null)
            {
                new ArgumentNullException("input Enumerable cannot be null.");
            }
            if (list.Count() == 0)
            {
                return "";
            }
            string result = "[";
            foreach (var item in list)
            {
                result += $"{item},";
            }
            return result.Substring(0, result.Length - 1) + "]";
        }

        public static List<string> ConvertMultipleList<T>(IEnumerable<IEnumerable<T>> list)
        {

            if (list is null)
            {
                new ArgumentNullException("input Enumerable cannot be null.");
            }
            if (list.Count() == 0)
            {
                return new List<string>();
            }
            List<string> result = new List<string>();
            foreach (var item in list)
            {
                result.Add(Convert(item));
            }
            return result;
        }
    }
}
