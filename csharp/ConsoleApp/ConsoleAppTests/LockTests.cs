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
        private static int _i = 0;

        [SetUp]
        public void SetUp()
        {
            _i = 0;
        }

        [Test]
        public async Task NoLockTest()
        {
            for (int i = 0; i < 100; i++)
            {
                 Task.Run(() => { _i++; });
                 Task.Run(() => { _i++; });
            }
            Thread.Sleep(1000);
            Console.Out.WriteLine(_i);
        }

        [Test]
        public async Task LockTest()
        {
            for (int i = 0; i < 100; i++)
            {
                Task.Run(ISelfPlus);
                Task.Run(ISelfPlus);
            }
            Thread.Sleep(1000);
            Console.Out.WriteLine(_i);
        }

        private void ISelfPlus()
        {
            lock (_lockme)
            {
                _i++;
            }
        }
    }
}
