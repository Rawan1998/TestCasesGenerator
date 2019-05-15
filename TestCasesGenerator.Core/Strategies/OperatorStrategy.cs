using System;
using System.Collections.Generic;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core.Strategies
{
    public abstract class OperatorStrategy
    {
        protected Operand LeftOperand;
        protected Operand RightOperand;

        public TestCase[] GenerateTests(Operand left, Operand right)
        {
            List<TestCase> testCases = new List<TestCase>();
            this.LeftOperand = left;
            this.RightOperand = right;

            if (left.IsVariableInput && right.IsConstant)
            {
                testCases.AddRange(this.GenerateTests(left.Value as Variable, right.Value));
            }
            else if (left.IsConstant && right.IsVariableInput)
            {
                testCases.AddRange(this.GenerateTests(left.Value, right.Value as Variable));
            }
            else if (left.IsVariableInput && right.IsVariableInput)
            {
                testCases.AddRange(this.GenerateTests(left.Value as Variable, right.Value as Variable));
            }

            this.LeftOperand = null;
            this.RightOperand = null;

            return testCases.ToArray();
        }

        public abstract TestCase[] GenerateTests(Variable variable, Object constant);
        public abstract TestCase[] GenerateTests(Object constant, Variable variable);
        public abstract TestCase[] GenerateTests(Variable left, Variable right);
    }
}
