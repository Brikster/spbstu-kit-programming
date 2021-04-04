using System;

namespace PolytechHomeworks
{
    public static class ForCycle
    {
        public static void Main2()
        {
            Console.Write("Введите N: ");
            if (!uint.TryParse(Console.ReadLine(), out uint n) || n < 1)
            {
                Console.WriteLine("На входе ожидалось целое положительное число.");
                return;
            }

            uint counter = 0;
            for (uint i = 1; i <= n; i++)
            {
                if (++counter == 10)
                {
                    counter = 0;
                    Console.WriteLine(i);
                }
                else
                {
                    Console.Write($"{i} ");
                }
            }

            Console.ReadKey();
        }
    }
}