using System;
using System.Collections.Generic;

namespace PolytechHomeworks
{
    public static class Pancakes
    {
        public static void Main2()
        {
            Console.Write("Введите количество блинов: ");
            if (!uint.TryParse(Console.ReadLine(), out uint n) || n == 0)
            {
                Console.WriteLine("Ожидалось натуральное число.");
                return;
            }

            List<int> pancakes = new List<int>();
            // new List<int>() { 4, 1, 7, 3, 2, 4, 8, 5, 6 };
            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите диаметр следующего блина: ");
                
                int k;
                
                while (!int.TryParse(Console.ReadLine(), out k))
                {
                    Console.WriteLine("Ожидалось целое число.");
                    Console.Write("Введите диаметр блина: ");
                }

                pancakes.Add(k);
            }
            
            SortPancakes(pancakes);
            
            Console.WriteLine(String.Join(" ", pancakes));
            Console.ReadKey();
        }

        private static void SortPancakes(List<int> list)
        {
            // Проходимся по стопке блинов, начиная с низу
            for (int i = list.Count - 1; i >= 0; i--)
            {
                int max = i;
                // Ищем наибольший блин над отсортированной частью стопки
                for (int j = 0; j < i; j++)
                {
                    if (list[j] > list[max]) max = j;
                }

                // Если наибольший блин внизу, значит, он уже на нужном месте
                if (max == i) continue;
                
                // Если самый большой блин не сверху,
                // то делаем переворот, чтобы он оказался сверху
                if (max != 0) Flip(list, max + 1);

                // Переворачиваем стопку так, чтобы наибольший блин
                // занял необходимое место в стопке снизу
                Flip(list, i + 1);
            }
        }

        private static void Flip(List<int> list, int index)
        {
            for (int i = 0; i < index; i++)
            {
                int element = list[i];
                list[i] = list[--index];
                list[index] = element;
            }
        }
    }
}