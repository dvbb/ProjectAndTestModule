using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests.Polymorphism
{
    internal class AbstractTests
    {
        [Test]
        public void AbstractTest()
        {
            // 抽象类和接口不能被实例化
            //Shape shape = new Shape();

            // 但是可以 父类类型来引用子类的实例
            Shape circle = new Circle();
            circle.Length = 5;
            circle.Draw();    // 实现的抽象类方法，circle
            circle.Display(); // override 的虚方法，circle
            circle.Rotate();

            Console.WriteLine("\n SQUARE:");
            Shape square = new Square();
            square.Draw();
            square.Display();
            square.Rotate(); //引用的还是父类Shape的Rotate()方法

            Square square2 = new Square();
            square2.Rotate();
        }

        private abstract class Shape
        {
            public int Length;

            // 抽象方法，需要在派生类中实现
            public abstract void Draw();

            // 虚方法，可以在派生类中选择性重写
            public virtual void Display()
            {
                Console.WriteLine("Displaying shape");
            }

            // 普通方法，派生类可以选择性地进行隐藏或者重写
            public void Rotate()
            {
                Console.WriteLine("Rotating shape");
            }
        }

        private class Circle : Shape
        {
            public override void Draw()
            {
                Console.WriteLine("Drawing a [circle]");
            }

            public override void Display()
            {
                Console.WriteLine("Displaying [circle]");
            }
        }

        private class Square : Shape
        {
            public override void Draw()
            {
                Console.WriteLine("Drawing a [Square]");
            }

            //public override void Display()
            //{
            //    Console.WriteLine("Displaying Square");
            //}

            public void Rotate()
            {
                Console.WriteLine("Rotating shape for [square]");
            }
        }
    }
}
