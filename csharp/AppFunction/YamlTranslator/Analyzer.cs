using System;
using System.Collections.Generic;
using System.Text;

namespace YamlTranslator
{
    internal class Analyzer
    {
        public void Start(string str)
        {
            IsSymbolMatch(str);
        }

        public void IsSymbolMatch(string str)
        {
            if (str == null || string.IsNullOrEmpty(str))
                throw new ArgumentNullException("str");

            char[] chars = str.ToCharArray();
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '{' || chars[i] == '[')
                {
                    stack.Push(chars[i]);
                    continue;
                }
                if ((chars[i] == '}' && stack.Peek() != '{') || (chars[i] == ']' && stack.Peek() != ']'))
                {
                    throw new Exception($"Unexpected String.");
                }
                if (chars[i] == '}' || stack.Peek() != ']')
                {
                    stack.Pop();
                    continue;
                }
                if (chars[i] == '"' && stack.Peek() == '"')
                {
                    stack.Pop();
                    continue;
                }
                if (chars[i] == '"' && stack.Peek() != '"')
                {
                    stack.Push(chars[i]);
                }
            }
        }
    }
}
