using System;

public static class TextScripting
{
    
    public static int Main2()
    {
        Console.Write("Введите число: ");

        int inputNumber;
        try
        {
            inputNumber = Convert.ToInt32(Console.ReadLine());
            if (inputNumber < 1 || inputNumber > 99)
            {
                throw new FormatException();
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Программа принимает только целые числа от 1 до 99");
            Console.ReadKey();
            return 1;
        }

        string readableName;
        if (inputNumber < 20)
        {
            if (inputNumber < 10)
            {
                readableName = GetUnitName(inputNumber);
            }
            else
            {
                string unitName;
                readableName = inputNumber switch
                {
                    10 => "десять", 11 => "одиннадцать", 
                    12 => "двенадцать", 13 => "тринадцать",
                    _ => $"{(unitName = GetUnitName(inputNumber % 10)).Substring(0, unitName.Length - 1)}надцать"
                };
            }
        }
        else
        {
            var decade = inputNumber / 10;
            var unit = inputNumber % 10;

            var decadeName = decade switch
            {
                2 => "двадцать", 3 => "тридцать", 4 => "сорок",
                5 => "пятьдесят", 6 => "шестьдесят", 7 => "семьдесят",
                8 => "восемьдесят", 9 => "девяносто",
                _ => throw new InvalidOperationException()
            };

            readableName = unit == 0
                ? $"{decadeName}"
                : $"{decadeName} {GetUnitName(unit)}";
        }

        Console.WriteLine(readableName);
        Console.ReadKey();

        return 0;
    }

    private static string GetUnitName(int number)
    {
        return number switch
        {
            1 => "один", 2 => "два", 3 => "три",
            4 => "четыре", 5 => "пять", 6 => "шесть",
            7 => "семь", 8 => "восемь", 9 => "девять",
            _ => throw new InvalidOperationException("Операция допустима только для целых чисел от 1 до 9")
        };
    }
}