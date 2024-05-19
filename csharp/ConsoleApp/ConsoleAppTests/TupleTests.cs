using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests
{
    public class TupleTests
    {

        [Test]
        public void TupleTest()
        {
            Tuple<int, string, double> tuple1 = new Tuple<int, string, double>(1, "2", 3.5);
            Console.WriteLine(tuple1.Item1);
            Console.WriteLine(tuple1.Item2);
            Console.WriteLine(tuple1.Item3);
            Console.WriteLine();

            Tuple<int, string, double, int, int, int, int, Tuple<int, int>> tuple2
                = new Tuple<int, string, double, int, int, int, int, Tuple<int, int>>(1, "2", 3.5, 4, 5, 6, 7, new Tuple<int, int>(8, 9));
            Console.WriteLine(tuple2.Item5);
            Console.WriteLine(tuple2.Rest.Item1);
        }

        [Test]
        public void ValueTupleTest()
        {
            var tuple2 = new ValueTuple<int, int, int, int, int, int, int, ValueTuple<int, int>>(1, 2, 3, 4, 5, 6, 7, new ValueTuple<int, int>(8, 9));
            Console.WriteLine(tuple2.Item8);
        }

        [Test]
        public void TupleSwap()
        {
            string x = "123456";
            string y = "abcd";
            Console.WriteLine($"x = {x}, y = {y}");
            (x, y) = (y, x);
            Console.WriteLine($"x = {x}, y = {y}");
        }

        /// <summary>
        /// Tuple可以减少类/结构体的定义，一些临时返回值均可使用Tuple.
        /// 例如 存储过程 的返回结果类型并不是每次都会被定义，此时用Tuple可以返回任意数目、类型的字段
        /// </summary>
        private Tuple<Guid, string, string> MockReturnTempValue(Guid guid)
        {
            if (guid != null)
            {
                throw new ArgumentNullException("name cannot be null or tmpty.");
            }
            Console.WriteLine("ADO operation...");
            string name = "costa";
            string sex = "male";
            Tuple<Guid, string, string> result = new Tuple<Guid, string, string>(guid, name, sex);
            return result;
        }
    }
}
