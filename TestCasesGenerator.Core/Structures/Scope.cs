using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCasesGenerator.Core.Structures
{
    public class Scope : Node
    {
        private string _name;
        public string Name => this._name;

        private List<Node> _children;
        public List<Node> Children => this._children;

        public List<Variable> Variables;

        public Scope(string name)
        {
            this._name = name;
            this._children = new List<Node>();
            this.Variables = new List<Variable>();
        }

        public void Push(Node node)
        {
            this._children.Add(node);
        }

        public Variable FindVariable(String variableName)
        {
            foreach (Variable var in this.Variables)
            {
                if (var.Name == variableName)
                {
                    return var;
                }
            }

            return null;
        }
    }
}
