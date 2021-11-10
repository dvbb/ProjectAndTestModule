using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Multithreading
{
    public class UseWaitAll
    {
        public async Task<string> Show(string str)
        {
            Random random = new Random();
            int sleepTime = random.Next(5000, 8000);
            await Task.Run(() =>
            {
                Thread.Sleep(sleepTime);
                Console.WriteLine(str);
            });
            return $"ok:{str}-{sleepTime}";
        }
    }
}
