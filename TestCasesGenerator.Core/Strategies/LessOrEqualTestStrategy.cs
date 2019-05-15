using System;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core.Strategies
{
    public class LessOrEqualTestStrategy : OperatorStrategy
    {
        private LessTestStrategy _lessStrategy = new LessTestStrategy();

        public override TestCase[] GenerateTests(Variable variable, object constant)
        {
            return this._lessStrategy.GenerateTests(variable, constant);
        }

        public override TestCase[] GenerateTests(object constant, Variable variable)
        {
            return this._lessStrategy.GenerateTests(constant, variable);
        }

        public override TestCase[] GenerateTests(Variable left, Variable right)
        {
            return this._lessStrategy.GenerateTests(left, right);
        }
    }
}
