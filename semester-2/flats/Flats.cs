using System;
using System.Collections.Generic;

namespace PolytechHomeworks
{
    public static class Flats
    {
        // Структура, описывающая помещение (не обязательно комнату)
        private struct Room
        {
            // Название помещения
            public string Name;
            // Жилое помещение или нежилое
            public bool Living;
            // Площадь помещения
            public float Square;
        }

        private enum Facility
        {
            NEAR_SUBWAY,
            HAS_INTERNET,
            HAS_TELEVISION,
            SEPARETE_BATHROOM
        }
        
        // Структура, описывающая квартиру
        private struct Flat
        {
            // Помещения в квартире (жилые и нежилиые)
            public List<Room> Rooms;
            // Адрес квартиры
            public string Address;
            // Стоимость квартиры
            public int Price;
            // Необходим ли залог
            public bool NeedDeposit;
            // Удобства, присутствующие в предложении
            public List<Facility> Facilities;
            
            public float TotalSquare { 
                get
                {
                    float square = 0;
                    foreach (Room room in Rooms) square += room.Square;
                    return square;
                }
            }
        }
        
        public static void Main2()
        {
            List<Flat> flats = new List<Flat>();

            do
            {
                Flat flat = new Flat();
            
                Console.Write("Введите адрес квартиры: ");
                flat.Address = Console.ReadLine();
            
                Console.WriteLine("====== Стоимость ======");
                Console.Write("Введите стоимость аренды квартиры: ");
                    
                while (!int.TryParse(Console.ReadLine(), out flat.Price) || flat.Price <= 0)
                {
                    Console.WriteLine("Ожидалось натуральное число.");
                    Console.Write("Введите стоимость аренды квартиры: ");
                }
                
                Console.WriteLine("====== Удобства ======");
                
                flat.Facilities = new List<Facility>();
                
                foreach (Facility facility in Enum.GetValues(typeof(Facility)))
                {
                    Console.Write($"Нажмите Y, если присутствует удобство {facility.ToString()}, иначе - любую другую клавишу. ");
            
                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        flat.Facilities.Add(facility);
                        Console.WriteLine("(да)");
                    } else 
                        Console.WriteLine("(нет)");
                }
                
                Console.WriteLine("====== Залог ======");
                Console.Write("Нажмите Y, если нужен залог, иначе - любую другую клавишу. ");
                flat.NeedDeposit = Console.ReadKey(true).Key == ConsoleKey.Y;
                Console.WriteLine("(" + (flat.NeedDeposit ? "да" : "нет") + ")");
            
                flat.Rooms = new List<Room>();
                
                Console.WriteLine("====== Комнаты ======");
            
                int roomNum = 1;
                do
                {
                    Room room = new Room();
                    Console.WriteLine($"* Ввод помещения №{roomNum} *");
                    
                    Console.Write("Введите название помещения: ");
                    room.Name = Console.ReadLine();
                    
                    Console.Write("Введите площадь помещения: ");
                    
                    while (!float.TryParse(Console.ReadLine(), out room.Square) || room.Square <= 0)
                    {
                        Console.WriteLine("Ожидалось натуральное число.");
                        Console.Write("Введите площадь помещения: ");
                    }
            
                    Console.Write("Нажмите Y, если помещение жилое, иначе - любую другую клавишу. ");
                    room.Living = Console.ReadKey(true).Key == ConsoleKey.Y;
                    Console.WriteLine("(" + (room.Living ? "да" : "нет") + ")");
                    
                    flat.Rooms.Add(room);
                    
                    Console.WriteLine("Продолжить ввод комнат? Нажмите Y для продолжения.");
                } 
                while (Console.ReadKey(true).Key == ConsoleKey.Y);
            
                flats.Add(flat);
                
                Console.WriteLine("Продолжить ввод квартир? Нажмите Y для продолжения.");
            } 
            while (Console.ReadKey(true).Key == ConsoleKey.Y);

            uint rooms;
            float square;
            
            Console.WriteLine("====== Вычисление медианной стоимости ======");
            Console.Write("Введите количество комнат (жилых помещений) для поиска: ");
            while (!uint.TryParse(Console.ReadLine(), out rooms) || rooms == 0)
            {
                Console.WriteLine("Ожидалось натуральное число.");
                Console.Write("Введите количество комнат (жилых помещений) для поиска: ");
            }
            
            Console.Write("Введите минимальную площадь для поиска: ");
            while (!float.TryParse(Console.ReadLine(), out square) || square <= 0)
            {
                Console.WriteLine("Ожидалось положительное число.");
                Console.Write("Введите минимальную площадь для поиска: ");
            }

            int medianPrice = FindFlatsPriceMedian(flats, rooms, square);
            Console.WriteLine(medianPrice == 0
                ? "Не найдено квартир по представленным параметрам."
                : $"Медианная стоимость квартир с введёнными параметрами: {medianPrice}.");
            Console.ReadKey();
        }

