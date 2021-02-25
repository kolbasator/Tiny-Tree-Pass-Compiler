using System;
using System.Collections.Generic;
using System.Text;

namespace Compilier
{
    public class BinOp : AbstractSyntaxTree
    {
        public string Value;
        public AbstractSyntaxTree LeftChild, RightChild;
        public BinOp(string value,AbstractSyntaxTree left,AbstractSyntaxTree right)
        {
            Value = value;
            LeftChild = left;
            RightChild = right;
            Status = "Op";
        }
    }
}
