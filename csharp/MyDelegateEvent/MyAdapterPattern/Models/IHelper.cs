using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdapterPattern.Models
{
    internal interface IHelper
    {
        public void Add<T>(T t) { }
        public void Delete<T>(T t) { }
    }
}
