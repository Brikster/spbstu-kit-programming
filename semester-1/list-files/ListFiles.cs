using System;
using System.IO;
using System.Linq;

namespace PolytechHomeworks
{
    public static class ListFiles
    {
        public static void Main2()
        {
            Console.Write("Введите абсолютный или относительный путь до директории: ");
            string path = Console.ReadLine();

            if (Directory.Exists(path))
            {
                PrintDirectoryFilesTree(path);
            }
            else
            {
                Console.WriteLine("Директория не найдена.");
            }

            Console.WriteLine("Нажмите любую клавишу для выхода из программы...");
            Console.ReadKey();
        }

        private static void PrintDirectoryFilesTree(string path, int level = 0)
        {
            if (level == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(Path.GetFullPath(path));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (!Directory.GetFileSystemEntries(path).Any())
                {
                    Console.WriteLine(new string(' ', level * 3) + $"L {Path.GetFileName(path)}");
                    return;
                }

                Console.WriteLine(new string(' ', level * 3) + $"+ {Path.GetFileName(path)}");
            }

            level++;
            
            Console.ResetColor();
            
            foreach (string filePath in Directory.GetFiles(path))
            {
                Console.WriteLine(new string(' ', level * 3) + $"L {Path.GetFileName(filePath)}");
            }

            foreach (string dirPath in Directory.GetDirectories(path))
            {
                PrintDirectoryFilesTree(dirPath, level);
            }
        }
    }
}