using System;
using System.Collections.Generic;
using System.Text;

namespace Compilier
{
    public class BinOp : Ast
    {
        public string Value;
        public Ast leftChild, rightChild;
        public BinOp(string value,Ast left,Ast right)
        {
            Value = value;
            leftChild = left;
            rightChild = right;
            Status = "Op";
        }
    }
}
