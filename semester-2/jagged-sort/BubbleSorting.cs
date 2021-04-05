using System;

namespace PolytechHomeworks
{
    public class BubbleSorting
    {
        public static void Main2()
        {
            int[][] arrTaxi = new int[10][];
 
            arrTaxi[0] = new[] { 80 };
            arrTaxi[1] = new[] { 60, 100, 40 };
            arrTaxi[2] = new[] { 70, 120, 290 };
            arrTaxi[3] = new[] { 100, 209, 175, 100 };
            arrTaxi[4] = new[] { 230, 200, 250, 100 };
            arrTaxi[5] = new[] { 90, 80, 105, 140, 120 };
            arrTaxi[6] = new[] { 290, 300, 303, 120, 150 };
            arrTaxi[7] = new[] { 300, 60, 120, 400, 410 };
            arrTaxi[8] = new[] { 100, 289, 200, 101, 90, 230 };
            arrTaxi[9] = new[] { 60, 160, 165, 120, 110, 230, 200, 30 };

            BubbleSort(arrTaxi, out int iterations);
            
            foreach (int[] arr in arrTaxi) 
                Console.WriteLine(String.Join(", ", arr));
            
            Console.WriteLine("Количество итераций: " + iterations);
        }

        private static void BubbleSort(int[][] arr, out int iterations)
        {
            iterations = 0;
            
            for (int i = 0; i < arr.Length; i++)
            for (int j = 0; j < arr.Length - 1; j++)
            {
                int[] firstArr = arr[i]; int[] secondArr = arr[j];

                if (firstArr.Length > secondArr.Length)
                {
                    Swap(ref arr[i], ref arr[j]);
                }
                else if (firstArr.Length == secondArr.Length)
                {
                    if (CountSum(firstArr) > CountSum(secondArr)) Swap(ref arr[i], ref arr[j]);

                    iterations += firstArr.Length + secondArr.Length;
                }

                iterations++;
            }
        }

        private static void Swap(ref int[] x, ref int[] y)
        {
            int[] t = x;
            x = y; y = t;
        }

        private static int CountSum(int[] arr)
        {
            int sum = 0;
            foreach (int t in arr) sum += t;
            return sum;
        }
    }
}