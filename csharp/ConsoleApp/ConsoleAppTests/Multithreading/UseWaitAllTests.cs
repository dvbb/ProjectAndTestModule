using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Multithreading.Tests
{
    public class UseWaitAllTests
    {
        private static UseWaitAll _member;
        private static int _i;

        [SetUp]
        public void SetUp()
        {
            _member = new UseWaitAll();
        }

        [Test]
        public void WaitAllTest()
        {
            // 单个 Task 执行 需要5-8s.
            // 将多个 Task 放入 Task[] 中，然后执行Task.WaitAll.
            // 此时 Task[] 中的所有任务会并发运行，同时所有的任务完成前，主线程会阻塞在此行.
            Action action = () =>
            {
               Task[] tasks = new Task[] { _member.Show("this is t1"), _member.Show("this is t2") };
               Task.WaitAll(tasks);
            };
            Watch(action);
        }

        [Test]
        public async Task NoAwait()
        {
            for (int i = 0; i < 100; i++)
            {
                Task.Run(() => { _i++; });
                Task.Run(() => { _i++; });
            }
            Thread.Sleep(1000);
            Console.Out.WriteLine(_i); //有时候 _i 不等于 200
        }

        private static void Watch(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action.Invoke();
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }
}