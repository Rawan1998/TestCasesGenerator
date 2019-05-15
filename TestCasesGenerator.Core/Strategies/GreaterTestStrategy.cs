using System;
using System.Collections.Generic;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core.Strategies
{
    public class GreaterTestStrategy : OperatorStrategy
    {
        public TestCase[] GenerateTests(Operand left, Operand right)
        {
            List<TestCase> testCases = new List<TestCase>();

            if (left.IsVariableInput && right.IsConstant)
            {
                double rValue = Convert.ToDouble((string) right.Value);

                TestCase t1 = new TestCase();
                t1.Inputs.Add((left.Value as Variable).Name, rValue - 1);
                testCases.Add(t1);

                TestCase t2 = new TestCase();
                t2.Inputs.Add((left.Value as Variable).Name, rValue + 1);
                testCases.Add(t2);

                TestCase t3 = new TestCase();
                t3.Inputs.Add((left.Value as Variable).Name, rValue);
                testCases.Add(t3);
            }

            return testCases.ToArray();
        }
    }
}
