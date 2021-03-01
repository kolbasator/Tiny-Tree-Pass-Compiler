using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Compilier
{
    public class InfixToAstParser
    {
        public static List<string> ExpressionArgs;
        public static bool IsOperator(string symbol)
        {
            return symbol == "*" || symbol == "/" || symbol == "-" || symbol == "+";
        }
        static int Priority(string symbol)
        {
            if (symbol == "*" || symbol == "/")
                return 3;
            else if (symbol == "+" || symbol == "-")
                return 2;
            else if (symbol == "(")
                return 1;
            return -1;
        }
        public static bool IsNumber(string n)
        {
            double retNum;
            bool isNumeric = double.TryParse(n, out retNum);
            return isNumeric;
        }
        public static string PostfixToInfix(List<string> expression)
        { 
            Stack<string> resultStack = new Stack<string>();
            for (int i = 0; i < expression.Count; i++)
            {
                string c = expression[i];
                if (c == "*" || c == "/" || c == "+"|| c == "-")
                {
                    string s1 = resultStack.Pop();
                    string s2 = resultStack.Pop();
                    string temp = "(" + s2 + c + s1 + ")";
                    resultStack.Push(temp);
                }
                else
                {
                    resultStack.Push(c + "");
                }
            }
            string result = resultStack.Pop();
            return result;
        }
        public static List<string> ShuntingYardAlgorithm(string expr)
        {
            var expression = Regex.Replace(expr.Replace(" ",string.Empty), @"[+\-*/()]", " $& ");
            string[] tokens = expression.Split(null);
            List<string> output = new List<string>();
            Stack<string> operators = new Stack<string>();
            for (int i = 0; i < tokens.Length; i++)
            {
                if (Regex.IsMatch(tokens[i].ToString(), @"[a-z]+$"))
                {
                    output.Add(tokens[i]);
                    continue;
                }
                if (IsNumber(tokens[i]) || tokens[i] == ".")
                {
                    output.Add(tokens[i]);
                    continue;
                }
                switch (tokens[i])
                {
                    case "(":
                        operators.Push(tokens[i]);
                        break;
                    case "*":
                    case "/":
                    case "+":
                    case "-":
                        while (operators.Count > 0 && Priority(tokens[i]) <= Priority(operators.Peek().ToString()))
                        {
                            output.Add(operators.Pop().ToString());
                        }
                        operators.Push(tokens[i]);
                        break;
                    case ")":
                        while (operators.Peek() != "(")
                        {
                            output.Add(operators.Pop().ToString());
                        }
                        operators.Pop();
                        break;
                }
            }
            while (operators.Any())
            {
                output.Add(operators.Pop().ToString());
            }
            return output;
        }
        public static AstOperator Parse(string expression)
        { 
            var newExpression = Regex.Replace(expression.Replace(" ", string.Empty), @"[+\-*/()]", " $& "); 
            var postfix = ShuntingYardAlgorithm(newExpression);//Получаем выражение в польской нотации 
            Stack<AbstractSyntaxTree> st = new Stack<AbstractSyntaxTree>();//Создаем стек для узлов
            AbstractSyntaxTree t, t1, t2;
            for (int i = 0; i < postfix.Count; i++)
            {
                if (IsOperator(postfix[i]))
                {
                    t1 = st.Pop();
                    t2 = st.Pop();
                    t = new AstOperator(postfix[i].ToString(), t2, t1); //Если элемент оператор то берем два последних узла из стека и создаем новый узел со значением равным текущему символу 
                    st.Push(t);
                }
                else if (Regex.IsMatch(postfix[i].ToString(), @"[a-z]+$"))
                {
                    t = new AstOperand("arg", ExpressionArgs.IndexOf(postfix[i]));
                    st.Push(t);
                }
                else if (IsNumber(postfix[i]))
                {
                    t = new AstOperand("imm", Convert.ToDouble(postfix[i]));//Если Элемент число то просто добавляем в стек новый узел без дочерних элементов
                    st.Push(t);
                }
            }
            t = st.Peek();
            st.Pop();
            return (AstOperator)t;//Последний элемент в стеке и будет корнем нашего дерева
        }
        public static AstOperator BuildTree(string expression)
        {  
            expression = Regex.Replace(expression.Replace(" ", string.Empty), @"[+\-*/()]", " $& "); 
            string[] parts = expression.Split("] ");
            string newExpression = parts[1];
            List<string> args = new List<string>();
            foreach (var element in parts[0])
            {
                if (Regex.IsMatch(element.ToString(), @"[a-z]+$"))
                {
                    args.Add(element.ToString());
                }
            }
            ExpressionArgs = args;
            var postfix = ShuntingYardAlgorithm(newExpression);//Получаем выражение в польской нотации 
            Stack<AbstractSyntaxTree> st = new Stack<AbstractSyntaxTree>();//Создаем стек для узлов
            AbstractSyntaxTree t, t1, t2;
            for (int i = 0; i < postfix.Count; i++)
            {
                if (IsOperator(postfix[i]))
                {
                    t1 = st.Pop();
                    t2 = st.Pop();
                    t = new AstOperator(postfix[i].ToString(), t2, t1); //Если элемент оператор то берем два последних узла из стека и создаем новый узел со значением равным текущему символу 
                    st.Push(t);
                }
                else if (Regex.IsMatch(postfix[i].ToString(), @"[a-z]+$"))
                {
                    t = new AstOperand("arg", args.IndexOf(postfix[i]));
                    st.Push(t);
                }
                else if (IsNumber(postfix[i]))
                {
                    t = new AstOperand("imm", Convert.ToDouble(postfix[i]));//Если Элемент число то просто добавляем в стек новый узел без дочерних элементов
                    st.Push(t);
                }
            }
            t = st.Peek();
            st.Pop();
            return (AstOperator)t;//Последний элемент в стеке и будет корнем нашего дерева
        }
    }
}
