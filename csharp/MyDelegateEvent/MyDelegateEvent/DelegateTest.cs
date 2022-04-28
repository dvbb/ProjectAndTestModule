using System;
using System.Collections.Generic;
using System.Text;

namespace MyDelegateEvent
{
    // 委托本质是一个类，继承至[System.MulticastDelegate]
    internal class DelegateTest // : System.MulticastDelegate
    {
        public delegate void NoReturnNoPara();
        public delegate void NoReturnWithPara(int count);
        public delegate string HaveReturnNoPara();
        public delegate string HaveReturnWithPara(ref int count, out string name);

        public void DoNothing() { }
        public void DoNothingWithPara(int count) { }
        public string ReturnStr() { return ""; }
        public string ReturnStrWithPara(ref int count, out string name) { name = ""; return ""; }

        public void Show()
        {
            // 委托实例化。传入一个方法名称（带括号的是方法调用结果），方法的参数列表和返回值类型与委托一致。
            NoReturnNoPara method1 = new NoReturnNoPara(this.DoNothing);
            NoReturnWithPara method2 = new NoReturnWithPara(this.DoNothingWithPara);
            HaveReturnNoPara method3 = new HaveReturnNoPara(this.ReturnStr);
            HaveReturnWithPara method4 = new HaveReturnWithPara(this.ReturnStrWithPara);

            // 委托的调用。Invoke()时参数与委托声明时一致。
            method1.Invoke();
            method1.BeginInvoke(null,null);// BeginInvoke( AsyncCallback callback, object @object)
            method1.EndInvoke(null); // EndInvoke( IAsyncResult result)

            method2.Invoke(2);
            method3.Invoke();
            int count = 0;
            string name = "";
            method4.Invoke(ref count,out name);
        }
    }
}
