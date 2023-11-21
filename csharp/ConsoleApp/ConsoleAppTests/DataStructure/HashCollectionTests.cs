using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests.DataStructure
{
    public class HashCollectionTests
    {
        [Test]
        public void HashTableCommonTest()
        {
            Hashtable ht = new Hashtable();
            ht.Add(1, "first");
            ht.Add("second", true);
            //ht.Add("second", true); //cannot add repetitive key
            ht.Add(3.152, new List<int>() { 2, 5, 7, 51 });
            foreach (var item in ht.Keys)
            {
                Console.WriteLine($"{item},{ht[item]}");
            }

            Console.WriteLine();
            Console.WriteLine($"ht.count:{ht.Count}");
            Console.WriteLine($"whether ht'key contain 1:{ht.ContainsKey(1)}");
            Console.WriteLine($"whether ht'value contain 1:{ht.ContainsValue(1)}");
        }

        [Test]
        public void HashSetCommonTest()
        {
            HashSet<int> hs = new HashSet<int>();
            hs.Add(1);
            hs.Add(1);
            hs.Add(2);
            foreach (var item in hs)
            {
                Console.WriteLine(item);
            }
            hs.Contains(1);
            Console.WriteLine();
            Console.WriteLine($"hs.count: {hs.Count}");
            Console.WriteLine($"whether hs contain 1: {hs.Contains(1)}");
        }

        [Test]
        public void DictionaryCommonTest()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "first");
            //dic.Add(1, "first"); //cannot add repetitive key
            dic.Add(2, "second");
            dic.Add(3, "third");
            Console.WriteLine("loop func 1:");
            foreach (var item in dic)
            {
                Console.WriteLine($"{item.Key},{item.Value}");
            }
            Console.WriteLine("loop func 2:");
            foreach (var item in dic.Keys)
            {
                Console.WriteLine($"{item},{dic[item]}");
            }

            Console.WriteLine();
            Console.WriteLine($"dic[1]: {dic[1]}");
            Console.WriteLine($"dic.count: {dic.Count}");
            Console.WriteLine($"whether dic contain key 2: {dic.ContainsKey(2)}");
            Console.WriteLine($"whether dic contain value 2: {dic.ContainsValue("2")}");
        }
    }
}
