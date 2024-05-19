using System;
using System.Collections.Generic;
using System.Text;

namespace MyDelegateEvent
{
    internal class LambdaClosure
    {
        public static Action<string?> MyConsoleWrite()
        {
            string msg = "";
            return (string info) =>
            {
                msg = info ?? msg;
                Console.WriteLine(msg);
            };
        }

        public static Func <decimal?, decimal? , decimal?> Add(decimal? d1, decimal? d2)
        {
            decimal? oldD1 = 0;
            decimal? oldD2 = 0;
            return (decimal? d1, decimal? d2) =>
            {
                oldD1 = d1 ?? oldD1;
                oldD2 = d2 ?? oldD2;
                var result = oldD1 + oldD2;
                return result;
            };
        }
    }
}
