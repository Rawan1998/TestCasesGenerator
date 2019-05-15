using System;
using System.Collections.Generic;

namespace TestCasesGenerator.Core.Structures
{
    public class Function : Scope
    {
        public List<Variable> Parameters;

        public Function() : base("function")
        {
            this.Parameters = new List<Variable>();
        }
    }
}
