using System;
using System.Linq;

namespace PolytechHomeworks
{
    public static class Kaleidoscope
    {
        private static readonly Random random = new Random();
        
        public static void Main2()
        {
            Console.Write("Введите n: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n > 20 || n < 3)
            {
                Console.WriteLine("Ожидалось целое число от 3 до 20.");
                Console.ReadKey();
                return;
            }

            int[,] square = GenerateSquare(n);

            for (int i = 0; i < n * 2; i++)
            {
                for (int j = 0; j < n * 2; j++)
                {
                    Console.BackgroundColor = (ConsoleColor) square[i, j];
                    Console.Write("  ");
                }
                
                Console.WriteLine();
            }

            Console.ReadKey();
        }
        
        private static int[,] GenerateSquare(int n)
        {
            int[,] square = new int[2 * n, 2 * n];
            int colorsCount = Enum.GetValues(typeof(ConsoleColor)).Length;
            int maxIndex = 2 * n - 1;

            for (int i = 0; i < n; i++)
            for (int j = i; j < n; j++)
            {
                int colorId = random.Next(colorsCount);
                
                SetColor(ref square, i, j, colorId);
                SetColor(ref square, maxIndex - i, j, colorId);
                SetColor(ref square, maxIndex - j, i, colorId);
                SetColor(ref square, maxIndex - i, maxIndex - j, colorId);
            }

            return square;
        }

        private static void SetColor(ref int[,] array, int x, int y, int color)
        {
            array[x, y] = color;
            array[y, x] = color;
        }
    }
}