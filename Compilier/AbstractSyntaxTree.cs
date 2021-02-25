using System;
using System.Collections.Generic;
using System.Text;

namespace Compilier
{
    public class AbstractSyntaxTree
    {
        public string Status { get; protected set; }
        public string Value { get; protected set; }
        public AbstractSyntaxTree LeftChild { get; protected set; }
        public AbstractSyntaxTree RightChild { get; protected set; }
    }
}
