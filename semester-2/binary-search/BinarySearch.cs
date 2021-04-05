using System;

namespace PolytechHomeworks
{
    public static class BinarySearch
    {
        public static void Main2()
        {
            int[,] arr = { { 2, 6, 7, 9, 9, 14}, {18, 20, 26, 26, 29, 40 }, { 44, 47, 50, 51, 55, 62} };
            int num = 12;

            Console.WriteLine(Find(arr, num, out int index1, out int index2)
                ? $"[{index1}, {index2}]"
                : "Элемент не найден.");
        }

        private static bool Find(int[,] arr, int number, out int index1, out int index2)
        {
            int left = 0; int right = arr.Length - 1;
            int length = arr.GetLength(1);

            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (arr[mid / length, mid % length] == number)
                {
                    index1 = mid / length;
                    index2 = mid % length;
                    return true;
                } else if (arr[mid / length, mid % length] > number)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            index1 = -1;
            index2 = -1;
            return false;
        }
    }
}