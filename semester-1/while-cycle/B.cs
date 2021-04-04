using System;

namespace PolytechHomeworks
{
    public static class B
    {
        public static void Main2()
        {
            long sum = 0;
            ushort count = 0;

            long input;
            while ((input = ReadNumber()) != 0)
            {
                sum += input;
                count++;
            }

            Console.WriteLine(count == 0
                ? "Не было введено ни одного числа."
                : $"Среднее арифметическое: {(double) sum / count}.");
            Console.ReadKey();
        }

        private static long ReadNumber()
        {
            Console.Write("Введите число (или 0 для выхода из цикла): ");
            return Convert.ToInt64(Console.ReadLine());
        }
    }
}