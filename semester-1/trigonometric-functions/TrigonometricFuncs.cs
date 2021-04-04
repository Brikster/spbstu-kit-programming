using System;

namespace PolytechHomeworks
{

    public static class TrigonometricFuncs
    {
        // Константа для указания точности расчётов
        // (минимального предела для вычисляемого члена ряда Маклорена)
        // Выведена, чтобы было удобно изменять
        private const double AccuracyValue = 0.001;
        
        public static void Main2()
        {
            while (true)
            {
                Console.WriteLine("Для выхода из программы введите произвольную строку.");
                Console.Write("Введите угол в градусах: ");

                if (!double.TryParse(Console.ReadLine(), out var angle))
                {
                    Console.WriteLine("Введено не число, выхожу из программы.");
                    return;
                }

                angle = (angle % 360) / 180 * Math.PI;

                double sin = 0;

                ushort n = 0; // Счётчик для номера члена ряда Маклорена
                double member = angle; // Значение члена ряда

                while (Math.Abs(member = GetMemberOfMaclaurinSeries(member, angle, n)) >= AccuracyValue || n < 3)
                {
                    // Определяем чередующийся знак перед n-ым членом ряда
                    // и увеличиваем счётчик на 1
                    if (n++ % 2 == 0)
                    {
                        sin += member;
                    }
                    else
                    {
                        sin -= member;
                    }
                }

                Console.WriteLine($"Значение функции sin: {sin} (вычислено за {n} итер.)");
                Console.WriteLine();
            }
        }

        /*
         * Функция для вычисления n-ого члена ряда Маклорена (0, 1, 2...)
         * 
         *     x ^ (2n + 1)
         *     ------------
         *      (2n + 1)!
         * 
         */
        private static double GetMemberOfMaclaurinSeries(double numerator, double angle, ushort n)
        {
            numerator /= 2 * n + 1;

            if (n == 0) return numerator;
            
            numerator *= Math.Pow(angle, 2);
            numerator /= 2 * n;

            return numerator;
        }
    }
}