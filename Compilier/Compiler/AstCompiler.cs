using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Compiler
{
    public static class AstCompiler
    {
        public static AbstractSyntaxTree FirstPass(string expression)
        { 
            return InfixToAstParser.BuildTree(expression);
        }
        
        public static AbstractSyntaxTree SecondPass(AbstractSyntaxTree tree)
        {
            // dont write literal "", use instead string.Empty
            Simulator.PolishNotation = string.Empty;
            
            Simulator.NodesToPolishNotation(tree);
            var tokens= InfixToAstParser
                .ShuntingYardAlgorithm(InfixToAstParser.PostfixToInfix(Simulator.PolishNotation));
            
            // use var instead of direct typing
            var resultStack = new List<string>();
            
            foreach (var token in tokens)
            {
                if (Regex.IsMatch(token, @"[a-z]+|[0-9]+$"))
                {
                    resultStack.Add(token);
                    continue;
                }
                
                // this section of code was repeated ten times
                var rightOperand = resultStack[^1];
                var leftOperand = resultStack[^2];
                if (!Regex.IsMatch(rightOperand, @"[0-9]+$") || !Regex.IsMatch(leftOperand, @"[0-9]+$"))
                {
                    resultStack.Add(token);
                    continue; 
                }
                resultStack.RemoveAt(resultStack.Count - 1);
                resultStack.RemoveAt(resultStack.Count - 1);
                
                switch (token)
                { 
                    case "+":
                        resultStack.Add(Convert.ToString(Convert.ToInt32(leftOperand) + Convert.ToInt32(rightOperand)));
                        break;
                    case "-":
                        resultStack.Add(Convert.ToString(Convert.ToInt32(leftOperand) - Convert.ToInt32(rightOperand)));
                        break;
                    case "*":
                        resultStack.Add(Convert.ToString(Convert.ToInt32(leftOperand) * Convert.ToInt32(rightOperand)));
                        break;
                    case "/":
                        resultStack.Add(Convert.ToString(Convert.ToInt32(leftOperand) / Convert.ToInt32(rightOperand)));
                        break;
                }
            }
            
            // return directly, dont waste lines of code 
            return InfixToAstParser.Parse(InfixToAstParser.PostfixToInfix(string.Join("", resultStack)));
        }
        public static List<string> ThirdPass(AbstractSyntaxTree tree)
        {
            // dont write literal "", use instead string.Empty
            Simulator.PolishNotation = string.Empty;
            
            Simulator.NodesToPolishNotation(tree);
            return InfixToAstParser.PostfixToInfix(Simulator.PolishNotation).ToCharArray().Select(x=>Convert.ToString(x)).ToList();

        }
    }
}
