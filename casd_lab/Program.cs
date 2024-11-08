using System;


struct ComplexNumber
{
    public double RealPart; // Вещественная
    public double ImaginaryPart; // Мнимая

    // Методы для работы с комплексными числами
    
    // Создание комплексного числа
    public static ComplexNumber Create(double real, double imaginary)
    {
        return new ComplexNumber { RealPart = real,
            ImaginaryPart = imaginary };
    }
    
    // Сложение
    public static ComplexNumber Add(ref ComplexNumber num1, ComplexNumber num2)
    {
        num1.RealPart += num2.RealPart;
        num1.ImaginaryPart += num2.ImaginaryPart;
        return num1;
    }
    
    // Вычитание
    public static ComplexNumber Subtract(ref ComplexNumber num1, ComplexNumber num2)
    {
        num1.RealPart -= num2.RealPart;
        num1.ImaginaryPart -= num2.ImaginaryPart;
        return num1;
    }
    
    // Умножение
    public static ComplexNumber Multiply(ref ComplexNumber num1, ComplexNumber num2)
    {
        var temp = num1.RealPart;
        num1.RealPart = num1.RealPart * num2.RealPart - num1.ImaginaryPart * num2.ImaginaryPart;
        num1.ImaginaryPart = temp * num2.ImaginaryPart + num1.ImaginaryPart * num2.RealPart;
        return num1;
    }
    
    // Деление
    public static ComplexNumber Divide(ref ComplexNumber num1, ComplexNumber num2)
    {
        var a = num2.RealPart * num2.RealPart + num2.ImaginaryPart * num2.ImaginaryPart;
        var temp = num1.RealPart;
        num1.RealPart = (num1.RealPart * num2.RealPart + num1.ImaginaryPart * num2.ImaginaryPart) / a;
        num1.ImaginaryPart = (num1.ImaginaryPart * num2.RealPart - temp * num2.ImaginaryPart) / a;
        return num1;
    }
    
    // Нахождение модуля
    public static double GetModule(ComplexNumber num)
    {
        return Math.Sqrt(num.RealPart * num.RealPart + num.ImaginaryPart * num.ImaginaryPart);
    }
    
    // Нахождение аргумента
    public static double GetArg(ComplexNumber num)
    {
        return Math.Atan2(num.ImaginaryPart, num.RealPart);
    }
    
    // Возврат мнимой части
    public static double GetImaginaryPart(ComplexNumber num)
    {
        return num.ImaginaryPart;
    }
    
    // Возврат вещественной части
    public static double GetRealPart(ComplexNumber num)
    {
        return num.RealPart;
    }
    
    // Вывод комплексного числа
    public static void Print(ComplexNumber num)
    {
        Console.WriteLine($"({num.RealPart},{num.ImaginaryPart})");
    }
}

class Program
{
    static void Main()
    {
        ComplexNumber num = ComplexNumber.Create(0, 0);
        ComplexNumber num2 = ComplexNumber.Create(0, 0);
        double real;
        double imaginary;
        char choice;
        bool exitFlag;

        do
        {
            Console.WriteLine("------ Задача 2. Комплексные числа -------");
            Console.WriteLine("1. Ввести комплексное число");
            Console.WriteLine("2. Сложить с комплексным числом");
            Console.WriteLine("3. Вычесть из комплексного числа");
            Console.WriteLine("4. Умножить комплексное число");
            Console.WriteLine("5. Разделить комплексное число");
            Console.WriteLine("6. Найти модуль комплексного числа");
            Console.WriteLine("7. Найти аргумент комплексного числа");
            Console.WriteLine("8. Вывести комплексное число");
            Console.WriteLine("Q. Выйти из программы");

            choice = char.ToUpper(Console.ReadKey().KeyChar);
            exitFlag = false;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    Console.Write("Введите вещественную часть: ");
                    real = double.Parse(Console.ReadLine());
                    Console.Write("Введите мнимую часть: ");
                    imaginary = double.Parse(Console.ReadLine());
                    num = ComplexNumber.Create(real, imaginary);
                    break;
                case '2':
                    Console.Write("Введите вещественную часть: ");
                    real = double.Parse(Console.ReadLine());
                    Console.Write("Введите мнимую часть: ");
                    imaginary = double.Parse(Console.ReadLine());
                    ComplexNumber.Print(ComplexNumber.Add(ref num, ComplexNumber.Create(real, imaginary)));
                    break;
                case '3':
                    Console.Write("Введите вещественную часть: ");
                    real = double.Parse(Console.ReadLine());
                    Console.Write("Введите мнимую часть: ");
                    imaginary = double.Parse(Console.ReadLine());
                    ComplexNumber.Print(ComplexNumber.Subtract(ref num, ComplexNumber.Create(real, imaginary)));
                    break;
                case '4':
                    Console.Write("Введите вещественную часть: ");
                    real = double.Parse(Console.ReadLine());
                    Console.Write("Введите мнимую часть: ");
                    imaginary = double.Parse(Console.ReadLine());
                    ComplexNumber.Print(ComplexNumber.Multiply(ref num, ComplexNumber.Create(real, imaginary)));
                    break;
                case '5':
                    Console.Write("Введите вещественную часть: ");
                    real = double.Parse(Console.ReadLine());
                    Console.Write("Введите мнимую часть: ");
                    imaginary = double.Parse(Console.ReadLine());
                    ComplexNumber.Print(ComplexNumber.Divide(ref num, ComplexNumber.Create(real, imaginary)));
                    break;
                case '6':
                    Console.WriteLine(ComplexNumber.GetModule(num));
                    break;
                case '7':
                    Console.WriteLine(ComplexNumber.GetArg(num));
                    break;
                case '8':
                    ComplexNumber.Print(num);
                    break;
                case 'Q':
                    exitFlag = true;
                    break;
                case 'q':
                    exitFlag = true;
                    break;
                default:
                    Console.WriteLine("Неправильный символ");
                    break;
            }
        } while (!exitFlag);
    }
}
