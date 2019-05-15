using System;

namespace TestCasesGenerator.Core.Strategies
{
    public class OperatorStrategyFactory
    {
        public OperatorStrategy CreateStrategy(String op)
        {
            if (op == ">")
            {
                return new GreaterTestStrategy();
            }
            else if (op == ">=")
            {
                return new GreaterOrEqualTestStrategy();
            }
            else if (op == "<")
            {
                return new LessTestStrategy();
            }
            else if (op == "<=")
            {
                return new LessOrEqualTestStrategy();
            }
            else if (op == "==")
            {
                return new EqualityTestStrategy();
            }
            else if (op == "*")
            {
                return new MultiplyTestStrategy();
            }
            else if (op == "/")
            {
                return new DivisionTestStrategy();
            }
            else if (op == "+")
            {
                return new PlusTestStrategy();
            }
            else if (op == "-")
            {
                return new SubtractTestStrategy();
            }
            else if (op == "=")
            {
                return new EqualTestStrategy();
            }
            else
            {
                throw new Exception("Operator strategy not found.");
            }
        }
    }
}
