using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdapterPattern.Models
{
    internal class MysqlHelper : IHelper
    {
        public MysqlHelper() { }

        public void Add<T>(T t)
        {
            Console.WriteLine($"{this.GetType().Name} add method");
        }

        public void Delete<T>(T t)
        {
            Console.WriteLine($"{this.GetType().Name} delete method");
        }
    }
}
