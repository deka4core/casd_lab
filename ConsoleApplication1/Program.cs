using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    internal abstract class Program
    {
        private static void Main(string[] args)
        {
            const string filePath = @"input.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }
            // Список для хранения тегов
            var tags = new HashSet<string>();
            // Регулярное выражение для поиска тегов
            const string pattern = @"<\s*\/?\s*([a-zA-Z][a-zA-Z0-9]*)\s*>";
            foreach (var line in File.ReadLines(filePath))
            {
                var matches = Regex.Matches(line, pattern);
                foreach (Match match in matches)
                {
                    var tag = match.Value.Trim('<', '>', ' '); // вырезаем пробелы
                    tag = match.Groups[1].Value.ToLower();
                    tags.Add(tag);
                }
            }
            // Преобразуем HashSet в список и выводим результаты
            List<string> uniqueTags = tags.ToList();
            Console.WriteLine("Уникальные теги:");
            foreach (var tag in uniqueTags)
            {
                Console.WriteLine(tag);
            }
        }
    }
}