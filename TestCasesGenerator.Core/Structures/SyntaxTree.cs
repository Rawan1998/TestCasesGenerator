using System;
using System.Collections;
using System.Collections.Generic;

namespace TestCasesGenerator.Core.Structures
{
    public class SyntaxTree
    {
        public Function Root;
        public List<Variable> InputVariables;

        public SyntaxTree(Function root, List<Variable> inputVariables)
        {
            this.Root = root;
            this.InputVariables = inputVariables;
        }

        public IEnumerable<Node> GetChildren(Node node = null)
        {
            if (node == null)
            {
                node = this.Root;
            }

            yield return node;
            foreach(Node child in node.Children)
            {
                if (child is Scope)
                {
                    foreach (Node parsedNode in this.GetChildren(child))
                    {
                        yield return parsedNode;
                    }
                }
            }
        }
    }
}
