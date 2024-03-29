﻿using System;
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

        // 委托和事件的区别：
        //  委托：委托是一个类
        //  事件：事件是委托的一个具体实例，且有安全限制

        // event 关键字是对委托的修饰
        //  添加了安全限制，使外部只能对该委托 += -= ，无法 = 或 invoke
        public event Action SayStartHandlerEvnet;
        public void SayStartVerEvent()
        {
            if (this.SayStartHandlerEvnet != null)
            {
                Console.WriteLine("MC: Start!");
                this.SayStartHandlerEvnet.Invoke();
            }
        }
    }
}
