using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Compilier
{
    public static class Simulator
    {
        public static string PolishNotation=string.Empty;
        public static int Calculate(List<string> pass3,int[] args)
        { 
            var list= InfixToAstParser.ShuntingYardAlgorithm(string.Join("", pass3));
            string patternToFindLetters = @"[a-z]+$";
            List<string> tokens = new List<string>();
            foreach (var element in list)
            {
                if (Regex.IsMatch(element.ToString(), patternToFindLetters))
                { 
                    int value = args[InfixToAstParser.ExpressionArgs.IndexOf(element.ToString())];
                    tokens.Add(value.ToString());
                }
                else
                {
                    tokens.Add(element.ToString());
                }
            } 
            Stack<int> resultStack = new Stack<int>(); 
            foreach (var token in tokens)
            {
                int leftOperand;
                int rightOperand;
                if (InfixToAstParser.IsNumber(token.ToString()))
                {
                    resultStack.Push(int.Parse(token.ToString()));
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
                        leftOperand = resultStack.Pop();
                        resultStack.Push(leftOperand / rightOperand);
                        break; 
                }
            }
            var result = resultStack.Pop();
            return result;
        }
        public static void NodesToPolishNotation(AbstractSyntaxTree tree)
        {
            if (tree != null)
            {
                var newTree = tree as BinOp;
                if(newTree==null)
                {
                    var unOpTree = tree as UnOp;
                    if (unOpTree.Status == "arg")
                    {
                        PolishNotation += InfixToAstParser.ExpressionArgs[unOpTree.Value].ToString();
                    }
                    else
                    {
                        PolishNotation += unOpTree.Value.ToString();
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
}
