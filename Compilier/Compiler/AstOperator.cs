namespace Compiler
{
    public class AstOperator : AbstractSyntaxTree
    {
        // you have already inherited all from AbstractSyntaxTree, here only constructor
        public AstOperator(string value, AbstractSyntaxTree left, AbstractSyntaxTree right)
        {
            Value = value;
            LeftChild = left;
            RightChild = right;
            Status = "Op";
        }
    }
}