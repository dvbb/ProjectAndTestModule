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

            Console.WriteLine("\ndiff with Combine() and Join()");
            string tempPath1 = @"d:\repo\";
            string tempPath2 = @"d:\repo\hello.txt";
            Console.WriteLine($"path1: {tempPath1}");
            Console.WriteLine($"path2: {tempPath2}");
            Console.WriteLine("Combine(): " + Path.Combine(tempPath1, tempPath2));
            Console.WriteLine("Join(): " + Path.Join(tempPath1, tempPath2));
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

        [Test]
        public void FileTest()
        {
            string path1 = @"d:\newFile1.txt";
            string path2 = @"d:\newFile2.txt";

            // File.Open()
            FileStream fileStream = File.Open(path1, FileMode.Append);
            byte[] Info = { (byte)'h', (byte)'e', (byte)'l', (byte)'l', (byte)'o' };
            fileStream.Write(Info, 0, Info.Length);
            fileStream.Close();

            // File.Create()
            FileStream fileStream2 = File.Create(path2);
            fileStream2.Close();

            // File.Exists()
            Console.WriteLine(path1 + " does exists? " + File.Exists(path1));

            // File.Delete()
            File.Delete(path1);
            File.Delete(path2);
            Console.WriteLine();
            Console.WriteLine($"Delete:{path1}");
            Console.WriteLine(path1 + " does exists? " + File.Exists(path1));

        }
    }
}
