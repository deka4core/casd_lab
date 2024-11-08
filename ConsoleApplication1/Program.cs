using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    internal abstract class IpAddressExtractor
    {
        private static void Main()
        {
            const string inputFilePath = "input.txt";
            const string outputFilePath = "output.txt";
            List<string> lines = ReadFromFile(inputFilePath);
            List<string> validIPs = ExtractValidIpAddresses(lines);
            WriteLinesToFile(outputFilePath, validIPs);
        }

        private static List<string> ReadFromFile(string filePath)
        {
            try
            {
                return new List<string>(File.ReadAllLines(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return new List<string>();
            }
        }

        private static List<string> ExtractValidIpAddresses(IEnumerable<string> lines)
        {
            return (from line in lines select line.Split(new char[] { ' ' }, 
                StringSplitOptions.RemoveEmptyEntries) into parts from part in parts 
                where IsValidIpAddress(part) select part).ToList();
            /* LINQ return (from line in lines from part in line.Split(new char[] { ' ' },
             StringSplitOptions.RemoveEmptyEntries) where IsValidIPAddress(part) select part).ToList(); */
        }

        private static bool IsValidIpAddress(string ip)
        {
            var parts = ip.Split('.');
            return parts.Length == 4 && parts.All(IsValidPart);
        }

        private static bool IsValidPart(string part)
        {
            if (string.IsNullOrEmpty(part) || part.Length > 3)
                return false;
            if (!int.TryParse(part, out int num) || num < 0 || num > 255)
                return false;
            return part.Length <= 1 || part[0] != '0';
        }

        private static void WriteLinesToFile(string filePath, IEnumerable<string> lines)
        {
            try
            {
                File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
            }
        }
    }
}
