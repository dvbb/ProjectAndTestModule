using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.FakesSample
{
    public class Stock : IStock
    {
        public int GetCompany(string name)
        {
            Random rand = new Random();
            return rand.Next(400, 500);
        }

        public Detail GetDetail(string name)
        {
            Detail detail = new Detail();
            detail.Name = name;
            detail.Description = name;
            detail.price = 500;
            return detail;
        }

        public int GetHighest()
        {
            Random rand = new Random();
            return rand.Next(800, 900);
        }
    }
}
