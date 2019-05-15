using System;
using System.Collections.Generic;

namespace TestCasesGenerator.Core.Structures
{
    public class TestCase
    {
        public String Name { get; set; }
        public Dictionary<String, object> Inputs { get; set; }
        public object ExpectedOutput { get; set; }

        public TestCase(String name = "")
        {
            this.Name = name;
            this.Inputs = new Dictionary<string, object>();
        }
    }
}
