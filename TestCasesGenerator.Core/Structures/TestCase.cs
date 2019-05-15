using System;
using System.Collections.Generic;

namespace TestCasesGenerator.Core.Structures
{
    public class TestCase
    {
        public String Name;
        public Dictionary<String, object> Inputs;
        public object ExpectedOutput;

        public TestCase(String name = "")
        {
            this.Name = name;
            this.Inputs = new Dictionary<string, object>();
        }
    }
}
