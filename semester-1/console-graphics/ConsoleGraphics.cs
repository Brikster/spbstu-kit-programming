using System;

namespace PolytechHomeworks
{
    public static class ConsoleGraphics
    {
        // Ширина ели
        private static uint _treeWide;

        public static void Main2()
        {
            Console.Write("Введите количество рядов (M): ");
            uint m = Convert.ToUInt32(Console.ReadLine());

            Console.Write("Введите количество колонок (N): ");
            uint n = Convert.ToUInt32(Console.ReadLine());

            Console.Write("Введите высоту ёлки (K): ");
            uint k = Convert.ToUInt32(Console.ReadLine());

            Console.WriteLine();

            _treeWide = k < 3 ? 1 : (k - 2) * 2 + 1;

            for (int i = 0; i < m; i++)
            {
                PrintTreeRow(k, n);

                if (i != m - 1)
                {
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
        }

        /**
         * "Отрисовываем" строки с разными частями нескольких деревьев
         */
        private static void PrintTreeRow(uint height, uint columns)
        {
            ushort currentWide = 1;
            for (ushort i = 0; i < height; i++)
            {
                if (i == height - 1)
                {
                    currentWide = 1;
                }

                PrintRowLine(currentWide, columns);

                currentWide += 2;
            }
        }

        /**
         * "Отрисовываем" части нескольких деревьев на определённой строке
         */
        private static void PrintRowLine(uint currentWide, uint columns)
        {
            string line = "";
            uint spaces = (_treeWide - currentWide) / 2;

            for (ushort j = 0; j < columns; j++)
            {
                line += GetInlineTreePart(spaces);

                if (j != (columns - 1))
                {
                    line += " ";
                }
            }

            Console.WriteLine(line);
        }

        /**
         * "Отрисовываем" часть одного дерева на определённой строке
         */
        private static string GetInlineTreePart(uint spaces)
        {
            var line = "";
            
            for (uint k = 0; k < _treeWide; k++)
            {
                if (k < spaces || (_treeWide - k) <= spaces)
                {
                    line += " ";
                }
                else
                {
                    line += "#";
                }
            }

            return line;
        }
    }
}