using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.DataStructure
{
    public class Node
    {
        public int Value;

        public Node Left;

        public Node Right;

        public Node() { }

        public Node(int value, Node left = null, Node right = null)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        public static Node CreateByIntArray(int[] nums)
        {
            if (nums.Length == 0)
                return new Node();
            Node root = new Node();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            for (int i = 0; i < nums.Length; i++)
            {
                Node cur = queue.Dequeue();
                cur.Value = nums[i];
                cur.Left = new Node();
                cur.Right = new Node();
                queue.Enqueue(cur.Left);
                queue.Enqueue(cur.Right);
            }
            return root;
        }

        public static List<List<string>> BFS(Node root)
        {
            if (root == null)
                return new List<List<string>>();

            List<List<string>> result = new List<List<string>>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                int curTotal = queue.Count;
                List<string> line = new List<string>();
                while (curTotal-- > 0 )
                {
                    Node curNode = queue.Dequeue();
                    line.Add(curNode?.Value.ToString());
                    queue.Enqueue(curNode?.Left);
                    queue.Enqueue(curNode?.Right);
                }
                if (queue.Contains(null))
                {
                    break;
                }
                result.Add(line);
            }
            return result;
        }
    }
}
