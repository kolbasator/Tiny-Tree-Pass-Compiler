using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Compiler
{
    // keep class name by guide styles, PascalCase
    public static class InfixToAstParser
    {
        // keep static member name by guide styles, PascalCase
        public static List<string> ExpressionArgs;

        private static bool IsOperator(string symbol)
        {
            return symbol == "*" || symbol == "/" || symbol == "-" || symbol == "+";
        }

        private static int Priority(string symbol)
        {
            switch (symbol)
            {
                // here converted to switch
                case "*":
                case "/":
                    return 3;
                case "+":
                case "-":
                    return 2;
                default:
                    return 1;
            }
        }

        public static bool IsNumber(string token)
        {
            // here return inline with temp variable _
            return double.TryParse(token, out _);
        }

        // keep methods naming by guide style, PascalCase
        public static string PostfixToInfix(string expression)
        {
            var stack = new Stack<string>();
            // convert to foreach
            // set normal iteration variable name
            foreach (var token in expression)
            {
                if (token == '*' || token == '/' || token == '+' || token == '-')
                {
                    var s1 = stack.Pop();
                    var s2 = stack.Pop();
                    var temp = "(" + s2 + token + s1 + ")";
                    stack.Push(temp);
                }
                else
                {
                    stack.Push(token + "");
                }
            }

            // return in line, dont waste space
            return stack.Pop();
        }

        public static IEnumerable<string> ShuntingYardAlgorithm(string expr)
        {
            // keep regex pattern in variable
            const string regexPattern = "[a-zA-Z]+|[0-9]+|[+*/()-]";
            var expression = Regex.Replace(expr.Replace(" ", ""), regexPattern, "$0 ").Trim();
            var tokens = expression.Split(null);
            var output = new List<string>();
            var operators = new Stack<string>();

            foreach (var token in tokens)
            {
                if (Regex.IsMatch(token, @"[a-z]+$"))
                {
                    output.Add(token);
                    continue;
                }

                if (IsNumber(token) || token == ".")
                {
                    output.Add(token);
                    continue;
                }

                switch (token)
                {
                    case "(":
                        operators.Push(token);
                        break;
                    case "^":
                    case "*":
                    case "/":
                    case "+":
                    case "-":
                        while (operators.Count > 0 && Priority(token) <= Priority(operators.Peek()))
                        {
                            output.Add(operators.Pop());
                        }

                        operators.Push(token);
                        break;
                    case ")":
                        while (operators.Peek() != "(")
                        {
                            output.Add(operators.Pop());
                        }

                        operators.Pop();
                        break;
                }
            }

            while (operators.Any())
            {
                output.Add(operators.Pop());
            }

            return output;
        }

        public static AstOperator Parse(string expression)
        {
            const string regexPattern = @"[a-zA-Z]+|[0-9]+|[+*/()-\[ \]]";
            var newExpression = Regex.Replace(expression.Replace(" ", ""), regexPattern, "$0 ").Trim();

            // Получаем выражение в польской нотации
            var postfixList = ShuntingYardAlgorithm(newExpression);

            // Создаем стек для узлов
            var treeStack = new Stack<AbstractSyntaxTree>();

            foreach (var token in postfixList)
            {
                if (IsOperator(token))
                {
                    var right = treeStack.Pop();
                    var left = treeStack.Pop();

                    //Если элемент оператор то берем два последних узла из стека и создаем новый узел со значением равным текущему символу 
                    treeStack.Push(new AstOperator(token, left, right));
                    continue;
                }

                if (Regex.IsMatch(token, @"[a-z]+$"))
                {
                    treeStack.Push(new AstOperand("arg", ExpressionArgs.IndexOf(token)));
                    continue;
                }

                if (IsNumber(token))
                {
                    //Если Элемент число то просто добавляем в стек новый узел без дочерних элементов
                    treeStack.Push(new AstOperand("imm", Convert.ToInt32(token)));
                }
            }

            var root = treeStack.Peek();
            treeStack.Pop();
            
            //Последний элемент в стеке и будет корнем нашего дерева
            return (AstOperator) root;
        }

        public static AstOperator BuildTree(string expression)
        {
            const string regexPattern = @"[a-zA-Z]+|[0-9]+|[+*/()-\[ \]]";
            expression = Regex.Replace(expression.Replace(" ", ""), regexPattern, "$0 ").Trim();
            var parts = expression.Split("] ");
            var newExpression = parts[1];
            var args = new List<string>();

            foreach (var l in parts[0])
            {
                if (Regex.IsMatch(l.ToString(), @"[a-z]+$"))
                {
                    args.Add(l.ToString());
                }
            }

            ExpressionArgs = args;
            var postfixList = ShuntingYardAlgorithm(newExpression); //Получаем выражение в польской нотации 
            var treeStack = new Stack<AbstractSyntaxTree>(); //Создаем стек для узлов
            foreach (var token in postfixList)
            {
                if (IsOperator(token))
                {
                    var right = treeStack.Pop();
                    var left = treeStack.Pop();

                    //Если элемент оператор то берем два последних узла из стека и создаем новый узел со значением равным текущему символу 
                    treeStack.Push(new AstOperator(token, left, right));

                    continue;
                }

                if (Regex.IsMatch(token, @"[a-z]+$"))
                {
                    treeStack.Push(new AstOperand("arg", args.IndexOf(token)));
                    continue;
                }

                if (IsNumber(token))
                {
                    //Если Элемент число то просто добавляем в стек новый узел без дочерних элементов
                    treeStack.Push(new AstOperand("imm", Convert.ToInt32(token)));
                }
            }

            var root = treeStack.Peek();
            treeStack.Pop();

            //Последний элемент в стеке и будет корнем нашего дерева
            return (AstOperator) root;
        }
    }
}