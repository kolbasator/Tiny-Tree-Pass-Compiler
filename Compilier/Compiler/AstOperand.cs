namespace Compiler
{
    public class AstOperand : AbstractSyntaxTree
    {
        // you have already inherited all from AbstractSyntaxTree, here only constructor
        public AstOperand(string status, int value)
        {
            Status = status;
            Value = value.ToString();
        }
    }
}