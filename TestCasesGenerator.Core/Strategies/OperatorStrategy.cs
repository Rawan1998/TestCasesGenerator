using System;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.Core.Strategies
{
    public interface OperatorStrategy
    {
        TestCase[] GenerateTests(Operand left, Operand right);
    }
}
