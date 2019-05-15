using System;

namespace TestCasesGenerator.Core.Structures
{
    public class ConditionalScope : Scope
    {
        public Operator Operator;

        public ConditionalScope(string name) : base(name)
        {
        }
    }
}
