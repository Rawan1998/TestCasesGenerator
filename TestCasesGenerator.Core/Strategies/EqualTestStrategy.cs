using System;
using System.Collections.Generic;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core.Strategies
{
    class EqualTestStrategy : OperatorStrategy
    {
        public override TestCase[] GenerateTests(Variable variable, object constant)
        {
            List<TestCase> testCases = new List<TestCase>();

            return testCases.ToArray(); 
        }

        public override TestCase[] GenerateTests(object constant, Variable variable)
        {
            throw new NotImplementedException();
        }

        public override TestCase[] GenerateTests(Variable left, Variable right)
        {
            List<TestCase> testCases = new List<TestCase>();

            return testCases.ToArray();
        }
    }
}
