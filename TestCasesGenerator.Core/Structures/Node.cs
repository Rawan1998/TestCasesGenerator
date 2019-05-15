using System;
using System.Collections.Generic;

namespace TestCasesGenerator.Core.Structures
{
    public interface Node
    {
        String Name { get; }
        List<Node> Children { get; }

        void Push(Node node);
    }
}