        private static int FindFlatsPriceMedian(List<Flat> flats, uint rooms, float totalSquare)
        {
            List<int> prices = new List<int>();
            foreach (Flat flat in flats)
            {
                int flatRoomsCount = 0;
                foreach (Room room in flat.Rooms)
                {
                    // Интересно количество именно жилых помещений
                    if (room.Living) flatRoomsCount++;
                }
                
                if (flatRoomsCount == rooms && flat.TotalSquare > totalSquare)
                {
                    prices.Add(flat.Price);
                }
            }
            
            // Возможная реализация отсеивания с помощью методов массивов и коллекций
            // 
            // List<int> prices = flats
            //     .Where(flat => flat.Rooms.Count(r => r.Living) == rooms)
            //     .Where(flat => flat.TotalSquare >= minTotalSquare)
            //     .Select(flat => flat.Price)
            //     .ToList();

            /*
             * Реализован поиск медианы за линейное время O(n),
             * поэтому программа получилась длинной.
             *
             * Можно отбросить большинство нижеобъявленных функций
             * и использовать FindMedianNLogN(), но этот алгоритм
             * менее эффективен.
             *
             * Также возможно использовать сортировку пузырьком,
             * тогда сложность будет O(n^2).
             */
            return FindMedian(prices.ToArray());
        }

        // Находим медиану за O(n) с помощью алгоритма QuickSelect
        // с выбором медианного pivot
        private static int FindMedian(int[] arr)
        {
            if (arr.Length == 0) return 0;
            
            if (arr.Length % 2 == 1)
                return QuickSelect(arr, arr.Length / 2);
            else
                return (QuickSelect(arr, arr.Length / 2 - 1) +
                        QuickSelect(arr, arr.Length / 2)) / 2;
        }

        // QuickSelect с медианным pivot
        private static int QuickSelect(int[] arr, int index)
        {
            if (arr.Length == 1) return arr[0];

            int pivot = FindPivot(arr);

            List<int> lows = new List<int>();
            List<int> highs = new List<int>();
            List<int> pivots = new List<int>();
            
            /*
             * Делим элементы на три массива:
             * меньше, больше и равные pivot
             */
            foreach (int el in arr)
            {
                if (el > pivot)
                    highs.Add(el);
                else if (el < pivot)
                    lows.Add(el);
                else pivots.Add(el);
            }

            if (index < lows.Count)
                return QuickSelect(lows.ToArray(), index);
            else if (index < lows.Count + pivots.Count)
                return pivots[0];
            else
                return QuickSelect(highs.ToArray(), index - lows.Count - pivots.Count);
        }

        // Выбор медианного pivot
        private static int FindPivot(int[] prices)
        {
            // Ищем медиану обычным O(n log n) алгоритмом, что с учётом маленького массива
            // не окажет влияния на эффективность.
            if (prices.Length < 5)
                return FindNLogNMedian(prices);
            
            // Делим массив на равные группы по 5 элементов
            // и записываем их в jagged массив.
            int[][] chunks = new int[prices.Length / 5][];
            
            for (int i = 0; i < chunks.Length; i++) 
                chunks[i] = new int[5];

            int chunkIndex = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                if (chunkIndex + 1 > chunks.Length) break;
                chunks[chunkIndex][i % 5] = prices[i];
                if ((i + 1) % 5 == 0) chunkIndex++;
            }
            
            // Ищем медиану в каждом подмассиве из 5 элементов,
            // в данном случае эффективность опять же не нарушится
            // благодаря маленькому размеру массива.
            int[] medians = new int[chunks.Length];
            for (int i = 0; i < chunks.Length; i++)
            {
                int[] chunk = chunks[i];
                QuickSort(chunk, 0, 4);
                medians[i] = chunk[2];
            }
            
            return FindMedian(medians);
        }

        // Поиск медианы за O(n log n), нужен при выборе pivot
        private static int FindNLogNMedian(int[] arr)
        {
            QuickSort(arr, 0, arr.Length - 1);
            return arr.Length % 2 == 1
                ? arr[arr.Length / 2] 
                : (arr[arr.Length / 2] + arr[arr.Length / 2 - 1]) / 2;
        }

        // Алгоритм QuickSort для сортировки массива, используется в FindNLogNMedian
        private static void QuickSort(int[] arr, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex) return;

            int pivotIndex = Partition(arr, minIndex, maxIndex);
            QuickSort(arr, minIndex, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, maxIndex);
        }
        
        private static int Partition(int[] arr, int minIndex, int maxIndex)
        {
            int index = minIndex - 1;
            for (int i = minIndex; i < maxIndex; i++)
            {
                if (arr[i] < arr[maxIndex])
                {
                    index++;
                    Swap(ref arr[index], ref arr[i]);
                }
            }

            index++;
            Swap(ref arr[index], ref arr[maxIndex]);
            return index;
        }

        private static void Swap(ref int x, ref int y)
        {
            int t = x;
            x = y; y = t;
        }
    }
}