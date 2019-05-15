using System;
using System.Collections.Generic;
using System.Linq;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core
{
    public class TestsMatcher
    {
        public TestCase[] Match(TestCase[] testCases)
        {
            List<TestCase> cases = new List<TestCase>();

            foreach(TestCase aTest in testCases)
            {
                foreach(TestCase bTest in testCases)
                {
                    TestCase test = new TestCase();
                    test.Inputs = new Dictionary<string, object>(aTest.Inputs);
                    
                    foreach(KeyValuePair<string, object> input in test.Inputs)
                    {
                        if (test.Inputs.ContainsKey(input.Key) == false)
                        {
                            test.Inputs.Add(input.Key, input.Value);
                        }
                    }

                    cases.Add(test);
                }
            }

            return this.RemoveDuplicates(cases.ToArray());
        }

        private TestCase[] RemoveDuplicates(TestCase[] testCases)
        {
            List<TestCase> cases = new List<TestCase>(testCases);

            for (int i = 0; i < cases.Count; ++i)
            {
                for (int j = i + 1; j < cases.Count; ++j)
                {
                    if (this.HasEqualInputs(cases[i], cases[j]))
                    {
                        cases.Remove(cases[j]);
                        j--;
                    }
                }
            }

            return cases.ToArray();
        }

        private bool HasEqualInputs(TestCase a, TestCase b)
        {
            var unmatchedInputs = a.Inputs.Where(entry => !b.Inputs.ContainsKey(entry.Key) || b.Inputs[entry.Key] != entry.Value)
                .ToDictionary(entry => entry.Key, entry => entry.Value);

            return unmatchedInputs.Count == 0;
        }
    }
}
