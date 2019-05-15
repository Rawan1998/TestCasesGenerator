using System;
using TestCasesGenerator.Core.Structures;
using TestCasesGenerator.Core.Strategies;
using System.Collections.Generic;

namespace TestCasesGenerator.Core
{
    public class TestsGenerator
    {
        public TestCase[] GenerateTests(string filePath)
        {
            var reader = new FileReader();
            String[] lines = reader.ReadFile(filePath);
            var parser = new FileParser();
            SyntaxTree syntaxTree = parser.Parse(lines);

            var generator = new TestsGenerator();
            TestCase[] testCases = generator.GenerateTests(syntaxTree);

            var matcher = new TestsMatcher();
            testCases = matcher.Match(testCases);

            var filler = new TestsFiller();
            testCases = filler.Fill(testCases, syntaxTree.InputVariables.ToArray());

            return testCases;
        }

        private TestCase[] GenerateTests(SyntaxTree syntaxTree)
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
