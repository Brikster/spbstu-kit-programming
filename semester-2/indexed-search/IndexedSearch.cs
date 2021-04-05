using System;
using System.Collections.Generic;
using System.IO;

namespace PolytechHomeworks
{
    public static class IndexedSearch
    {
        private struct File
        {
            public string Name;
            public string AbosolutePath;
        }
        
        private static readonly List<File>[] files = new List<File>[256];

        private static void Main()
        {
            Console.WriteLine("Введите имя стартовой директории: ");
            string beginPath = Console.ReadLine();
            
            Console.WriteLine("Индексируем директорию...");
            
            try
            {
                CollectFiles(beginPath);
            }
            catch
            {
                Console.WriteLine("Директория не найдена.");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Y - поиск файла по имени, N - выход.");

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Y)
                {
                    Console.Write("Введите имя файла для поиска (без учёта регистра): ");
                    FindFileByName(Console.ReadLine());
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey(true);
                }
                else break;
            }
        }

        private static void CollectFiles(string path)
        {
            foreach (string dirFile in Directory.GetFiles(path))
            {
                string fileName = Path.GetFileName(dirFile);
                byte hashCode = CalculateHashCode(fileName.ToLower());
                
                File file = new File()
                {
                    AbosolutePath = dirFile,
                    Name = fileName
                };
                
                try
                {
                    files[hashCode].Add(file);
                }
                catch
                {
                    files[hashCode] = new List<File> { file };
                }
            }

            foreach (string directory in Directory.GetDirectories(path))
            {
                try
                {
                    CollectFiles(directory);
                }
                catch
                {
                    Console.WriteLine($"[Warn] Невозможно прочитать {directory}.");
                }
            }
        }

        private static void FindFileByName(string fileName)
        {
            fileName = fileName.ToLower();
            byte hashCode = CalculateHashCode(fileName);
            List<File> suitableFiles = files[hashCode];
            
            bool found = false;
            try
            {
                foreach (File file in suitableFiles)
                {
                    if (file.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        Console.WriteLine(file.AbosolutePath);
                    }
                }
            }
            finally
            {
                if (!found) Console.WriteLine("Файл не найден.");
            }
        }

        private static byte CalculateHashCode(string fileName)
        {
            uint fileNameSum = 0;
            foreach (char c in fileName)
            {
                fileNameSum += c;
            }

            return (byte) (fileNameSum % 256);
        }
    }
}