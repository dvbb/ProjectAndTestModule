using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChainofResponsibility.Models
{
    /// <summary>
    /// context上下文 是行为型设计模式的标配
    /// 行为型设计模式关注 对象和行为 的分离
    /// 行为的执行需要：参数、返回
    /// </summary>
    internal class ApplyContext
    {
        public ApplyContext()
        {
            Description = "please write your OOF reason here";
            isApprove = false;
        }

        public int TimeOnHours { get; set; }

        public string Description { get; set; }

        public bool isApprove { get; set; }
    }
}
