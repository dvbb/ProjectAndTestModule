using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyDelegateEvent
{
    internal static class LinqExtend
    {
        public static List<T> EventWhere<T>(this List<T> list, Func<T, bool> func)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }
            List<T> result = new List<T>();
            foreach (var item in list)
            {
                if (func.Invoke(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 使用IEnumerable 迭代器，实现按需获取、延迟加载
        ///     使用：语法糖 IEnumerable + yield 可以直接使用
        ///     原理：状态机模式(23个设计模式之一)(async-await也是状态机模式)
        ///         
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> EventWhereIEnumerable<T>(this List<T> list, Func<T, bool> func)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }
            foreach (var item in list)
            {
                if (func.Invoke(item))
                {
                    yield return item;
                }
            }
        }
    }
}
