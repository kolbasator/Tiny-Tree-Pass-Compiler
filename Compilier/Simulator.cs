using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Compilier
{
    public static class Simulator
    { 
        public static string PolishNotation { get; set; }
        public static List<string> Postfix=new List<string>();
        public static double Calculate(string expression, double[] args)
        {
            Compiler compiler = new Compiler();
            return Simulate(compiler.ThirdPass(compiler.SecondPass(compiler.FirstPass(expression))), args);
        }
        public static double Simulate(List<string> pass3, double[] args)
        {
            var list = InfixToAstParser.ShuntingYardAlgorithm(string.Join("", pass3));
            string patternToFindLetters = @"[a-z]+$";
            List<string> tokens = new List<string>();
            foreach (var element in list)
            {
                if (Regex.IsMatch(element.ToString(), patternToFindLetters))
                {
                    double value = args[InfixToAstParser.ExpressionArgs.IndexOf(element.ToString())];
                    tokens.Add(value.ToString());
                }
                else
                {
                    tokens.Add(element.ToString());
                }
            }
            Stack<double> resultStack = new Stack<double>();
            foreach (var token in tokens)
            {
                double leftOperand;
                double rightOperand;
                if (InfixToAstParser.IsNumber(token.ToString()))
                {
                    resultStack.Push(double.Parse(token.ToString()));
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
                if (!(tree is AstOperator newTree))
                {
                    var unOpTree = tree as AstOperand;
                    if (unOpTree.Status == "arg")
                    {
                        Postfix.Add(InfixToAstParser.ExpressionArgs[int.Parse(unOpTree.Value)]);
                        PolishNotation += InfixToAstParser.ExpressionArgs[int.Parse(unOpTree.Value)];
                    }
                    else
                    {
                        Postfix.Add(unOpTree.Value);
                        PolishNotation += unOpTree.Value;
                    }
                }
                else if (newTree.LeftChild != null && newTree.RightChild != null)
                {
                    NodesToPolishNotation(newTree.LeftChild);
                    NodesToPolishNotation(newTree.RightChild);
                    Postfix.Add(newTree.Value);
                    PolishNotation += newTree.Value;
                }
            }
        }
    }
}
