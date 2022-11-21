using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleAppTests
{
    /// <summary>
    /// Syntactic sugar for question mark
    /// </summary>
    public class QuestionMarkTests
    {
        [Test]
        public void SingleQuestionMark()
        {
           int? number = null;
        }
    }
}
