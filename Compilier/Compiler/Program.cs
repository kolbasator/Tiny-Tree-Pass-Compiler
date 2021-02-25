using System;

namespace Compiler
{
    internal static class Program
    {
        private static void Main()
        {
            const string prog = "[ x y z ] ( 2*3*x + 5*y - 3*z ) / (1 + 3 + 2*2)";
            AbstractSyntaxTree tree = InfixToAstParser.BuildTree(prog); 
            Console.WriteLine("Testing: " + prog);
            
            // in order traversal of AST should give an infix expression
            // and it works for you, gj
            tree.InOrderTraversal(tree);
        }
    }
}
