using System;
using System.Collections.Generic;
using System.Text;

namespace MyAttribute
{
    /// <summary>
    /// 特性：继承至Attribute父类，通常命名为XXAttribute，引用时可直接[XX]
    /// [AttributeUsage]是对Attribute做约束的，下方定义的均为原来的默认值。
    ///     AttributeTargets: enum，定义特性的范围，默认为All，可以修饰class、property、Delegate...
    ///     AllowMultiple: 是否可以多次声明，例如[Custom] [Cutom("")] [Custom(111)]
    ///     Inherited: 该特性是否可以被子类继承
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    internal class CustomAttribute : Attribute
    {

    }
}
