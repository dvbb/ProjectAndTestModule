using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace ConsoleAppTests
{
    internal class UriTests
    {
        private const string _uriString1 = @"http://www.google.com";
        private const string _uriString2 = @"www.google.com";
        private const string _uriString3 = @"c:\\directory\filename";
        private const string _uriString4 = @"file://c:/directory/filename";
        private const string _uriString5 = @"http:\\host/path/file";

        [Test]
        public void TryCreate()
        {
            Uri uri;
            bool flag;
            flag = Uri.TryCreate(_uriString1, UriKind.Absolute, out uri);
            Show(uri, flag);

            flag = Uri.TryCreate(_uriString2, UriKind.Absolute, out uri);
            Show(uri, flag);

            flag = Uri.TryCreate(_uriString3, UriKind.Absolute, out uri);
            Show(uri, flag);

            flag = Uri.TryCreate(_uriString4, UriKind.Absolute, out uri);
            Show(uri, flag);

            flag = Uri.TryCreate(_uriString5, UriKind.Absolute, out uri);
            Show(uri, flag);
        }

        [Test]
        public void IsWellFormedUriString()
        {
            Uri uri;
            bool flag;
            flag = Uri.IsWellFormedUriString(_uriString1, UriKind.Absolute);
            uri = flag ? new Uri(_uriString1) : null;
            Show(uri, flag);

            flag = Uri.IsWellFormedUriString(_uriString2, UriKind.Absolute);
            uri = flag ? new Uri(_uriString2) : null;
            Show(uri, flag);

            flag = Uri.IsWellFormedUriString(_uriString3, UriKind.Absolute);
            uri = flag ? new Uri(_uriString3) : null;
            Show(uri, flag);

            flag = Uri.IsWellFormedUriString(_uriString4, UriKind.Absolute);
            uri = flag ? new Uri(_uriString4) : null;
            Show(uri, flag);

            flag = Uri.IsWellFormedUriString(_uriString5, UriKind.Absolute);
            uri = flag ? new Uri(_uriString5) : null;
            Show(uri, flag);
        }

        private void Show(Uri uri, bool flag)
        {
            Console.WriteLine("flag:" + flag);
            Console.WriteLine("uri:" + uri);
            Console.WriteLine();
        }
    }
}
