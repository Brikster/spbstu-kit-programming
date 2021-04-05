using System;
using System.Collections.Generic;

namespace PolytechHomeworks
{
    public static class NMaxes
    {
        public static void Main2()
        {
            Console.Write("Введите количество чисел: ");
            if (!uint.TryParse(Console.ReadLine(), out uint n) || n == 0)
            {
                Console.WriteLine("Ожидалось натуральное число.");
                return;
            }

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите следующее число: ");
                
                int k;
                
                while (!int.TryParse(Console.ReadLine(), out k))
                {
                    Console.WriteLine("Ожидалось целое число.");
                    Console.Write("Введите число: ");
                }

                arr[i] = k;
            }
            
            Console.Write("Введите количество максимумов: ");

            int count;
            while (!int.TryParse(Console.ReadLine(), out count))
            {
                Console.WriteLine("Ожидалось целое число.");
                Console.Write("Введите число: ");
            }
            
            List<int> maxes = FindMaxes(arr, count);
            
            Console.WriteLine(String.Join(" ", maxes));
        }

        private static List<int> FindMaxes(int[] arr, int n)
        {
            List<int> maxes = new List<int>(n);
            
            for (int i = 0; i < n; i++) 
                maxes.Add(Int32.MinValue);

            foreach (int el in arr)
            {
                if (maxes[maxes.Count - 1] < el)
                {
                    maxes.RemoveAt(0);
                    maxes.Add(el);
                    continue;
                }
                
                if (maxes[0] < el)
                {
                    for (int i = 0; i < n; i++)
                    {
                        maxes[i] = maxes[i + 1];
                        
                        if (maxes[i + 1] > el)
                        {
                            maxes[i] = el;
                            break;
                        }
                    }
                }
            }

            return maxes;
        }
    }
}