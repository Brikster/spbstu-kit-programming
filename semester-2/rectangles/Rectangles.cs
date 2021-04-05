using System;

namespace PolytechHomeworks
{
    public static class Rectangles
    {
        private struct Rectangle
        {
            public double X1; 
            public double X2;
            public double Y1; 
            public double Y2;
        }

        public static void Main2()
        {
            Console.Write("Введите количество прямоугольников: ");
            
            if (!int.TryParse(Console.ReadLine(), out int n))
            {
                Console.WriteLine("Ожидалось натуральное число.");
                Console.ReadKey();
                return;
            }
            
            Rectangle[] rectangles = new Rectangle[n];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Ввод нового прямоугольника...");
                rectangles[i] = new Rectangle();
                
                if (!ReadCoord(out var x1, "Введите первую координату X: ")) return;
                if (!ReadCoord(out var y1, "Введите первую координату Y: ")) return;
                if (!ReadCoord(out var x2, "Введите вторую координату X: ")) return;
                if (!ReadCoord(out var y2, "Введите вторую координату Y: ")) return;

                rectangles[i].X1 = x1; rectangles[i].X2 = x2;
                rectangles[i].Y1 = y1; rectangles[i].Y2 = y2;
            }
            
            FindEmbracingRectangle(rectangles, out double resultX1, out double resultX2, out double resultY1, out double resultY2);
            Console.WriteLine("Координаты точек охватывающего прямоугольника: " +
                              $"[{resultX1} ; {resultY1}], [{resultX1} ; {resultY2}], " +
                              $"[{resultX2} ; {resultY1}], [{resultX2} ; {resultY2}]");
        }

        private static bool ReadCoord(out double coord, string message)
        {
            Console.Write(message);
            
            if (!double.TryParse(Console.ReadLine(), out coord))
            {
                Console.WriteLine("Ожидалось целое число.");
                Console.ReadKey();
                return false;
            }

            return true;
        }

        private static void FindEmbracingRectangle(Rectangle[] rectangles, out double x1, out double x2, out double y1, out double y2)
        {
            double xMin = Double.MaxValue; double xMax = Double.MinValue;
            double yMin = Double.MaxValue; double yMax = Double.MinValue;
            
            foreach (Rectangle rectangle in rectangles)
            {
                Min(rectangle.X1, rectangle.X2, ref xMin);
                Min(rectangle.Y1, rectangle.Y2, ref yMin);
                Max(rectangle.X1, rectangle.X2, ref xMax);
                Max(rectangle.Y1, rectangle.Y2, ref yMax);
            }

            x1 = xMin; x2 = xMax;
            y1 = yMin; y2 = xMax;
        }

        private static void Min(double value0, double value1, ref double targetValue)
        {
            double min = Math.Min(value0, value1);
            targetValue = targetValue > min ? min : targetValue;
        }

        private static void Max(double value0, double value1, ref double targetValue)
        {
            double max = Math.Max(value0, value1);
            targetValue = targetValue < max ? max : targetValue;
        }
    }
}