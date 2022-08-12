using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MyAttribute
{
    public static class AopExtend
    {
        /// <summary>
        /// 通用的验证方法，如果属性标注了[DataRequired],则验证该属性是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool Validate<T>(this T t)
        {
            // 获取类型
            Type type = t.GetType();

            // 获取所有的property
            foreach (var prop in type.GetProperties())
            {
                // 验证该property是否定义了[DataRequired]
                if (prop.IsDefined(typeof(DataRequiredAttribute), true))
                {
                    object value = prop.GetValue(t);
                    if (value ==null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
