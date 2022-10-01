using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests.Helper
{
    internal class TestBase
    {
        protected string RecordErrorMessage<T>(Func<T> func)
        {
			try
			{
                func.Invoke();
            }
			catch (Exception e)
			{
                return e.ToString();
            }
            return "";
        }
    }
}
