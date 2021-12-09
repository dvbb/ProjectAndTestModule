using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBankApiServer.Models
{
    [Serializable]
    public class AmountChange
    {
        public Guid pId { set; get; }
        public string dId { set; get; }
        public string dName { set; get; }
        public string addr { set; get; }
        public string dType { set; get; }
        public DateTime dTime { set; get; }
        public double rate { set; get; }
        public decimal deposit { set; get; }
        public bool dStatus { set; get; }
    }
}
