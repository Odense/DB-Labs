using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Lab2.Database
{
    class RandomGen
    {
        private Random random = new Random();

        public double getRandomDouble(double max)
        {
            return random.NextDouble() * max;
        }

        public int getRandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public bool getRandomBoolean()
        {
            if (random.Next(3) == 1) return true;
            return false;
        }

        public DateTime getRandomBirthDate()
        {
            DateTime randomDate = DateTime.Today.AddDays(-random.Next(30)).AddMonths(-random.Next(12)).AddYears(-random.Next(5,18));
            return randomDate;
        }

        public DateTime getRandomCreationDate()
        {
            DateTime randomDate = DateTime.Today.AddYears(-random.Next(11)).AddMonths(-random.Next(12));
            return randomDate;
        }

        public string getRandomName()
        {
            return Names[random.Next(Names.Length)];
        }

        public string getRandomSurname()
        {
            return Surnames[random.Next(Surnames.Length)];
        }

        public string[] getLessons()
        {
            return Lessons;
        }

        public string getRandomSpec()
        {
            int randomInt = random.Next(4);
            if (randomInt == 0) return "math";
            else if (randomInt == 1) return "economics";
            else if (randomInt == 2) return "english";
            else return "ecology";
        }

        public string[] getClassnames()
        {
            string[] classNames = new string[20];
            for (int i = 0; i < 20; i++)
            {
                int classNumber = random.Next(1, 12);
                string classLetter;
                if (random.Next(3) == 1) classLetter = "b";
                else classLetter = "a";
                classNames[i] = classNumber.ToString() + classLetter;
            }
            return classNames;
        }

        public long getRandomIndex(List<long> l)
        {
            int val = random.Next(0, l.Count);
            return l.ElementAt(val);
        }

        private string[] Names = new string[]
        {
            "Alice",
            "Ivan",
            "Andriy",
            "Mykhail",
            "Dmytro",
            "Kate",
            "Ann",
            "Nadia",
            "Oleg"
        };

        private string[] Surnames = new string[]
        {
            "Logarin",
            "Torgozov",
            "Ivashchenko",
            "Levanenko",
            "Kurlan",
            "Taranenko",
            "Lipovskih",
            "Andrienko"
        };

        private string[] Lessons = new string[]
        {
            "Math",
            "Economic",
            "History",
            "Biology",
            "Law",
            "Geography",
            "Physics",
            "Chemistry",
            "Literature"
        };
    }
}
