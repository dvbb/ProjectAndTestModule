using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankApiServer.Models
{
    public class Employee
    {
        public int EId { set; get; }
        public string? Pwd { set; get; }
        public string? EName { set; get; }
    }
}