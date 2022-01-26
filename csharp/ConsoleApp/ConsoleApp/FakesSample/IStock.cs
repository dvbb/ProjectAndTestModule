using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.FakesSample
{
    public interface IStock
    {
        public int GetCompany(string name);

        public int GetHighest();

        public Detail GetDetail(string name);
    }
}
