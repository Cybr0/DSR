using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DSRTestTask.Helpers
{
    static class DriverPath
    {
        public static string ChromeDriverPath => TestContext.CurrentContext.TestDirectory
            .Replace(@$"bin{Path.DirectorySeparatorChar}Debug{Path.DirectorySeparatorChar}netcoreapp3.1", "Drivers");
    }
}
