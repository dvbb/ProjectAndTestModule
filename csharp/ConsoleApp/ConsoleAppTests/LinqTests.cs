using ConsoleApp.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppTests
{
    public class LinqTests
    {
        [Test]
        public void DistinctTest()
        {
            int[] nums = new int[] { 1, 2, 3, 5, 5, 1, 2, 6, 8, 8, 12, 0 };
            List<int> list = new List<int>(nums);
            Console.WriteLine($"row arrays: {Formatting.Convert(nums)}");
            var result = list.Distinct();
            Console.WriteLine($"Distinct: {Formatting.Convert(result)}");
        }

        [Test]
        public void DefaltIfEmptyTest()
        {
            IList<int> intLums = new List<int>();
            var defaltIntNums = intLums.DefaultIfEmpty().ToList();
            Console.WriteLine("defalt a List<int>: " + defaltIntNums[0]);

            IList<bool> boolLums = new List<bool>();
            var defaltBoolNums = boolLums.DefaultIfEmpty().ToList();
            Console.WriteLine("defalt a List<Bool>: " + defaltBoolNums[0]);

            IList<string> stringLums = new List<string>();
            var defaltStringNums = stringLums.DefaultIfEmpty().ToList();
            Console.WriteLine("defalt a List<string>: " + defaltStringNums[0]);
        }

        [Test]
        public void ElementAtTest()
        {
            IList<string> strList = new List<string>() { "1203351", "tier", "hello" };
            string str1 = strList.ElementAtOrDefault(2);
            string str2 = strList.ElementAtOrDefault(8);
            Console.WriteLine("item2: " + str1);
            Console.WriteLine("item8: " + str2);
        }

        [Test]
        public void ExceptTest()
        {
            IList<int> nums1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            IList<int> nums2 = new List<int>() { 2, 3, 6 };
            Console.WriteLine($"nums1: {Formatting.Convert(nums1)}");
            Console.WriteLine($"nums2: {Formatting.Convert(nums2)}");
            Console.WriteLine($"nums1.Except(nums2): {Formatting.Convert(nums1.Except(nums2))}");
        }

        [Test]
        public void UniontTest()
        {
            IList<int> nums1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            IList<int> nums2 = new List<int>() { 0, 2, 3, 6, 25, 28 };
            Console.WriteLine($"nums1: {Formatting.Convert(nums1)}");
            Console.WriteLine($"nums2: {Formatting.Convert(nums2)}");
            Console.WriteLine($"nums1.Except(nums2): {Formatting.Convert(nums1.Union(nums2))}");
        }

        [Test]
        public void IntersectTest()
        {
            IList<int> nums1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            IList<int> nums2 = new List<int>() { 0, 2, 3, 6, 25, 28 };
            Console.WriteLine($"nums1: {Formatting.Convert(nums1)}");
            Console.WriteLine($"nums2: {Formatting.Convert(nums2)}");
            Console.WriteLine($"nums1.Intersect(nums2): {Formatting.Convert(nums1.Intersect(nums2))}");
        }

        [Test]
        public void LastTest()
        {
            IList<int> nums1 = new List<int>() { 1, 2, 3, 4, 5, 6 };
            IList<int> nums2 = new List<int>() { 0, 2, 3, 6, 25, 28 };
            IList<int> nums3 = new List<int>() { };
            Console.WriteLine($"nums1: {Formatting.Convert(nums1)}");
            Console.WriteLine($"nums2: {Formatting.Convert(nums2)}");
            Console.WriteLine($"nums3: {Formatting.Convert(nums3)}");
            Console.WriteLine($"nums1 last: {nums1.LastOrDefault()}");
            Console.WriteLine($"nums2 last: {nums2.LastOrDefault()}");
            Console.WriteLine($"nums3 last: {nums3.LastOrDefault()}");
        }
    }
}
