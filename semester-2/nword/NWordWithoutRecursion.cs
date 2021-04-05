using System;

namespace PolytechHomeworks
{
    public static class NWordWithoutRecursion
    {
        private const string Alpabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        
        public static void Main2()
        {
            Console.Write("Введите длину слова: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 1)
            {
                Console.WriteLine("Ожидалось положительное число.");
                return;
            }
            
            Console.Write("Введите количество букв алфавита: ");
            if (!int.TryParse(Console.ReadLine(), out int m) || m > 33 || m < 1)
            {
                Console.WriteLine("Ожидалось число в диапазоне [1; 33].");
                return;
            }

            PrintWords(n, m);
        }

        private static void PrintWords(int n, int m)
        {
            int[] charNums = new int[n];

            do
            {
                string word = "";
                for (int i = 0; i < n; i++)
                {
                    word += Alpabet[charNums[i]];
                }
                
                Console.WriteLine(word);

                charNums[n - 1]++;
                for (int i = n - 1; i > 0; i--)
                {
                    if (charNums[i] == m)
                    {
                        charNums[i] = 0;
                        charNums[i - 1]++;
                    }
                    else break;
                }
            } while (charNums[0] != m);
        }
    }
}