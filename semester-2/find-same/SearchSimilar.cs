using System;
using System.Collections.Generic;

namespace PolytechHomeworks
{
    public static class SearchSimilar
    {
        public struct SHuman
        {
            public string Surname;          // фамилия
            public string Firstname;        // имя
            public string Patronymic;       // отчество
            public int Year;                // год рождения
            public SHuman(string surname, string firstname, string patronymic, int year)
            {
                this.Surname = surname;
                this.Firstname = firstname;
                this.Patronymic = patronymic;
                this.Year = year;
            }

            public override string ToString() => $"{Firstname} {Patronymic} {Surname}, {Year}";
        }

        public static void Main2()
        {
            SHuman[] group = {
                new SHuman("Пушкин", "Александр", "Сергеевич", 1799),
                new SHuman("Ломоносов", "Михаил", "Васильевич", 1711),
                new SHuman("Тютчев", "Фёдор", "Иванович", 1803),
                new SHuman("Суворов", "Александр", "Васильевич", 1729),
                new SHuman("Менделеев", "Дмитрий", "Иванович", 1834),
                new SHuman("Ахматова", "Анна", "Андреевна", 1889),
                new SHuman("Володин", "Александр", "Моисеевич", 1919),
                new SHuman("Мухина", "Вера", "Игнатьевна", 1889),
                new SHuman("Верещагин", "Петр", "Петрович", 1834)
            };

            foreach (List<SHuman> humans in FindSimilar(group))
            {
                Console.WriteLine("======================================");

                foreach (SHuman human in humans)
                    Console.WriteLine(human);
            }

            Console.WriteLine("======================================");
            Console.ReadKey();
        }

        private static List<List<SHuman>> FindSimilar(SHuman[] humans)
        {
            List<List<SHuman>> groups = new List<List<SHuman>>
                { new List<SHuman>() { humans[0] } };

            for (int i = 1; i < humans.Length; i++)
            {
                SHuman human = humans[i];
                List<SHuman> selectedGroup = null;

                foreach (List<SHuman> group in groups)
                {
                    if (GroupHasSimilar(group, human))
                    {
                        if (selectedGroup == null)
                            (selectedGroup = group).Add(human);
                        else
                        {
                            selectedGroup.AddRange(group);
                            group.Clear();
                        }
                    }
                }

                if (selectedGroup == null)
                    groups.Add(new List<SHuman>() { human });
            }

            groups.RemoveAll(group => group.Count == 0);

            return groups;
        }

        private static bool GroupHasSimilar(List<SHuman> group, SHuman human)
        {
            foreach (SHuman humanToCompare in group)
            {
                bool similar = humanToCompare.Firstname.Equals(human.Firstname);
                similar |= humanToCompare.Surname.Equals(human.Surname);
                similar |= humanToCompare.Patronymic.Equals(human.Patronymic);
                similar |= humanToCompare.Year == human.Year;
                if (similar) return true;
            }

            return false;
        }
    }
}