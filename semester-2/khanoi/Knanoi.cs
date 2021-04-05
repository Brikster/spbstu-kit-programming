using System;

namespace PolytechHomeworks
{
    public static class Khanoi
    {
        private struct Tower
        {
            public short[] Disks;
            public short CurrentCount;
        }
        
        private static short currentStep = 1;
        private static short disksCount;
        
        private static Tower tower1;
        private static Tower tower2;
        private static Tower tower3;

        public static void Main2()
        {
            Console.Write("Введите количество колец: ");
            if (!short.TryParse(Console.ReadLine(), out disksCount) || disksCount < 1 || disksCount > 10)
            {
                Console.WriteLine("Ожидалось число от 1 до 10.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Нажимайте любую клавишу для перехода к следующему шагу.");
            Console.WriteLine();
            
            Init();
            PrintStepInfo();
            Move(ref tower1, ref tower2, ref tower3, disksCount);
            
            Console.WriteLine("Головоломка решена.");
            Console.ReadKey();
        }

        private static void Init()
        {
            tower1.Disks = new short[disksCount];
            tower2.Disks = new short[disksCount];
            tower3.Disks = new short[disksCount];
            
            short diskSize = (short) (disksCount - 1);
            short index = 0;
            
            while (index <= diskSize)
            {
                tower1.Disks[index] = (short) (disksCount - index);
                index++;
            }

            tower1.CurrentCount = disksCount;
        }

        private static void PrintStepInfo()
        {
            Console.WriteLine($"Шаг №{currentStep++}: ");
            
            short index = (short) (disksCount - 1);
            while (index >= 0)
            {
                Console.WriteLine(tower1.Disks[index] + "   " + tower2.Disks[index] + "   " + tower3.Disks[index]);
                index--;
            }
            
            Console.WriteLine();

            Console.ReadKey(true);
        }

        private static void Move(ref Tower from, ref Tower to, ref Tower temp, short count)
        {
            if (count > 1) Move(ref from, ref temp, ref to, (short) (count - 1));

            to.Disks[to.CurrentCount] = from.Disks[from.CurrentCount - 1];
            from.Disks[from.CurrentCount - 1] = 0;
            
            from.CurrentCount--;
            to.CurrentCount++;
            
            PrintStepInfo();
            
            if (count > 1) Move(ref temp, ref to, ref from, (short) (count - 1));
        }
    }
}