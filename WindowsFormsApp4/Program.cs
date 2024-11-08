using System;

namespace ConsoleApp1
{
    internal abstract class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var menu = new Menu();
            menu.ShowDialog();
            Console.ReadLine();
        }
    }
}
