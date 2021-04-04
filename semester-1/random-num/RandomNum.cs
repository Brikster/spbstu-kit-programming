using System;

namespace PolytechHomeworks
{
    public static class RandomNum
    {
        
        private static readonly Random Random = new Random();
        
        public static void Main2()
        {
            if (!ReadValue(out int delimeter, "Введите делитель: ")) return;

            int number;
            bool success;
            
            Console.WriteLine("Использовать значения min и max по-умолчанию? (Y)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                success = RandomNumber(out number, delimeter);
            }
            else
            {
                if (!ReadValue(out int min, "Введите min: ")) return; 
                if (!ReadValue(out int max, "Введите max: ")) return; 
                success = RandomNumber(out number, delimeter, min, max);
            }
            
            Console.WriteLine(success
                    ? $"Случайное число: {number}" 
                    : "Некорректные аргументы.");
        }

        /*
         * Генерирует случайное число в диапазоне [min; max], делящееся на delimeter
         * Возвращает true в случае успешной генерации, false - при ошибке
         */
        private static bool RandomNumber(out int number, int delimeter, int min = 1, int max = 100)
        {
            number = 0;
            
            // Проверям корректность аргументов
            if (min >= max || min <= 0 || delimeter <= 0)
            {
                return false;
            }
            
            int minDelimeterDiv = min / delimeter;
            int maxDelimeterDiv = max / delimeter;

            // Находим минимальное случайное число из диапазона
            int minRandomizedNumber = minDelimeterDiv * delimeter;
            if (min % delimeter != 0)
            {
                minRandomizedNumber += delimeter;
            }

            // Находим максимальное случайное число из диапазона
            int maxRandomizedNumber = maxDelimeterDiv * delimeter;

            // Проверяем корректность найденных чисел
            if (minRandomizedNumber > maxRandomizedNumber)
            {
                return false;
            }

            // Находим число из диапазона, удовлетворяющее условиям, с помощью равновероятной функции рандомизации
            int randomIndex = Random.Next((maxRandomizedNumber - minRandomizedNumber) / delimeter + 1);
            number = minRandomizedNumber + randomIndex * delimeter;

            return true;
        }

        private static bool ReadValue(out int var, string text)
        {
            Console.Write(text);
            
            if (!int.TryParse(Console.ReadLine(), out var))
            {
                Console.WriteLine("Ожидалось целое число.");
                return false;
            }
            
            return true;
        }
    }
}