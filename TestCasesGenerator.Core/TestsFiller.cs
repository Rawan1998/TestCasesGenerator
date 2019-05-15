using System;
using System.Collections.Generic;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core
{
    public class TestsFiller
    {
        public TestCase[] Fill(TestCase[] testCases, Variable[] inputVariables)
        {
            List<TestCase> cases = new List<TestCase>(testCases);

            foreach(TestCase testCase in cases)
            {
                foreach(Variable variable in inputVariables)
                {
                    if (testCase.Inputs.ContainsKey(variable.Name) == false)
                    {
                        testCase.Inputs.Add(variable.Name, new Random().Next(10, 1000));
                    }
                }
            }

            return cases.ToArray();
        }
    }
}
