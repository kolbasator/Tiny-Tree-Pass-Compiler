using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Compilier
{
    public class Compiler
    {
        public AbstractSyntaxTree FirstPass(string expression)
        {
            return InfixToAstParser.BuildTree(expression);
        }
        public AbstractSyntaxTree SecondPass(AbstractSyntaxTree tree)
        {
            Simulator.Postfix.Clear(); 
            Simulator.NodesToPolishNotation(tree);
            var tokens = InfixToAstParser.ShuntingYardAlgorithm(InfixToAstParser.PostfixToInfix(Simulator.Postfix));
            List<string> resultStack = new List<string>();
            foreach (var token in tokens)
            {
                const string pattern = @"[a-z]+|[0-9]+$";
                if (Regex.IsMatch(token.ToString(), pattern))
                {
                    resultStack.Add(token.ToString());
                    continue;
                }
                string rightOperand = resultStack[resultStack.Count - 1];
                string leftOperand = resultStack[resultStack.Count - 2]; 
                if (!Regex.IsMatch(rightOperand.ToString(), @"[0-9]+$") || !Regex.IsMatch(leftOperand.ToString(), @"[0-9]+$"))
                {
                    resultStack.Add(token);
                    continue;
                }
                resultStack.RemoveAt(resultStack.Count - 1);
                resultStack.RemoveAt(resultStack.Count - 1);
                switch (token)
                {
                    case "+":
                        resultStack.Add(Convert.ToString(Convert.ToDouble(leftOperand) + Convert.ToDouble(rightOperand)));
                        break;
                    case "-":
                        resultStack.Add(Convert.ToString(Convert.ToDouble(leftOperand) - Convert.ToDouble(rightOperand)));
                        break;
                    case "*":
                        resultStack.Add(Convert.ToString(Convert.ToDouble(leftOperand) * Convert.ToDouble(rightOperand)));
                        break;
                    case "/":
                        resultStack.Add(Convert.ToString(Convert.ToDouble(leftOperand) / Convert.ToDouble(rightOperand)));
                        break;
                }
            } 
            return InfixToAstParser.Parse(InfixToAstParser.PostfixToInfix(resultStack));
        }
        public List<string> ThirdPass(AbstractSyntaxTree tree)
        {
            Simulator.Postfix.Clear(); 
            Simulator.NodesToPolishNotation(tree);
            return InfixToAstParser.PostfixToInfix(Simulator.Postfix).Select(x=>Convert.ToString(x)).ToList();

        }
    }
}
