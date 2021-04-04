using System;

namespace PolytechHomeworks
{
    public static class SubProgram
    {
        public static void Main2()
        {
            Console.Write("Введите длину листа: ");
            if (!uint.TryParse(Console.ReadLine(), out var length) || length == 0)
            {
                Console.WriteLine("Ожидалось целое положительное число.");
                return;
            }
            
            Console.Write("Введите ширину листа: ");
            if (!uint.TryParse(Console.ReadLine(), out var width) || width == 0)
            {
                Console.WriteLine("Ожидалось целое положительное число.");
                return;
            }
            
            CalculateBestDimensions(length, width, out uint bestLength, out uint bestWidth, out uint bestHeight);
            Console.WriteLine($"Размеры коробочки: {bestLength} x {bestWidth} x {bestHeight} (Д x Ш x В)");
            Console.ReadKey();
        }
        
        private static void CalculateBestDimensions(uint paperLength, uint paperWidth, out uint length, out uint width, out uint height) {
            uint b = 4 * (paperLength + paperWidth);
            double discriminant = Math.Sqrt(16 * Math.Pow(paperLength + paperWidth, 2) - 24 * paperLength * paperWidth);

            uint x1 = (uint) Math.Round((b + discriminant) / 48 / 2, MidpointRounding.AwayFromZero) * 2;
            uint x2 = (uint) Math.Round((b - discriminant) / 48 / 2, MidpointRounding.AwayFromZero) * 2;
            
            height = paperLength <= 2 * x1 || paperWidth <= 2 * x1 ? x2 : x1;
            length = paperLength - 2 * height;
            width = paperWidth - 2 * height;
        }
    }
}