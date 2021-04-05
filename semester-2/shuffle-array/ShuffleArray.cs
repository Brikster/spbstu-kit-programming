using System;

namespace PolytechHomeworks
{
    public static class ShuffleArray
    {
        private static readonly Random random = new Random();
        
        public static void Main2()
        {
            Console.Write("Введите size: ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n) || n < 1)
            {
                Console.WriteLine("Ожидалось натруальное число.");
                Console.Write("Введите size: ");
            }

            int[] arr = new int[n];
            for (int i = 0; i < n; i++) 
                arr[i] = i + 1;
            
            for (int i = n - 1; i >= 1; --i)
            {
                int indexToSwap = random.Next(i + 1);
                int temp = arr[i];
                arr[i] = arr[indexToSwap];
                arr[indexToSwap] = temp;
            }
            
            Console.WriteLine(String.Join(", ", arr));
        }
    }
}