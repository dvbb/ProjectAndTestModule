using MyChainofResponsibility.Models;
using System;

namespace MyChainofResponsibility
{
    internal class Program
    {
        /// <summary>
        /// 行为型设计模式的核心：封装转移
        /// 类似编写好通用的模板，然后所有的逻辑、操作都从参数中获取
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ApplyContext context = new ApplyContext()
            {
                Description = "It takes three days off to move",
                TimeOnHours = 24
            };

            Management principal = new Principal() { Id = "dfxer1478c5wa", Name = "Snow Pinkman" ,ReportTo = null};
            Management manager = new Manager() { Id = "s4x2a35d7x8", Name = "John Cart", ReportTo = principal };
            Management charge = new Charge() { Id = "sf48x215wx", Name = "Eric Nash", ReportTo = manager };

            // Scenario: 填写一张请假申请表，然后发送给主管.
            charge.Audit(context);
        }
    }
}
