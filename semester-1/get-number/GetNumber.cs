using System;
using System.Collections.Generic;
using System.Linq;

namespace PolytechHomeworks
{
    public static class GetNumber
    {
        private static uint _a;
        private static uint _b;
        private static uint _target;
        
        public static void Main2()
        {
            if (!ReadNumber(out _a, "Введите A: ")) return;
            if (!ReadNumber(out _b, "Введите B: ")) return;
            if (!ReadNumber(out _target, "Введите целевое число: ")) return;
            
            if (_target <= 1)
            {
                Console.WriteLine("Ожидалось число, большее единицы.");
                return;
            }
            
            List<String> combinations = new List<string>();
            
            // Передаём список в функцию рекурсивного поиска комбинаций
            // Можно было бы использовать ref, но в нашем случае это не имеет смысла
            FindCombinations(combinations);
            
            // Проверяем список на наличие элементов
            if (combinations.Any())
            {
                // Сортируем список по длине строки для красоты
                combinations.Sort((x, y) => x.Length - y.Length);
                combinations.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("Не найдено ни одной комбинации.");
            }
            
            Console.ReadKey();
        }

        private static bool ReadNumber(out uint number, string text)
        {
            Console.Write(text);
            if (uint.TryParse(Console.ReadLine(), out number) && number > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Ожидалось целое положительное число.");
                return false;
            }
        }

        private static void FindCombinations(List<string> list, uint number = 1, string pattern = "1")
        {
            if (number < _target)
            {
                // Объявляем шаблоны, используемые для построения выражения
                string sumPattern = $"{pattern} + {_a}";
                // Для числа "1" используем другой шаблон, чтобы избежать лишних скобок в записи
                string multiplyPattern = number == 1 
                    ? $"{pattern} * {_b}" 
                    : $"({pattern}) * {_b}";
                
                // Рекурсивно вызываем функцию поиска комбинаций, пока не дойдём до нужного числа
                if (number + _a == _target)
                {
                    list.Add(sumPattern);
                }
                else
                {
                    FindCombinations(list, number + _a, sumPattern);
                }
                
                if (number * _b == _target)
                {
                    list.Add(multiplyPattern);
                }
                else
                {
                    FindCombinations(list, number * _b, multiplyPattern);
                }
            }
        }
    }
}