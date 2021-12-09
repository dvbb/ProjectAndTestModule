using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankApiServer.Models
{
    public class Depositor
    {
        public string dId { set; get; }
        public string pwd { set; get; }
        public string dName { set; get; }
        public decimal deposit { set; get; }
    }
}