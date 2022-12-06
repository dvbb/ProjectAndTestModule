using System;
using System.Collections.Generic;
using System.Text;

namespace Track2.Helper
{
    internal static class Recording
    {
        public static string GenerateAssetName(string name)
        {
            Random random = new Random();
            return $"{name}{random.Next(9999)}";
        }
    }
}
