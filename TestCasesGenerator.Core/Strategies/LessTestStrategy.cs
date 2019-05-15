using System;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core.Strategies
{
    public class LessTestStrategy : OperatorStrategy
    {
        private GreaterTestStrategy _greaterStrategy = new GreaterTestStrategy();

        public override TestCase[] GenerateTests(Variable variable, object constant)
        {
            return this._greaterStrategy.GenerateTests(constant, variable);
        }

        public override TestCase[] GenerateTests(object constant, Variable variable)
        {
            return this._greaterStrategy.GenerateTests(variable, constant);
        }

        public override TestCase[] GenerateTests(Variable left, Variable right)
        {
            return this._greaterStrategy.GenerateTests(right, left);
        }
    }
}
