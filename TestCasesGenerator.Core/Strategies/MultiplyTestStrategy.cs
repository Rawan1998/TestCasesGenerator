using System;
using System.Collections.Generic;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core.Strategies
{
    public class MultiplyTestStrategy : OperatorStrategy
    {
        public override TestCase[] GenerateTests(Variable variable, object constant)
        {
            List<TestCase> testCases = new List<TestCase>();

            double rValue = double.Parse((string)constant);

            TestCase t1 = new TestCase();
            t1.Inputs.Add(variable.Name, 0);
            testCases.Add(t1);

            TestCase t2 = new TestCase();
            t2.Inputs.Add(variable.Name, 1);
            testCases.Add(t2);

            TestCase t3 = new TestCase();
            t3.Inputs.Add(variable.Name, new Random().Next(10, 1000));
            testCases.Add(t3);

            return testCases.ToArray();
        }

        public override TestCase[] GenerateTests(object constant, Variable variable)
        {
            return this.GenerateTests(variable, constant);
        }

        public override TestCase[] GenerateTests(Variable left, Variable right)
        {
            List<TestCase> testCases = new List<TestCase>();
            int rValue = new Random().Next(10, 1000);

            TestCase t1 = new TestCase();
            t1.Inputs.Add(left.Name, 0);
            t1.Inputs.Add(right.Name, rValue);
            testCases.Add(t1);

            TestCase t2 = new TestCase();
            t2.Inputs.Add(left.Name, 1);
            t2.Inputs.Add(right.Name, rValue);
            testCases.Add(t2);

            TestCase t3 = new TestCase();
            t3.Inputs.Add(left.Name, new Random().Next(10, 1000));
            t3.Inputs.Add(right.Name, rValue);
            testCases.Add(t3);

            TestCase t4 = new TestCase();
            t4.Inputs.Add(left.Name, rValue);
            t4.Inputs.Add(right.Name, 0);
            testCases.Add(t4);

            TestCase t5 = new TestCase();
            t5.Inputs.Add(left.Name, rValue);
            t5.Inputs.Add(right.Name, 1);
            testCases.Add(t5);

            return testCases.ToArray();
        }
    }
}
