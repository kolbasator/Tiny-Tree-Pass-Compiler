using System;
using System.Collections.Generic;
using System.Text;

namespace Compilier
{
    public class AstOperator : AbstractSyntaxTree
    {
        public AstOperator(string value, AbstractSyntaxTree left, AbstractSyntaxTree right)
        {
            Value = value;
            LeftChild = left;
            RightChild = right;
            Status = "Op";
        }
    }
}
