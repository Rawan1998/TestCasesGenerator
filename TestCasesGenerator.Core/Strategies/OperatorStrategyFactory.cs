using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            else
            {
                throw new Exception("Operator strategy not found.");
            }
        }
    }
}
