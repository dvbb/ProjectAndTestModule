using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        }

        [Test]
        public void DirectoryTest()
        {
            Console.WriteLine("Is [d:\\NewDirectoty] exist? " + Directory.Exists(@"d:\NewDirectoty"));
            Directory.CreateDirectory(@"d:\NewDirectoty");
            Console.WriteLine("CreateDirectory()");
            Console.WriteLine("Is [d:\\NewDirectoty] exist? " + Directory.Exists(@"d:\NewDirectoty"));
            Console.WriteLine();

            string curDirectory = Directory.GetCurrentDirectory();
            string[] fileList = Directory.GetFiles(curDirectory);
            string[] directoryList = Directory.GetDirectories(curDirectory);
            Console.WriteLine("GetCurrentDirectory(): " + curDirectory);
            Console.WriteLine("GetFiles() " + fileList + " " + fileList.Count());   //cur下的所有文件
            Console.WriteLine("GetDirectories() " + directoryList + " " + directoryList.Count()); //cur下的所有子目录
            Console.WriteLine();
        }
    }
}
