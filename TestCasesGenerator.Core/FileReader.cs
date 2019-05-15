using System;
using System.IO;

namespace TestCasesGenerator.Core
{
    public class FileReader
    {
        public String[] ReadFile(String filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllLines(filePath);
            }
            else
            {
                throw new Exception("File Not Found!");
            }
        }
    }
}
