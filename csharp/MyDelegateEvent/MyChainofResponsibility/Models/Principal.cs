using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChainofResponsibility.Models
{
    internal class Principal : Management
    {
        public override bool Audit(ApplyContext context)
        {
            Console.WriteLine($"{this.Name} approved this request.");
            context.isApprove = true;
            return context.isApprove;
        }
    }
}
