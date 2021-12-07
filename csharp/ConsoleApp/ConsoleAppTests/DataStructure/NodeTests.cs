using ConsoleApp.DataStructure;
using ConsoleApp.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests.DataStructure
{
    public class NodeTests
    {
        [Test]
        public void NodeTest()
        {
            Node root = Node.CreateByIntArray(new int[] { 1, 2, 3, 4, 5, 6, 7 });
            List<List<string>> tree1 = Node.BFS(root);
            var show = Formatting.ConvertMultipleList(tree1);
            foreach (var item in show)
            {
                Console.WriteLine(item);
            }
        }
    }
}
