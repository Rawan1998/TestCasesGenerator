using System;
using System.Collections.Generic;

namespace TestCasesGenerator.Core.Structures
{
    public class Operator : Node
    {
        private string _name;
        public string Name => this._name;

        public List<Node> Children => throw new NotImplementedException();

        public Operand LeftOperand;
        public Operand RightOperand;

        public Operator(string name)
        {
            this._name = name;
        }

        public void Push(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
