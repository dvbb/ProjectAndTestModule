using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChainofResponsibility.Models
{
    internal class Charge : Management
    {
        public override bool Audit(ApplyContext context)
        {
            // 若请假时间大于8小时，则没有批准权限，交由上级审批
            if (context.TimeOnHours <= 8)
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
