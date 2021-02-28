using System;
using System.Collections.Generic;
using System.Text;

namespace Compilier
{
    public class AstOperand : AbstractSyntaxTree
    {
        public AstOperand(string status, double value)
        {
            Status = status;
            Value = value.ToString();
        }
    }
}
