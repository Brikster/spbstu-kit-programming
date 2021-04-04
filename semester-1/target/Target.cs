using System;
using System.Threading;

namespace PolytechHomeworks
{
    public static class Target
    {
        private static uint _maxScore = 10;
        private static uint _step = 1;
        private static uint _wide = 15;
        private static uint _delay = 30;

        private static readonly Random Random = new Random();

        public static void Main2()
        {
            while (true)
            {
                Console.WriteLine("Обновить настройки игры? (Y)");
                if (Console.ReadKey(true).Key == ConsoleKey.Y && !UpdateGameSettings())
                {
                    Console.WriteLine("Введены некорректные данные, будут использоваться стандартные настройки.");
                    continue;
                }
                
                uint points = 0;

                do
                {
                    StartGameRound(ref points);
                    Console.WriteLine("Закончить игру? (Y)");
                } while (Console.ReadKey(true).Key != ConsoleKey.Y);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Вы закончили игру. За всё время вы заработали {points} очков.");
                Console.ReadKey();
                break;
            }
        }
        
        // Запускает очередной раунд игры
        private static void StartGameRound(ref uint points)
        {
            StartAimingAction("X", ConsoleColor.Blue, out var x);
            StartAimingAction("Y", ConsoleColor.Red, out var y);
            
            var radius = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            var currentRoundPoints = (uint) Math.Max(_maxScore - Math.Truncate(radius / _step), 0);
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Вы заработали {currentRoundPoints} очков за этот раунд.");
            Console.ResetColor();

            points += currentRoundPoints;
        }
        
        // Запускает процесс "прицеливания" по оси
        private static void StartAimingAction(string axisName, ConsoleColor axisColor, out double value)
        {
            Console.Clear();
            Console.ForegroundColor = axisColor;
            Console.WriteLine($"Вы целитесь по оси {axisName}: ");
            Console.ResetColor();

            double currentValue = -_wide;

            var up = true;
            
            do
            {
                var randomValue = Random.NextDouble();
                if (randomValue == 0) randomValue = 1;
                
                currentValue += (up ? 1 : -1) * randomValue;

                if (Math.Abs(currentValue) >= _wide)
                {
                    currentValue += (up ? -2 : 2) * _wide;
                    currentValue *= -1;
                    up = !up;
                } 

                Console.CursorLeft = 0;
                Console.Write(currentValue);
                
                Thread.Sleep((int) _delay);
            } while (!Console.KeyAvailable);

            Console.ReadKey(true);
            Console.WriteLine();

            value = currentValue;
        }

        // Запрашивает у пользователя параметры настроек игры
        private static bool UpdateGameSettings()
        {
            if (!TryReadParameter("Введите количество секций: ", out _maxScore, 1, 50) || 
                !TryReadParameter("Введите ширину секции: ", out _step, 1, 10) ||
                !TryReadParameter("Введите ширину (радиус) мишени: ", out _wide, 1, 50) ||
                !TryReadParameter("Введите задержку: ", out _delay, 1, 300)) 
            {
                return false;
            }

            return _wide >= _maxScore * _step;
        }

        // Запрашивает параметр у пользователя и проверяет его на корректность
        private static bool TryReadParameter(string desc, out uint parameter, int rangeStart, int rangeEnd)
        {
            Console.Write(desc);
            if (!uint.TryParse(Console.ReadLine(), out parameter) || (parameter < rangeStart || parameter > rangeEnd))
            {
                Console.WriteLine($"Ожидалось число в диапазоне [{rangeStart}; {rangeEnd}]");
                return false;
            }

            return true;
        }
    }
}