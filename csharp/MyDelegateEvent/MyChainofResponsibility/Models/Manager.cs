using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChainofResponsibility.Models
{
    internal class Manager : Management
    {
        public override bool Audit(ApplyContext context)
        {
            if (context.TimeOnHours <= 16)
            {
                context.isApprove = true;
            }
            else
            {
                Console.WriteLine($"trun over to {this.ReportTo.Name}");
                this.ReportTo.Audit(context);
            }
            return context.isApprove;
        }
    }
}
