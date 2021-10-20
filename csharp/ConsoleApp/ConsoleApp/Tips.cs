using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class Tips
    {
        public void UseEnvironment()
        {
            // please use Enviroment Variable to store important information
            var OS_message = Environment.GetEnvironmentVariable("OS");
            var ConnStr = Environment.GetEnvironmentVariable("ConnStr");
            var SqlPassword = Environment.GetEnvironmentVariable("SqlPassword");
            Console.WriteLine(OS_message);
            Console.WriteLine(ConnStr);
            Console.WriteLine(SqlPassword);
        }
    }
}
