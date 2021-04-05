using System;

namespace PolytechHomeworks
{
    public static class BestTeacher
    {
        public static void Main2()
        {
            double[,] marks = {{3.6, 3.1, 2.8, 1, 4, 3.3, 3.2, 3 },
                {3.5, 3.6, 4.1, 3.9, 3.5, 5, 4, 5 },
                {2.2, 2.7, 3.1, 3, 4.5, 2.2, 3.1, 3.7},
                {4.2, 3.4, 3, 4.3, 4.1, 4.6, 4.4, 4.5},
                {4.7, 4.1, 3.6, 2.1, 2.7, 2, 2.5, 2.7}};

            if (marks.GetLength(0) > 1000 || marks.GetLength(1) > 1000)
            {
                Console.WriteLine("Количество учителей и количество учеников должны быть <= 1000.");
                Console.ReadKey();
                return;
            }

            FindBestTeacher(marks, out int index, out double average);

            Console.WriteLine(index == -1
                ? "Лучший преподаватель не найден."
                : $"Лучший преподаватель имеет индекс {index} и балл {average}.");

            Console.ReadKey();
        }

        private static void FindBestTeacher(double[,] marks, out int bestTeacherIndex, out double bestTeacherMark)
        {
            int studentsCount = marks.GetLength(1);

            bestTeacherIndex = -1;
            bestTeacherMark = 0;
            
            for (int teacherIndex = 0; teacherIndex < marks.GetLength(0); teacherIndex++)
            {
                double min = 5;
                double max = 0;

                double sum = 0;
                
                for (int studentIndex = 0; studentIndex < studentsCount; studentIndex++)
                {
                    double mark = marks[teacherIndex, studentIndex];

                    if (mark < min) min = mark;
                    if (mark > max) max = mark;

                    sum += mark;
                }

                double average = (sum - min - max) / (studentsCount - 2);

                if (average > bestTeacherMark)
                {
                    bestTeacherMark = average;
                    bestTeacherIndex = teacherIndex;
                }
            }
        }
    }
}