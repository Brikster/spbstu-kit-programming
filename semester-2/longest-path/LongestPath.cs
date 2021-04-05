using System;
using System.Collections.Generic;

namespace PolytechHomeworks
{
    public static class LongestPath
    {
        private struct Cell
        {
            public readonly int Value;
            public bool Visited;
            public int Steps;

            public int X; 
            public int Y;
            
            public int NextLongestX; 
            public int NextLongestY;

            public Cell(int value)
            {
                Value = value;
                Visited = false;
                Steps = 0;
                X = -1; 
                Y = -1;
                NextLongestX = -1; 
                NextLongestY = -1;
            }
        }
        
        public static void Main2()
        {
            Cell[,] cells = new[,]
            {
                {new Cell(2), new Cell(5), new Cell(1), new Cell(0)},
                {new Cell(3), new Cell(3), new Cell(1), new Cell(9)},
                {new Cell(4), new Cell(4), new Cell(7), new Cell(8)}
            };

            Dfs(cells);
        }

        private static void Dfs(Cell[,] cells)
        {
            Cell maxStepsCell = new Cell();
            int maxSteps = -1;
            
            int maxX = cells.GetLength(0);
            int maxY = cells.GetLength(1);

            for (int x = 0; x < maxX; x++)
            for (int y = 0; y < maxY; y++)
                cells[x, y] = new Cell(cells[x, y].Value) {X = x, Y = y};

            for (int x = 0; x < maxX; x++)
            for (int y = 0; y < maxY; y++)
            {
                Cell cell = cells[x, y];

                if (!cell.Visited)
                {
                    VisitCell(cells, x, y);
                    cell = cells[x, y];
                }

                if (cell.Steps > maxSteps)
                {
                    maxStepsCell = cell;
                    maxSteps = cell.Steps;
                }
            }

            int nextX = maxStepsCell.X;
            int nextY = maxStepsCell.Y;

            string coords = "";
            string values = "";

            while (nextX != -1 && nextY != -1)
            {
                Cell cell = cells[nextX, nextY];
                
                if (coords.Length != 0)
                {
                    coords += " -> ";
                    values += " -> ";
                }
                
                coords += $"({cell.X}, {cell.Y})";
                values += $"{cell.Value}";
                
                nextX = cell.NextLongestX;
                nextY = cell.NextLongestY;
            }
            
            Console.WriteLine(coords);
            Console.WriteLine(values);
        }

        private static void VisitCell(Cell[,] cells, int x, int y)
        {
            int maxCellX = -1, maxCellY = -1,
                maxCellSteps = -1, maxSteps = -1;

            List<Cell> cellsToCheck = new List<Cell>();

            if (x != cells.GetLength(0) - 1) cellsToCheck.Add(cells[x + 1, y]);
            if (y != cells.GetLength(1) - 1) cellsToCheck.Add(cells[x, y + 1]);
            if (x != 0) cellsToCheck.Add(cells[x - 1, y]);
            if (y != 0) cellsToCheck.Add(cells[x, y - 1]);

            foreach (Cell cellToCheck in cellsToCheck)
            {
                if (cellToCheck.Value <= cells[x, y].Value) continue;
                if (!cellToCheck.Visited) VisitCell(cells, cellToCheck.X, cellToCheck.Y);

                Cell updated = cells[cellToCheck.X, cellToCheck.Y];
                if (updated.Steps > maxSteps)
                {
                    maxSteps = updated.Steps;
                    maxCellX = updated.X;
                    maxCellY = updated.Y;
                    maxCellSteps = updated.Steps;
                }
            }
            
            cells[x, y].Visited = true;
            cells[x, y].NextLongestX = maxCellX;
            cells[x, y].NextLongestY = maxCellY;
            cells[x, y].Steps = maxCellSteps + 1;
        }
    }
}