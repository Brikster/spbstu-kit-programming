using System;

namespace PolytechHomeworks
{
    public static class NWord
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

            PrintWords(n, m, "");
        }

        private static void PrintWords(int n, int m, string word)
        {
            bool lastIteration = word.Length == n - 1;
            for (int i = 0; i < m; i++)
            {
                string newWord = word + Alpabet[i];
                if (lastIteration) Console.WriteLine(newWord);
                else PrintWords(n, m, newWord);
            }
        }

    }
}