using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChainofResponsibility.Models
{
    internal abstract class Management
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Management ReportTo { get; set; }

        public abstract bool Audit(ApplyContext context);
    }
}
