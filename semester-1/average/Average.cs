using System;

namespace PolytechHomeworks
{
    public static class Average
    {
        public static void Main2()
        {
            Console.Write("Введите первое число: ");
            if (!int.TryParse(Console.ReadLine(), out var firstNum))
            {
                Console.WriteLine("Вы ввели не число.");
                return;
            }

            Console.Write("Введите второе число: ");
            if (!int.TryParse(Console.ReadLine(), out var secondNum))
            {
                Console.WriteLine("Вы ввели не число.");
                return;
            }

            var diff = Math.Abs(firstNum - secondNum);

            ushort twoMod = 1;
            ushort step = 2;

            double average = Math.Min(firstNum, secondNum);
            while (diff > 0)
            {
                if (diff - step < 2)
                {
                    step = 2;
                    twoMod = 1;
                }

                diff -= step;
                average += twoMod;

                step += step;
                twoMod += twoMod;
            }

            if (diff < 0)
            {
                average -= (firstNum - secondNum) % 2 == 0 ? 0.5 : 1;
            }

            Console.WriteLine($"Среднее арифметическое: {average}");
        }
    }
}