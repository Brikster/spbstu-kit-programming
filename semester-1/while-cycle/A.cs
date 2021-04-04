using System;

namespace PolytechHomeworks
{
    public static class A
    {
        private static void Main2()
        {
            Console.Write("Введите число средств, вложенных Сидоровым (N): ");
            uint n = Convert.ToUInt32(Console.ReadLine());
            
            Console.Write("Введите процент по вкладу (M): ");
            double m = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Введите необходимую сумму средств (R): ");
            uint r = Convert.ToUInt32(Console.ReadLine());

            int years = 0;

            while (n < r)
            {
                n = (uint) (n * (100 + m) / 100);
                years++;
            }
            
            Console.WriteLine($"Необходимое кол-во лет для достижения R: {years}.");
            Console.ReadKey();
        }
    }
}