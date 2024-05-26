using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello world.");
            string binDirectory = Directory.GetCurrentDirectory();
            string HelloSystemDll = binDirectory.Substring(0, binDirectory.IndexOf("ConsoleApp")) + @"ConsoleApp\asset\HelloSystem.dll";
            string HelloSystemExe = binDirectory.Substring(0, binDirectory.IndexOf("ConsoleApp")) + @"ConsoleApp\asset\HelloSystem.exe";
            Console.WriteLine(HelloSystemDll);
            Console.WriteLine(HelloSystemExe);
            //ProcessStartInfo processStartInfo = new ProcessStartInfo("C:\\Program Files\\dotnet\\dotnet.exe", HelloSystemDll);
            ProcessStartInfo processStartInfo = new ProcessStartInfo(HelloSystemExe);
            var foo = Process.Start(processStartInfo);
        }
    }
}
