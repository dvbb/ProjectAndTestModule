using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyDelegateEvent.Character
{
    internal class MC
    {
        /// <summary>
        /// MC说开始，然后发生一些列的事件.
        /// 若需要添加新的事件或修改事件顺序，就需要改动SayStartVer1()方法体的代码，违背了设计的开闭原则
        /// 按照面向对象的原则，
        /// </summary>
        public void SayStartVer1()
        {
            Console.WriteLine("MC: Start!");
            new Singer().Singing();
            new Audience().Listening();
            new Cat().Miao();
            new Dog().Wang();
        }

        public Action SayStartHandler;
        public  void SayStartVer2()
        {
            if (this.SayStartHandler != null)
            {
                Console.WriteLine("MC: Start!");
                this.SayStartHandler.Invoke();
            }
        }
    }
}
