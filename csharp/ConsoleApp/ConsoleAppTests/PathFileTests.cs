using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleAppTests
{
    public class PathFileTests
    {
        [Test]
        public void PathTest()
        {
            string file = "testhost.exe";
            string path = Path.GetFullPath(file);
            Console.WriteLine("file name: " + file);
            Console.WriteLine("file path: " + path);

            string filename = Path.GetFileName(path);
            string extension = Path.GetExtension(path);
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            string dir = Path.GetDirectoryName(path);
            string root = Path.GetPathRoot(path);

            Console.WriteLine("GetFileName(): " + filename);
            Console.WriteLine("GetExtension(): " + extension);
            Console.WriteLine("GetFileNameWithoutExtension(): " + nameWithoutExtension);
            Console.WriteLine("GetDirectoryName(): " + dir);
            Console.WriteLine("GetPathRoot(): " + root);
            //string binDirectory = Directory.GetCurrentDirectory();
        }
    }
}
