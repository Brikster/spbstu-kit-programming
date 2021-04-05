using System;

namespace PolytechHomeworks
{
    // Структура, описывающая вершину
    public struct Vertex
    {
        public double X;
        public double Y;
        public double Test { get; set; }

        public double getX()
        {
            return X;
        }
    }

    // Структура, описывающая многоугольник
    public struct Polygon
    {
        public Vertex[] Vertices;
        public ushort LineWidth;
        public ConsoleColor LineColor;
        public bool Filling;
    }
    
    public static class Polygons
    {
        // Допустимая погрешность для сравнения double'ов
        private const double TOLERANCE = 0.01;
        
        public static void Main2()
        {
            Polygon[] polygons = new []
            {
                // Равносторонний треугольник с нужной толщиной линии
                new Polygon()
                {
                    Vertices = new []
                    {
                        new Vertex() { X = 0, Y = 0 },
                        new Vertex() { X = 0.5, Y = Math.Sqrt(3) / 2 },
                        new Vertex() { X = 1, Y = 0 }
                    },
                    LineWidth = 10
                },
                // Равносторонний треугольник с неподходящей толщиной линии
                new Polygon()
                {
                    Vertices = new []
                    {
                        new Vertex() { X = 0, Y = 0 },
                        new Vertex() { X = 0.5, Y = Math.Sqrt(3) / 2 },
                        new Vertex() { X = 1, Y = 0 }
                    },
                    LineWidth = 12
                },
                // Неравносторонний треугольник с нужной толщиной линии
                new Polygon()
                {
                    Vertices = new []
                    {
                        new Vertex() { X = 0, Y = 5 },
                        new Vertex() { X = 5, Y = 0 },
                        new Vertex() { X = 0, Y = 0 }
                    },
                    LineWidth = 10
                },
                // Четырёхугольник с неподходящей толщиной линии
                new Polygon()
                {
                    Vertices = new []
                    {
                        new Vertex() { X = 0, Y = 5 },
                        new Vertex() { X = 5, Y = 0 },
                        new Vertex() { X = 0, Y = 0 },
                        new Vertex() { X = 5, Y = 5 }
                    },
                    LineWidth = 15
                }
            };
            
            Console.WriteLine($"Количество равносторонних треугольников с толщиной 10 равно {CountTriangles(polygons, 10)}.");
            Console.ReadKey();
        }
        
        private static int CountTriangles(Polygon[] polygons, ushort width)
        {
            int triangles = 0;
            
            foreach (Polygon polygon in polygons)
            {   
                if (polygon.Vertices.Length != 3) continue;
                if (polygon.LineWidth != width) continue;
                
                double firstSide = CalculateSideSquared(polygon.Vertices[0], polygon.Vertices[1]);
                double secondSide = CalculateSideSquared(polygon.Vertices[0], polygon.Vertices[2]);
                double thirdSide = CalculateSideSquared(polygon.Vertices[1], polygon.Vertices[2]);

                if (Math.Abs(firstSide - secondSide) < TOLERANCE
                    && Math.Abs(secondSide - thirdSide) < TOLERANCE
                    && Math.Abs(thirdSide - firstSide) < TOLERANCE)
                { triangles++; }
            }

            return triangles;
        }

        private static double CalculateSideSquared(Vertex vertex1, Vertex vertex2)
        {
            return Math.Pow(vertex1.X - vertex2.X, 2) + Math.Pow(vertex1.Y - vertex2.Y, 2);
        }
    }
}