using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleAppTests
{
    internal class ProcessTests
    {
        private static string DotNetExeName => GetDotnetExe();
        private static string S_DotNetExe => GetS_DotnetExe();

        [Test]
        public void ProcessDllTest()
        {
            string binDirectory = Directory.GetCurrentDirectory();
            string HelloSystemDll = binDirectory.Substring(0, binDirectory.IndexOf("ConsoleApp")) + @"ConsoleApp\asset\HelloSystem.dll";
            Console.WriteLine(HelloSystemDll);
            ProcessStartInfo processStartInfo = new ProcessStartInfo(S_DotNetExe, HelloSystemDll);
            var foo = Process.Start(processStartInfo);
        }

        [Test]
        public void ProcessExeTest()
        {
            string binDirectory = Directory.GetCurrentDirectory();
            string HelloSystemExe = binDirectory.Substring(0, binDirectory.IndexOf("ConsoleApp")) + @"ConsoleApp\asset\HelloSystem.exe";
            Console.WriteLine(HelloSystemExe);
            ProcessStartInfo processStartInfo = new ProcessStartInfo(S_DotNetExe, HelloSystemExe);
            var foo = Process.Start(processStartInfo);
        }

        private static string GetDotnetExe()
        {
            return "dotnet" + (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : "");
        }

        private static string GetS_DotnetExe()
        {
            string installDir = Environment.GetEnvironmentVariable("DOTNET_INSTALL_DIR");
            if (!HasDotNetExe(installDir))
            {
                installDir = Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator).FirstOrDefault(HasDotNetExe);
            }
            return Path.Combine(installDir, DotNetExeName);

            bool HasDotNetExe(string dotnetDir) => dotnetDir != null && File.Exists(Path.Combine(dotnetDir, DotNetExeName));
        }
    }
}
