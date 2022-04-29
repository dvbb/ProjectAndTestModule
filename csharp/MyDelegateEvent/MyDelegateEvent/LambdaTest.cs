using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyDelegateEvent
{
    internal class LambdaTest
    {
        // Lambda可用于语法糖，使代码更简洁
        private string OtherStr => "Hello"; //等同于一个只读的属性 { get{ return "Hello";} }
        private string GetOtherStr() => this.OtherStr; //带返回值的方法 { return this.OtherStr; }

        public delegate void NoReturnNoPara(int num, string name);
        public void Show(int num, string name)
        {
            Console.WriteLine($"[1] Hello,{num}-{name}.");
        }

        public void Start()
        {
            // 定义方法
            NoReturnNoPara method1 = new NoReturnNoPara(this.Show);
            method1.Invoke(20171316, "noki");

            // 匿名方法
            NoReturnNoPara method2 = new NoReturnNoPara(delegate (int num, string name) { Console.WriteLine($"[2] Hello,{num}-{name}."); });
            method2.Invoke(20171325, "fuyumi");

            // Lambda (Lambda类似匿名方法)
            // 参数列表可以不用加 int string，编译器会根据委托约束自动推断参数类型。
            // new NoReturnNoPara(...)也可以省略。编译器会根据返回类型推断出实例委托。

            // Lambda的本质：
            // 在IL中，会生成一个生成一个类中类(private sealed class), 然后为每一个lambda都生成一个独立的方法，然后绑定到委托的实例。

            //NoReturnNoPara method3 = new NoReturnNoPara((int num, string name) => { Console.WriteLine($"[3] Hello,{num}-{name}."); });
            NoReturnNoPara method3 = (num, name) => { Console.WriteLine($"[3] Hello,{num}-{name}."); };
            method3.Invoke(20171325, "yukiri");

            // 基于lambda表达式注册的多播委托无法移除
            // 例如：
            //  method4 定义了5个一模一样的lambda表达式，但实际上IL中是五个独立的方法
            //  也无法被 += -=
            Console.WriteLine("基于lambda表达式注册的多播委托无法移除:");
            NoReturnNoPara method4 = (num, name) => Console.WriteLine($"[4] Hello,{num}-{name}."); ;
            method4 += (num, name) => Console.WriteLine($"[4] Hello,{num}-{name}."); ;
            method4 += (num, name) => Console.WriteLine($"[4] Hello,{num}-{name}."); ;
            method4 -= (num, name) => Console.WriteLine($"[4] Hello,{num}-{name}."); ;
            method4 -= (num, name) => Console.WriteLine($"[4] Hello,{num}-{name}."); ;
            method4.Invoke(20201351, "natsuki");
        }

        public void Start2()
        {
            // Action 无返回值的泛型委托（0-16个参数）
            Action action0 = () => { };
            Action<string> action1 = str => { };
            Action<string, int, double> action2 = (str, num, count) => { };

            // Func 有返回值的泛型委托（0-16个参数）
            Func<string> func0 = () => { return ""; };
            Func<int, string> func11 = (num) => { return num.ToString(); };
            Func<int, string> func12 = (num) => num.ToString();  // 与func11是一个效果，编译器会识别return
        }

        public void Start3()
        {
            // func0 func1 的lambda是一个方法
            Func<Student, bool> func0 = s => s.Number == 20171316 && s.Name.Equals("hibi");
            Func<Student, bool> func1 = s =>
            {
                Console.WriteLine("searching...");
                return s.Number == 20171316 && s.Name.Equals("hibi");
            };

            // func3 的lambda是一个Expression(二叉树数据结构)
            Expression<Func<Student, bool>> func3 = s => s.Number == 20171316 && s.Name.Equals("hibi");

            //  CS0834: 无法将具有语句体的lambda表达式转换为表达式目录树
            //Expression<Func<Student, bool>> func4 = s =>
            //{
            //    Console.WriteLine("searching...");
            //    return s.Number == 20171316 && s.Name.Equals("hibi");
            //};
        }
    }
}
