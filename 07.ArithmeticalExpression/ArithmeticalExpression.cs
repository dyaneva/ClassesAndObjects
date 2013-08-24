// Write a program that calculates the value of given arithmetical expression. The expression can contain the 
// following elements only:
// 1.Real numbers, e.g. 5, 18.33, 3.14159, 12.6 
// 2.Arithmetic operators: +, -, *, / (standard priorities)
// 3.Mathematical functions: ln(x), sqrt(x), pow(x,y)
// 4.Brackets (for changing the default priorities)
// Examples: (3+5.3) * 2.7 - ln(22) / pow(2.2, -1.7)  ~ 10.6
//           pow(2, 3.14) * (3 - (3 * sqrt(2) - 3.2) + 1.5*0.3)  ~ 21.22
// Hint: Use the classical "shunting yard" algorithm and "reverse Polish notation".

using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;

class ArithmeticalExpression
{
    public static List<char> arithmeticOperations = new List<char>() { '+', '-', '*', '/' };
    public static List<char> brackets = new List<char>() { '(', ')' };
    public static List<string> functions = new List<string>() { "pow", "sqrt", "ln" };
    
    public static string TrimInput(string input)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != ' ')
            {
                result.Append(input[i]);
            }
        }
        return result.ToString();
    }
    // method for separating up numbers, arithmetic operators, functions, other tokens
    public static List<string> SeparateTokens(string input)
    {
        List<string> result = new List<string>();
        StringBuilder number = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '-' && (i == 0 || input[i - 1] == ',' || input[i - 1] == '('))
            {
                number.Append('-');
            }
            else if (char.IsDigit(input[i]) || input[i] == '.')
            {
                number.Append(input[i]);
            }
            else if (!char.IsDigit(input[i]) && input[i] != '.' && number.Length != 0)
            {
                result.Add(number.ToString());
                number.Clear();
                i--;
            }
            else if (brackets.Contains(input[i]))
            {
                result.Add(input[i].ToString());
            }
            else if (arithmeticOperations.Contains(input[i]))
            {
                result.Add(input[i].ToString());
            }
            else if (input[i] == ',')
            {
                result.Add(",");
            }
            else if (i + 1 < input.Length && input.Substring(i, 2).ToLower() == "ln")
            {
                result.Add("ln");
                i++;
            }
            else if (i + 2 < input.Length && input.Substring(i, 3).ToLower() == "pow")
            {
                result.Add("pow");
                i += 2;
            }
            else if (i + 3 < input.Length && input.Substring(i, 4).ToLower() == "sqrt")
            {
                result.Add("sqrt");
                i += 3;
            }
            else
            {
                throw new ArgumentException("Invalid expression");
            }
        }
        if (number.Length != 0)
        {
            result.Add(number.ToString());
        }
        return result;
    }
    // method for determination standard priorities
    public static int Precedence(string operators)
    {
        if (operators == "+" || operators == "-")
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
    // method using Shunting-yard algorithm 
    public static Queue<string> ConvertToReversePolishNotation(List<string> tokens)
    {
        Stack<string> stack = new Stack<string>();
        Queue<string> queue = new Queue<string>();
        for (int i = 0; i < tokens.Count; i++)
        {
            var currentToken = tokens[i];
            double number;
            if (double.TryParse(currentToken, out number))
            {
                queue.Enqueue(currentToken);
            }
            else if (functions.Contains(currentToken))
            {
                stack.Push(currentToken);
            }
            else if (currentToken == ",")
            {
                if (!stack.Contains("(") || stack.Count == 0)
                {
                    throw new ArgumentException("Invalid brackets or function separator");
                }
                while (stack.Peek() != "(")
                {
                    queue.Enqueue(stack.Pop());                    
                }
            }
            else if (arithmeticOperations.Contains(currentToken[0]))
            {                
                while (stack.Count != 0 && arithmeticOperations.Contains(stack.Peek()[0]) && Precedence(currentToken) <= Precedence(stack.Peek()))
                {
                    queue.Enqueue(stack.Pop());
                }
                stack.Push(currentToken);
            }
            else if (currentToken == "(")
            {
                stack.Push("(");
            }
            else if (currentToken == ")")
            {
                if (!stack.Contains("(") || stack.Count == 0)
                {
                    throw new ArgumentException("Invalid brackets position");
                }
                while (stack.Peek() != "(")
                {
                    queue.Enqueue(stack.Pop());
                }
                stack.Pop();
                if (stack.Count != 0 && functions.Contains(stack.Peek()))
                {
                    queue.Enqueue(stack.Pop());
                }
            }
        }
        while (stack.Count != 0)
        {
            if (brackets.Contains(stack.Peek()[0]))
            {
                throw new ArgumentException("Invalid brackets position");
            }
            queue.Enqueue(stack.Pop());
        }
        return queue;
    }
    // method for converting into real number using Reverse Polish notation
    public static double GetResultFromRPN(Queue<string> queue)
    {
        Stack<double> stack = new Stack<double>();
        while (queue.Count != 0)
        {
            string currentToken = queue.Dequeue();
            double number;
            if (double.TryParse(currentToken, out number))
            {
                stack.Push(number);
            }
            else if (arithmeticOperations.Contains(currentToken[0]) || functions.Contains(currentToken))
            {
                if (currentToken == "+")
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("No sufficient values!");
                    }
                    double firstValue = stack.Pop();
                    double secondValue = stack.Pop();
                    stack.Push(firstValue + secondValue);
                }
                else if (currentToken == "-")
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("No sufficient values!");
                    }
                    double firstValue = stack.Pop();
                    double secondValue = stack.Pop();
                    stack.Push(secondValue - firstValue);
                }
                else if (currentToken == "*")
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("No sufficient values!");
                    }
                    double firstValue = stack.Pop();
                    double secondValue = stack.Pop();
                    stack.Push(secondValue * firstValue);
                }
                else if (currentToken == "/")
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("No sufficient values!");
                    }
                    double firstValue = stack.Pop();
                    double secondValue = stack.Pop();
                    stack.Push(secondValue / firstValue);
                }
                else if (currentToken == "pow")
                {
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException("No sufficient values!");
                    }
                    double firstValue = stack.Pop();
                    double secondValue = stack.Pop();
                    stack.Push(Math.Pow(secondValue, firstValue));
                }
                else if (currentToken == "sqrt")
                {
                    if (stack.Count < 1)
                    {
                        throw new ArgumentException("No sufficient values!");
                    }
                    double value = stack.Pop();
                    stack.Push(Math.Sqrt(value));
                }
                else if (currentToken == "ln")
                {
                    if (stack.Count < 1)
                    {
                        throw new ArgumentException("No sufficient values!");
                    }
                    double value = stack.Pop();
                    stack.Push(Math.Log(value));
                }
            }
        }
        if (stack.Count == 1)
        {
            return stack.Pop();
        }
        else
        {
            throw new ArgumentException("Error - too many values!");
        }
    }    
    static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Console.WriteLine("Enter math expression:");        
        try
        {
            string expression = Console.ReadLine();
            string trimExpression = expression.Replace(" ", string.Empty);
            var separatedTokens = SeparateTokens(trimExpression);
            var reversePolishNotation = ConvertToReversePolishNotation(separatedTokens);
            var result = GetResultFromRPN(reversePolishNotation);
            Console.WriteLine(result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error!\n{0}: {1}", e.GetType().Name, e.Message);
        }      
    }
}
