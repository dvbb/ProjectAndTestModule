using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdapterPattern.Models
{
    internal class RedisHelper
    {
        public RedisHelper() { }

        public void AddRedis<T>()
        {
            Console.WriteLine($"{this.GetType().Name} add method");
        }

        public void DeleteRedis<T>()
        {
            Console.WriteLine($"{this.GetType().Name} delete method");
        }
    }
}
