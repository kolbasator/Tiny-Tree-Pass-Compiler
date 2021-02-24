using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Compilier
{
    public static class Simulator
    {
        public static string PolishNotation="";
        public static int Simulate(List<string> pass3,int[] args)
        { 
            var list= InfixToASTParser.ShuntingYardAlgorithm(string.Join("", pass3));
            List<string> tokens = new List<string>();
            foreach (var g in list)
            {
                if (Regex.IsMatch(g.ToString(), @"[a-z]+$"))
                { 
                    int value = args[InfixToASTParser.expressionArgs.IndexOf(g.ToString())];
                    tokens.Add(value.ToString());
                }
                else
                {
                    tokens.Add(g.ToString());
                }
            } 
            Stack<int> resultStack = new Stack<int>(); 
            foreach (var token in tokens)
            {
                int leftOperand;
                int rightOperand;
                if (InfixToASTParser.IsNumber(token.ToString()))
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
        public static void NodesToPolishNotation(Ast tree)
        {
            if (tree != null)
            {
                var newTree = tree as BinOp;
                if(newTree==null)
                {
                    var unOpTree = tree as UnOp;
                    if (unOpTree.Status == "arg")
                    {
                        PolishNotation += InfixToASTParser.expressionArgs[unOpTree.Value].ToString();
                    }
                    else
                    {
                        PolishNotation += unOpTree.Value.ToString();
                    }
                }
                else if (newTree.leftChild != null && newTree.rightChild != null)
                {
                    NodesToPolishNotation(newTree.leftChild);
                    NodesToPolishNotation(newTree.rightChild);
                    PolishNotation += newTree.Value;
                }
            }
        }
    }
}
