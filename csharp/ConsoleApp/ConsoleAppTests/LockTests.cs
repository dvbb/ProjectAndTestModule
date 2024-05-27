using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppTests
{
    internal class LockTests
    {
        private static readonly object _lockme = new object();
        private static List<string> strings = new List<string>();

        [SetUp]
        public void SetUp()
        {
            strings.Clear();
        }

        [Test]
        public async Task NoLockTest()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(AddNow());
            tasks.Add(AddNow2());
            tasks.Add(AddNow3());
            Task.WaitAll(tasks.ToArray());

            foreach (var item in strings)
            {
                await Console.Out.WriteLineAsync(item);
            }
        }

        [Test]
        public async Task LockTest()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(AddNow_With_Lock());
            tasks.Add(AddNow_With_Lock());
            tasks.Add(AddNow_With_Lock());
            tasks.Add(AddNow_With_Lock());
            tasks.Add(AddNow_With_Lock());
            Task.WaitAll(tasks.ToArray());

            foreach (var item in strings)
            {
                await Console.Out.WriteLineAsync(item);
            }
        }

        public static async Task AddNow()
        {
            await Task.Delay(1000);
            strings.Add(DateTime.Now.ToString());
        }
        public static async Task AddNow2()
        {
            await Task.Delay(2000);
            strings.Add(DateTime.Now.ToString());
        }
        public static async Task AddNow3()
        {
            await Task.Delay(5000);
            strings.Add(DateTime.Now.ToString());
        }

        public static async Task AddNow_With_Lock()
        {
            lock (_lockme)
            {
                Task.Delay(1000);
                strings.Add(DateTime.Now.ToString());
            }
        }
    }
}
