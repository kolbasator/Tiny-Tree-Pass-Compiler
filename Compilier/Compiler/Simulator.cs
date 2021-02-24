using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Compiler
{
    public static class Simulator
    {
        // dont write literal "", use instead string.Empty
        public static string PolishNotation = string.Empty;
        
        // keep naming guide style for methods, PascalCase
        public static int Simulate(IEnumerable<string> pass3, int[] args)
        {
            // use var instead of direct typing
            var postfixList = InfixToAstParser.ShuntingYardAlgorithm(string.Join("", pass3));
            
            // use var instead of direct typing
            var tokens = new List<string>();
            
            // name loop variables in self-understand way
            foreach (var token in postfixList)
            {
                if (Regex.IsMatch(token, @"[a-z]+$"))
                {
                    var value = args[InfixToAstParser.ExpressionArgs.IndexOf(token)];
                    tokens.Add(value.ToString());
                }
                else
                {
                    tokens.Add(token);
                }
            }

            // use var instead of direct typing
            var resultStack = new Stack<int>();
            
            foreach (var token in tokens)
            {
                int rightOperand;
                if (InfixToAstParser.IsNumber(token))
                {
                    resultStack.Push(int.Parse(token));
                }

                switch (token)
                {
                    case "+":
                        resultStack.Push(resultStack.Pop() + resultStack.Pop());
                        break;
                    case "-":
                        rightOperand = resultStack.Pop();
                        resultStack.Push(resultStack.Pop() - rightOperand);
                        break;
                    case "*":
                        resultStack.Push(resultStack.Pop() * resultStack.Pop());
                        break;
                    case "/":
                        rightOperand = resultStack.Pop();
                        var leftOperand = resultStack.Pop();
                        resultStack.Push(leftOperand / rightOperand);
                        break;
                }
            }

            var result = resultStack.Pop();
            return result;
        }

        // keep naming guide style for methods, PascalCase
        public static void NodesToPolishNotation(AbstractSyntaxTree tree)
        {
            if (tree == null) return;
            if (!(tree is AstOperator newTree))
            {
                var unOpTree = tree as AstOperand;
                // conditional access in case of null
                if (unOpTree?.Status == "arg")
                {
                    PolishNotation += InfixToAstParser.ExpressionArgs[int.Parse(unOpTree.Value)];
                }
                else
                {
                    // conditional access in case of null
                    PolishNotation += unOpTree?.Value;
                }
            }
            else if (newTree.LeftChild != null && newTree.RightChild != null)
            {
                NodesToPolishNotation(newTree.LeftChild);
                NodesToPolishNotation(newTree.RightChild);
                PolishNotation += newTree.Value;
            }
        }
    }
}