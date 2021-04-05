using System;

namespace PolytechHomeworks
{
    public static class Diagonals
    {
        public static void Main2()
        {
            // int[, ,] arr = {{ { 23, 30, 2 }, { 4, 8, 12 }, { 9, 11, 16 } }, 
            //     { { 3, 21, 5 }, { 14, 18, 9 }, { 49, 33, 2 } }, 
            //     { { 32, 32, 55 }, { 71, 86, 74 }, { 7, 1, 8 } }};
            
            int[, ,] arr = {
                { // 86
                    { 23, 30, 2, 11}, 
                    { 4, 8, 12, 5 }, 
                    { 9, 11, 16, 7 }, 
                    {44, 46, 2, 8}
                },
                { // 62
                    { 3, 21, 5, 14 }, 
                    { 14, 18, 9, 78 }, 
                    { 49, 33, 2, 2 }, 
                    {11, 61, 71, 42}
                },
                { // 144
                    { 74, 24, 15, 64 }, 
                    { 77, 20, 59, 13 }, 
                    { 13, 3, 62, 78 }, 
                    {90, 4, 28, 62}
                },
                { // 45
                    { 32, 32, 55, 5 }, 
                    { 71, 86, 74, 71 }, 
                    { 7, 1, 8, 99 }, 
                    {3, 1, 8, 5}
                }};

            Console.WriteLine(CountSum(arr, out int sum)
                ? $"Сумма элементов диагоналей = {sum}."
                : "Массив не является кубом.");

            Console.ReadKey();
        }

        private static bool CountSum(int[,,] arr, out int sum)
        {
            sum = 0;
            
            int length;
            if (arr.Rank != 3 
                || arr.GetLength(0) != (length = arr.GetLength(1)) 
                || length != arr.GetLength(2))
                return false;
            
            for (int i = 0; i < length; i++)
            for (int j = 0; j < length; j++)
            for (int k = 0; k < length; k++)
            {
                int jReversed = length - j - 1;
                int kReversed = length - k - 1;
                    
                bool diagonal = false;
                diagonal |= CheckEquals(i, j, k);
                diagonal |= CheckEquals(i, j, kReversed);
                diagonal |= CheckEquals(i, jReversed, k);
                diagonal |= CheckEquals(i, jReversed, kReversed);

                if (diagonal)
                {
                    sum += arr[i, j, k];
                }
            }

            return true;

        }

        private static bool CheckEquals(int i, int j, int k)
        {
            return i == j && k == j;
        }
    }

}