using System;

namespace PolytechHomeworks
{
    public static class Numerology
    {
        public static void Main2()
        {
            Console.Write("Введите дату в формате DDMMYYYY: ");
            if (!uint.TryParse(Console.ReadLine(), out uint date) || date < 10000000)
            {
                Console.WriteLine("Ожидалось положительное число (запись даты в формате DDMMYYYY)");
                return;
            }

            uint sum = 0;
            do
            {
                do
                {
                    sum += date % 10;
                    date /= 10;
                } while (date != 0);

                date = sum;
                sum = 0;
            } while (date > 10);
            
            Console.WriteLine($"Результат нумерологических преобразований: {date}");
            Console.ReadKey();
        }
    }
}