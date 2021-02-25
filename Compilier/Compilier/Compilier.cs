﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Compilier
{
    public class Compilier
    {
        public Ast FirstPass(string expression)
        { 
            return InfixToASTParser.BuildTree(expression);
        }
        public Ast SecondPass(Ast tree)
        {
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(tree);
            var tokens= InfixToASTParser.ShuntingYardAlgorithm(InfixToASTParser.PostfixToInfix(Simulator.PolishNotation));
            List<string> resultStack = new List<string>();
            foreach (var token in tokens)
            {
                string leftOperand;
                string rightOperand; 
                if (Regex.IsMatch(token.ToString(), @"[a-z]+|[0-9]+$"))
                {
                    resultStack.Add(token.ToString());
                }
                switch (token)
                { 
                    case "+":
                        rightOperand = resultStack[resultStack.Count-1];
                        leftOperand = resultStack[resultStack.Count - 2];
                        if (!Regex.IsMatch(rightOperand.ToString(), @"[0-9]+$") || !Regex.IsMatch(leftOperand.ToString(), @"[0-9]+$"))
                        {
                            resultStack.Add(token);
                            continue; 
                        }
                        resultStack.RemoveAt(resultStack.Count - 1);
                        resultStack.RemoveAt(resultStack.Count - 1);
                        resultStack.Add(Convert.ToString(Convert.ToInt32(leftOperand) + Convert.ToInt32(rightOperand)));
                        break;
                    case "-":
                        rightOperand = resultStack[resultStack.Count - 1];
                        leftOperand = resultStack[resultStack.Count - 2];
                        if (!Regex.IsMatch(rightOperand.ToString(), @"[0-9]+$") || !Regex.IsMatch(leftOperand.ToString(), @"[0-9]+$"))
                        {
                            resultStack.Add(token);
                            continue;
                        }
                        resultStack.RemoveAt(resultStack.Count - 1);
                        resultStack.RemoveAt(resultStack.Count - 1);
                        resultStack.Add(Convert.ToString(Convert.ToInt32(leftOperand) - Convert.ToInt32(rightOperand)));
                        break;
                    case "*":
                        rightOperand = resultStack[resultStack.Count - 1];
                        leftOperand = resultStack[resultStack.Count - 2];
                        if (!Regex.IsMatch(rightOperand.ToString(), @"[0-9]+$") || !Regex.IsMatch(leftOperand.ToString(), @"[0-9]+$"))
                        {
                            resultStack.Add(token);
                            continue;
                        }
                        resultStack.RemoveAt(resultStack.Count - 1);
                        resultStack.RemoveAt(resultStack.Count - 1);
                        resultStack.Add(Convert.ToString(Convert.ToInt32(leftOperand) * Convert.ToInt32(rightOperand)));
                        break;
                    case "/":
                        rightOperand = resultStack[resultStack.Count - 1];
                        leftOperand = resultStack[resultStack.Count - 2];
                        if (!Regex.IsMatch(rightOperand.ToString(), @"[0-9]+$") || !Regex.IsMatch(leftOperand.ToString(), @"[0-9]+$"))
                        {
                            resultStack.Add(token);
                            continue;
                        }
                        resultStack.RemoveAt(resultStack.Count - 1);
                        resultStack.RemoveAt(resultStack.Count - 1);
                        resultStack.Add(Convert.ToString(Convert.ToInt32(leftOperand) / Convert.ToInt32(rightOperand)));
                        break;
                }
            }
            var result = InfixToASTParser.Parse(InfixToASTParser.PostfixToInfix(string.Join("", resultStack)));
            return result;
        }
        public List<string> ThirdPass(Ast tree)
        {
            Simulator.PolishNotation = "";
            Simulator.NodesToPolishNotation(tree);
            return InfixToASTParser.PostfixToInfix(Simulator.PolishNotation).ToCharArray().Select(x=>Convert.ToString(x)).ToList();

        }
    }
}
