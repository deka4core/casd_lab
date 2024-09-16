/* Задача 1. Описать метод, находящий длину 𝑥 вектора в 𝑁-мерном пространстве. Пространство 
задаётся матрицей метрического тензора 𝐺 (если интересно, то можно почитать, что это, 
если нет, то нет). Матрица 𝐺 должна быть симметричной, её размерность совпадает с 
размерностью пространства. Нахождение длины: √𝑥 × 𝐺 × 𝑥
𝑇. Размерность пространства, 
матрица тензора и вектор вводятся из файла. Память выделяется динамически; проверка 
на то, что матрица симметрична – необходима. Результат выводится на экран.
*/
using System;
using System.IO;

namespace casd_lab
{
    internal class Program
    {
        public static void Main(string[] args) {
            const string inputFilePath = "input.txt"; // Входной файл

            ReadInput(inputFilePath, out var dim, out var matrixG, out var x);

            if (!IsSymmetric(matrixG)) {
                Console.WriteLine("Матрица тензора G не является симметричной.");
                return;
            }

            double length = CalculatorLength(matrixG, x);
            Console.WriteLine($"Длина вектора: {length}");
        }

        private static void ReadInput(string filePath, out int dim, out double[,] matrixG, out double[] x) {
            var reader = new StreamReader(filePath);
            dim = Convert.ToInt32(reader.ReadLine()); // размерность матрицы
            matrixG = new double[dim, dim]; // Матрица тензора G
            
            for (var i = 0; i < dim; i++){ // Ввод матрицы тензора G
                var row = reader.ReadLine()?.Split();
                for (var j = 0; j < dim; j++)
                {
                    if (row != null) matrixG[i, j] = Convert.ToDouble(row[j]);
                }
            }
            
            x = new double[dim];
            var vecRow = reader.ReadLine()?.Split();
            for (var i = 0; i < dim; i++) // Ввод вектора x
            {
                if (vecRow != null) x[i] = Convert.ToDouble(vecRow[i]);
            }
        }

        private static bool IsSymmetric(double[,] matrix)
        { // Метод проверки матрицы на симметричность
            int n = matrix.GetLength(0); // длина строки
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Math.Abs(matrix[i, j] - matrix[j, i]) > 0) // иначе числа различны
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static double CalculatorLength(double[,] matrixG, double[] x)
        {
            int dim = x.Length;
            double result = 0;
            double[] temp = new double[dim];

            // G * x
            for (int i = 0; i < dim; i++)
            {
                temp[i] = 0;
                for (int j = 0; j < dim; j++)
                {
                    temp[i] += matrixG[i, j] * x[j];
                }
            }

            // (G * x) * x^T
            for (int i = 0; i < dim; i++)
            {
                result += x[i] * temp[i];
            }

            return Math.Sqrt(result);
        }
    }
}