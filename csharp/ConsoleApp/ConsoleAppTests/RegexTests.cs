using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleAppTests
{
    internal class RegexTests
    {
        #region Other char match rule
        //  \b  匹配一个单词边界，指单词和空格间的位置
        //  \B  匹配非单词边界
        //  \d  匹配一个数字字符，等价于[0 - 9]
        //  \D  匹配一个非数字字符，等价于[^0 - 9]
        //  \f  匹配一个换页符
        //  \n  匹配一个换行符
        //  \r  匹配一个回车符
        //  \s  匹配任何空白字符，包括空格、制表符、换页符等
        //  \S  匹配任何非空白字符
        //  \t  匹配一个制表符
        //  \v  匹配一个垂直制表符。等价于\x0b和\cK
        //  \w  匹配包括下划线的任何单词字符。等价于‘'[A-Za-z0-9_]’
        //  \W  匹配任何非单词字符。等价于‘[^A-Za-z0-9_]’
        #endregion

        [Test]
        public void RegexTest()
        {
            Console.WriteLine("^ 匹配 input 的开始位置");
            Console.WriteLine("$ 匹配 input 的结束位置");
            Console.WriteLine("* 匹配前面的 0 or n 的子表达式");
            Console.WriteLine("+ 匹配前面的 1 or n 的子表达式");
            Console.WriteLine("? 匹配前面的 0 or 1 的子表达式");
            Console.WriteLine(". 匹配除 '\\n' 之外的任何单个字符");
            Console.WriteLine("[] 匹配 [] 内的字符");
            Console.WriteLine("[^] 匹配除 [] 内的字符");

            Console.WriteLine();

            string input = "hello world!";
            string pattern1 = "[hel]";
            string pattern2 = "[hel]*";
            string pattern3 = "[^r]*";
            string pattern4 = "[^r]*r+.*";
            string pattern5 = "hel*o? w.*!";
            Console.WriteLine($"input:{input}   pattern:{pattern1}");
            Console.WriteLine("\t" + Regex.Match(input, pattern1));
            Console.WriteLine($"input:{input}   pattern:{pattern2}");
            Console.WriteLine("\t" + Regex.Match(input, pattern2));
            Console.WriteLine($"input:{input}   pattern:{pattern3}");
            Console.WriteLine("\t" + Regex.Match(input, pattern3));
            Console.WriteLine($"input:{input}   pattern:{pattern4}");
            Console.WriteLine("\t" + Regex.Match(input, pattern4));
            Console.WriteLine($"input:{input}   pattern:{pattern5}");
            Console.WriteLine("\t" + Regex.Match(input, pattern5));
        }

        [Test]
        public void EmailRegexMatch()
        {
            string suffix = "com|net|org|edu|mil|tv|biz|info";
            string organization = "qq|163|microsoft";
            string pattern = $"^(https://|http://)?\\w*@({organization}).({suffix})$";
            string email1 = "xjXdDYGZZ845f@qq.com";
            string email2 = "xjidkf.cn";
            string email3 = "www.baidu.com";
            string email4 = "https://pxlink@163.com";
            string email5 = "pxlink@163.com";
            string email6 = "xji011dk485f@qq.com";
            Console.WriteLine(email1 + ":\t" + Regex.IsMatch(email1, pattern));
            Console.WriteLine(email2 + ":\t" + Regex.IsMatch(email2, pattern));
            Console.WriteLine(email3 + ":\t" + Regex.IsMatch(email3, pattern));
            Console.WriteLine(email4 + ":\t" + Regex.Match(email4, pattern));
            Console.WriteLine(email5 + ":\t" + Regex.Match(email5, pattern));
            Console.WriteLine(email6 + ":\t" + Regex.IsMatch(email6, pattern));
        }
    }
}
