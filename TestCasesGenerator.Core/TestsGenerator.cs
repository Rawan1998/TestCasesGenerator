using System;
using TestCasesGenerator.Core.Structures;
using TestCasesGenerator.Core.Strategies;
using System.Collections.Generic;

namespace TestCasesGenerator.Core
{
    public class TestsGenerator
    {
        public TestCase[] GenerateTests(SyntaxTree syntaxTree)
        {
            List<TestCase> testCases = new List<TestCase>();

            foreach(Node child in syntaxTree.GetChildren())
            {
                if (child is ConditionalScope conditionalScope)
                {
                    testCases.AddRange(this.GenerateOperatorTest(conditionalScope.Operator));
                }
                else if (child is Operator op)
                {
                    testCases.AddRange(this.GenerateOperatorTest(op));
                }
            }

            return testCases.ToArray();
        }

        private TestCase[] GenerateOperatorTest(Operator op)
        {
            var factory = new OperatorStrategyFactory();
            return factory.CreateStrategy(op.Name).GenerateTests(op.LeftOperand, op.RightOperand);
        }
    }
}
