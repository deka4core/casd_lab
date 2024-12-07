using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ca4
{
    public abstract class Calculator
    {
        public static double SwitchExpression(string sign, double a, double b)
        {
            switch (sign)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "*":
                    return a * b;
                case "/":
                    return a / b;
                case "^":
                    return Math.Pow(a, b);
                case "√":
                    return Math.Sqrt(a);
                case "sin":
                    return Math.Sin(a);
                case "cos":
                    return Math.Cos(a);
                case "tan":
                    return Math.Tan(a);
                case "ln" when a <= 0:
                    throw new ArgumentException();
                case "ln":
                    return Math.Log(a);
                case "log" when a <= 0:
                    throw new ArgumentException();
                case "log":
                    return Math.Log10(a);
                case "min":
                    return Math.Min(a, b);
                case "max":
                    return Math.Max(a, b);
                case "%":
                    return a % b;
                case "//":
                    return (int)(a / b);
                case "exp":
                    return Math.Exp(1);
                default:
                    return 0;
            }
        }

        private static readonly string[] Operators = { "+", "-", "*", "/", "^", "√", "sin", "cos", "tan", "ln", "log", "min", "max", "%", "//", "(", ")" };
        private const string NumberPattern = @"^-?\d+(\.\d+)?$";

        private static void Parse(string expression, out MyStack<double> numbers, out MyStack<string> signs)
        {
            numbers = new MyStack<double>();
            signs = new MyStack<string>();

            var tokens = expression.Split(' ');
            foreach (var token in tokens)
            {
                try
                {
                    if (Array.Find(Operators, op => op.Equals(token)) != null)
                    {
                        signs.Push(token);
                    }
                    else if (token == "exp")
                    {
                        numbers.Push(Math.Exp(1));
                    }
                    else if (Regex.Matches(token, NumberPattern).Count > 0)
                    {
                        double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out var a);
                        numbers.Push(a);
                    }
                    else
                    {
                        var flag = token.All(char.IsLetter);
                        if (!flag) continue;
                        Console.WriteLine("Введите " + token + ": ");
                        try
                        {
                            var n = Console.ReadLine();
                            numbers.Push(Convert.ToDouble(n));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(token + " не число.");
                        }
                    }
                }
                catch 
                {
                    Console.WriteLine("Ввели некорректное выражение. Разделите все операции, числа и скобки пробелами. Вместо , в дробном числе введите .");
                }

            }
        }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            while (true)
            {
                {
                    Console.WriteLine("Введите математическое выражение, разделяя все части выражения пробелом:");
                    var expression = Console.ReadLine();
                    try
                    {
                        Parse(expression, out var numbers, out var signs);
                        var result = Calculate(numbers, signs);
                        Console.WriteLine(result);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
            }
        }
        
        private static double Calculate(MyStack<double> numbers, MyStack<string> signs)
        {
            while (!signs.Empty() && !numbers.Empty())
            {
                try
                {
                    var sign = signs.Pop();
                    if (sign == ")")
                    {
                        numbers.Push(Calculate(numbers, signs));
                    }
                    else if (sign == "(")
                        break;
                    else
                        numbers.Push(Switch(sign, numbers));
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            return numbers.Pop();
        }

        private static double Switch(string sign, MyStack<double> numbers)
        {
            switch (sign)
            {
                case "+":
                    return numbers.Pop() + numbers.Pop();
                case "-":
                    return -1 * (numbers.Pop() - numbers.Pop());
                case "*":
                    return numbers.Pop() * numbers.Pop();
                case "/":
                {
                    var a = numbers.Pop();
                    var b = numbers.Pop();
                    return b / a;
                }
                case "^":
                {
                    var a = numbers.Pop();
                    var b = numbers.Pop();
                    return Math.Pow(b, a);
                }
                case "√":
                    return Math.Sqrt(numbers.Pop());
                case "sin":
                    return Math.Sin(numbers.Pop());
                case "cos":
                    return Math.Cos(numbers.Pop());
                case "tan":
                    return Math.Tan(numbers.Pop());
                case "ln":
                {
                    var a = numbers.Pop();
                    if (a <= 0) throw new ArgumentException();
                    return Math.Log(a);
                }
                case "log":
                {
                    var a = numbers.Pop();
                    if (a <= 0) throw new ArgumentException();
                    return Math.Log10(a);
                }
                case "min":
                    return Math.Min(numbers.Pop(), numbers.Pop());
                case "max":
                    return Math.Max(numbers.Pop(), numbers.Pop());
                case "%":
                {
                    var a = numbers.Pop();
                    var b = numbers.Pop();
                    return b % a;
                }
                case "//":
                {
                    var a = numbers.Pop();
                    var b = numbers.Pop();
                    return (int)b / a;
                }
                default:
                    return 0;
            }
        }
    }
}
