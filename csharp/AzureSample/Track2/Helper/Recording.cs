using System;
using System.Collections.Generic;
using System.Text;

namespace Track2.Helper
{
    internal static class Recording
    {
        public static string GenerateAssetName(string name)
        {
            return $"{name}0000";
        }
    }
}
