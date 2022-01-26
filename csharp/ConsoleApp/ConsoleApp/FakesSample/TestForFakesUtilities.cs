using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.FakesSample
{
    public static class TestForFakesUtilities
    {
        public static int GetRandomNumber()
        {
            Random random = new Random();
            return random.Next(99);
        }

        public static string GetRandomString()
        {
            Random random = new Random();
            int count = random.Next(99);
            return count.ToString();
        }

        public static string GetRandomString(int count)
        {
            return count.ToString();
        }
    }
}
