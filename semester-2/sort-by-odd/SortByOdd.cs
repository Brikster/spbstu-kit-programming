using System;
using System.Collections.Generic;

namespace PolytechHomeworks
{
    public static class SortByOdd
    {
        private static readonly Random random = new Random();
        
        public static void Main2()
        {
            int[] arr = new int[30];

            for (int i = 0; i < 30; i++)
            { 
                arr[i] = random.Next(30);
            }
            
            // Два метода сортировки (через массив и через списки)
            SortArray(ref arr);
            //SortArrayWithList(ref arr);

            Console.WriteLine(String.Join(", ", arr));
            Console.ReadKey();
        }

        // Поочерёдно проходимся по элементам массива.
        // Для каждого нечётного элемента находим
        // ближайший чётный элемент и меняем их местами
        private static void SortArray(ref int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] % 2 == 0) continue;
                
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] % 2 != 0) continue;
                    
                    int temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                    break;
                }
            }
        }
        
        // Создаём пустой список и добавляем элемент
        // в зависимости от чётности в начало или конец.
        private static void SortArrayWithList(ref int[] arr)
        {
            List<int> list = new List<int>();
            foreach (int i in arr)
            {
                if (i % 2 == 0) list.Insert(0, i);
                else list.Add(i);
            }

            arr = list.ToArray();
        }
    }
}