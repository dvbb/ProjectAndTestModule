using Microsoft.QualityTools.Testing.Fakes.Instances;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace ConsoleAppTests.Reflection
{
    internal class ReflectionTests
    {
        [OneTimeSetUp]
        public void GlobalSetUp()
        {
            
        }

        [Test]
        public void GetCurrentDll()
        {
            Assembly assembly = Assembly.LoadFrom("ConsoleAppTests.dll");
            Type[] types = assembly.GetTypes();
        }

        [Test]
        [Ignore("System.ArgumentNullException : Value cannot be null. (Parameter 'type')")]
        public void GetExternalDll()
        {
            // Init
            string _dllPath = Directory.GetCurrentDirectory().Replace("ConsoleAppTests\\bin\\Debug\\.netcoreapp,version=v3.1", "") + "asset\\MyTools.dll";
            int[] _numbers = new int[] { 1, 2, 5, 7, 9, 15 };
            
            // road dll file
            Assembly assembly = Assembly.LoadFile(_dllPath);

            Type type = assembly.GetType("MyTools.MyFormat");//Type[] tArray = assembly.GetTypes();
            dynamic dInstance = Activator.CreateInstance(type);
            string str = dInstance.Convert(_numbers);
            Console.WriteLine(str);
        }
    }
}
