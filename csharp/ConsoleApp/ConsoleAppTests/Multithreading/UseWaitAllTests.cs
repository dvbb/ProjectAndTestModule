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
        public async Task WaitAllTest()
        {
            // 单个 Task 执行 需要5-8s.
            // 将多个 Task 放入 Task[] 中，然后执行Task.WaitAll.
            // 此时 Task[] 中的所有任务会并发运行，同时所有的任务完成前，主线程会阻塞在此行.
            Action action = () =>
            {
                Task[] tasks = new Task[] { _member.Show("this is t1"), _member.Show("this is t2") };
                Task.WaitAll(tasks);
            };
            await Watch(action);
        }

        [Test]
        public async Task NoAwaitTest()
        {
            for (int i = 0; i < 100; i++)
            {
                Task.Run(() => { _i++; });
                Task.Run(() => { _i++; });
            }
            Thread.Sleep(1000);
            Console.Out.WriteLine(_i); //有时候 _i 不等于 200
        }

        [Test]
        public async Task AwaitTest1()
        {
            Action action1 = () =>
            {
                WaitOneSecond();
                WaitThreeSeconds();
            };
            await Console.Out.WriteLineAsync("不使用await");
            // 实际上会打印00:00:04.0014695； 只是还未到这一行，程序就已经结束
            Watch(action1);
        }

        [Test]
        public async Task AwaitTest2()
        {
            Action action2 = async () =>
            {
                await WaitOneSecond();
                await WaitThreeSeconds();
            };

            await Console.Out.WriteLineAsync("使用await");
            await Watch(action2);
        }

        private static async Task Watch(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await Task.Run(action);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }

        private static async Task WaitOneSecond()
        {
            Thread.Sleep(1000);
        }
        private static async Task WaitThreeSeconds()
        {
            Thread.Sleep(3000);
        }
    }
}