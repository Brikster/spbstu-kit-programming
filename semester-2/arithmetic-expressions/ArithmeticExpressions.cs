using System;
using System.Collections;

namespace PolytechHomeworks
{
    // Илья Андреев, 3530903/00001
    public static class ArithmeticExpressions
    {
        private struct Lexeme
        {
            public bool IsNumber;
            public double Value;
            public int Level;
        }

        private enum Operator
        {
            OpenBracket, CloseBracket,
            Multiply, Divide,
            Plus, Minus,
            
            None
        }

        public static void Main2()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Предупреждение: выражения необходимо вводить со всеми скобами,");
            Console.Write("например: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(2,8 + (4 * ((3,5 * 7) / 3,1)))");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Введите выражение для вычисления:");
            Console.ForegroundColor = ConsoleColor.White;

            string expression = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            
            if (!ParseLexemes(expression, out LinkedList<Lexeme> list) || !CalculateExpression(list, out double result))
            {
                Console.WriteLine("Введено некорректное выражение.");
                Console.ReadKey();
                return;
            }
            
            Console.SetCursorPosition(expression.Length, 3);
            Console.WriteLine($" = {result}");
            Console.ReadKey();
        }

        // Преобразовываем лексемы из строки и собираем в список
        private static bool ParseLexemes(string expression, out LinkedList<Lexeme> list)
        {
            list = new LinkedList<Lexeme>(128);
            
            int level = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (c == ' ') continue;
                
                Lexeme lexeme;

                // Проверяем, является ли лексема оператором
                Operator op = getOperator(c);
                if (op == Operator.None)
                {
                    // Если лексема - число, обрабатываем последующие символы для преобразования в double
                    lexeme.IsNumber = true;
                    string number = c.ToString();
                    while (++i < expression.Length && getOperator(expression[i]) == Operator.None)
                        number += expression[i];
                    i--;
                    
                    lexeme.Level = ++level;

                    if (!double.TryParse(number, out lexeme.Value))
                        return false;
                }
                else
                {
                    // Если лексема - оператор, то сразу заполняем структуру
                    switch (op)
                    {
                        case Operator.OpenBracket:
                            lexeme.Level = ++level;
                            break;
                        default:
                            lexeme.Level = --level;
                            break;
                    }

                    lexeme.Value = (int) op;
                    lexeme.IsNumber = false;
                }

                list.Add(lexeme);
            }

            return level == 1;
        }
        
        // Вычисляем выражение, имея список лексем
        private static bool CalculateExpression(LinkedList<Lexeme> list, out double expressionResult)
        {
            while (list.Count != 1)
            {
                // Ищем элемент с максимальным уровнем
                int index = 0;
                for (int i = 1; i < list.Count; i++)
                {
                    if (list[i].Level > list[index].Level) 
                        index = i;
                }
                
                // Проверяем выражение на валидность: можно ли собрать "тройку" со скобочками 
                if (!(index - 1 >= 0 && index + 3 < list.Count))
                {
                    expressionResult = -1;
                    return false;
                }
                
                // Проверям выражение на валидность: на всех местах "тройки" должны стоять определённые элементы
                bool valid;
                valid  = !list[index - 1].IsNumber && ((Operator) list[index - 1].Value) == Operator.OpenBracket;
                valid &= list[index].IsNumber;
                valid &= !list[index + 1].IsNumber;
                valid &= list[index + 2].IsNumber;
                valid &= !list[index + 3].IsNumber && ((Operator) list[index + 3].Value) == Operator.CloseBracket;
                
                if (!valid) {
                    expressionResult = -1;
                    return false;
                }

                int maxLevel = list[index].Level;
                
                double firstNumber = list[index].Value;
                double secondNumber = list[index + 2].Value;

                // Вычисляем выражение
                double result = 0;
                switch ((Operator) list[index + 1].Value)
                {
                    case Operator.Plus:
                        result = firstNumber + secondNumber;
                        break;
                    case Operator.Minus:
                        result = firstNumber - secondNumber;
                        break;
                    case Operator.Multiply:
                        result = firstNumber * secondNumber;
                        break;
                    case Operator.Divide:
                        result = firstNumber / secondNumber;
                        break;
                }

                // Убираем все элементы тройки из списка
                for (int i = 0; i < 5; i++) 
                    list.RemoveAt(index - 1);

                // Добавляем вычисленный результат в список
                list.AddAt(index - 1, new Lexeme()
                {
                    Level = maxLevel - 1,
                    IsNumber = true,
                    Value = result
                });
            }

            expressionResult = list[0].Value;
            return true;
        }

        // Преобразование символа оператора в enum
        private static Operator getOperator(char c)
        {
            switch (c)
            {
                case '(':
                    return Operator.OpenBracket;
                case ')':
                    return Operator.CloseBracket;
                case '*':
                    return Operator.Multiply;
                case '/':
                    return Operator.Divide;
                case '+':
                    return Operator.Plus;
                case '-':
                    return Operator.Minus;
                default:
                    return Operator.None;
            }
        }
    }
}