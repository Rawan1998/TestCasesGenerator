using System;
using System.Collections.Generic;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core.Strategies
{
    public class SubtractTestStrategy : OperatorStrategy
    {
        private PlusTestStrategy _plusStrategy = new PlusTestStrategy();

        public override TestCase[] GenerateTests(Variable variable, object constant)
        {
            return this._plusStrategy.GenerateTests(variable, constant);
        }

        public override TestCase[] GenerateTests(object constant, Variable variable)
        {
            return this._plusStrategy.GenerateTests(constant, variable);
        }

        public override TestCase[] GenerateTests(Variable left, Variable right)
        {
            return this._plusStrategy.GenerateTests(left, right);
        }
    }
}
