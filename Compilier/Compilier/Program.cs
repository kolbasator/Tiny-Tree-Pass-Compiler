using System;

namespace Compilier
{
    class Program
    {
        static void Main(string[] args)
        {
            string prog = "[ x ] 2*( x + 1 )";
            Ast tree = InfixToASTParser.BuildTree(prog); 
            Console.WriteLine("Testing: " + prog);
        }
    }
}
