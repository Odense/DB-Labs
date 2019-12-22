using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Lab2.Model;
using Lab2.Database;
using static Lab2.Database.PupilDAO;

namespace Lab2.View
{
    public enum Entity
    {
        Null,
        Junction,
        Pupil,
        Teacher,
        Exception
    }

    class ViewClass
    {
        public Entity entity;
        private List<Class> classes;
        private List<Lesson> lessons;

        public ViewClass(List<Class> classes, List<Lesson> lessons)
        {
            this.classes = classes;
            this.lessons = lessons;
            entity = Entity.Null;
        }

        public void PrintPupils(List<Pupil> list)
        {
            Console.WriteLine("Pupils:\n");
            if (list.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (Pupil p in list)
                {
                    Console.WriteLine(p.ToString());
                    Console.WriteLine("----------------------------------");
                }
            }
        }

        public void PrintTeachers(List<Teacher> list)
        {
            Console.WriteLine("Teachers:\n");
            if (list.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (Teacher s in list)
                {
                    Console.WriteLine(s.ToString());
                    Console.WriteLine("----------------------------------");
                }
            }
        }

        public void PrintJunctions(List<Junction> list)
        {
            Console.WriteLine("Juntions:\n");
            if (list.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (Junction m in list)
                {
                    Console.WriteLine(m.ToString());
                    Console.WriteLine("----------------------------------");
                }
            }
        }


        public void PrintFullTextSearch_FullPhrase(List<SearchRes> res)
        {
            if (res.Count == 0 || res[0] == null)
            {
                Console.WriteLine("No result");
            }
            else
            {
                foreach (SearchRes s in res)
                {
                    Console.WriteLine("Id: " + s.Id + " Attr: " + s.Attr + " ts_headline " + s.Ts_headline);
                }
            }
        }

        public void PrintFullTextSearch_NotIncludedWord(List<SearchRes> res)
        {
            if (res.Count == 0 || res[0] == null)
            {
                Console.WriteLine("No result");
            }
            else
            {
                foreach (SearchRes s in res)
                {
                    Console.WriteLine("Id: " + s.Id + " Attr: " + s.Attr);
                }
            }
        }

        public int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose task:\n1. Entities\n2. Full text search through pupils\n3. Static search through pupils\n0. Exit");
            try
            {
                int key = Convert.ToInt32(Console.ReadLine());
                if (key != 1 && key != 2 && key != 3) return -1;
                return key;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Entity EntitiesMenu()
        {
            Console.Clear();
            Console.Write("ENTITIES\n1. See junctions\n2. Pupil\n3. Teacher\n");
            Console.WriteLine("\n0. Exit");
            Console.WriteLine("Choose entity:");
            try
            {
                int key = Convert.ToInt32(Console.ReadLine());
                switch (key)
                {
                    case 0:
                        entity = Entity.Null;
                        break;
                    case 1:
                        entity = Entity.Junction;
                        break;
                    case 2:
                        entity = Entity.Pupil;
                        break;
                    case 3:
                        entity = Entity.Teacher;
                        break;
                    default: return Entity.Exception;
                }
            }
            catch (Exception)
            {
                entity = Entity.Exception;
            }
            return entity;
        }

        public int OperationsMenu()
        {
            Console.Clear();
            if (entity == Entity.Junction) return -1;
            Console.WriteLine("OPERATIONS:\n1. Get by id\n2. Get all\n3. Add new entity\n4. Update\n5. Delete");
            Console.WriteLine("\n\n0. Exit");
            Console.WriteLine("Choose operation:");
            try
            {
                int key = Convert.ToInt32(Console.ReadLine());
                if (key >= 0 && key < 6) return key;
                return -1;

            }
            catch (Exception)
            {
                return -1;
            }

        }

        public long EnterId()
        {
            Console.WriteLine("Enter id:");
            try
            {
                return Convert.ToInt64(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public Pupil PupilAddOrUpdateEnter()
        {
            Console.WriteLine("Name of pupil:");
            string name = Console.ReadLine();
            Console.WriteLine("Surname of pupil:");
            string surname = Console.ReadLine();
            Console.WriteLine("Class id:");
            long classId = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Birth date:");
            DateTime bday = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Is excellent?");
            bool isEx = Convert.ToBoolean(Console.ReadLine());
            return new Pupil(-1, name, surname, classId, bday, isEx);
        }

        public Teacher TeacherAddOrUpdateEnter()
        {
            Console.WriteLine("Name of teacher:");
            string name = Console.ReadLine();
            Console.WriteLine("Surname of teacher:");
            string surname = Console.ReadLine();
            return new Teacher(-1, name, surname);
        }

        public SearchData StaticSearch()
        {
            Console.WriteLine("Enter min class creation date");
            DateTime min = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter max class creation date");
            DateTime max = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Is student excellent?");
            bool isFree = Convert.ToBoolean(Console.ReadLine());
            return new SearchData(min, max, isFree);
        }

        public int PupilAtr()
        {
            Console.WriteLine("CHOOSE ATRIBUTE");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Surname");
            int key = 0;
            while (key != 1 && key != 2)
            {
                key = Convert.ToInt32(Console.ReadLine());
            }
            return key;

        }

        public int FullText()
        {
            Console.WriteLine("CHOOSE SEARCH");
            Console.WriteLine("1. Full phrase");
            Console.WriteLine("2. Not included word");
            int key = 0;
            while (key != 1 && key != 2)
            {
                key = Convert.ToInt32(Console.ReadLine());
            }
            return key;

        }

        public string SearchQuery()
        {
            Console.WriteLine("Enter query");
            return Console.ReadLine();
        }

        public void Error(string message)
        {
            Console.WriteLine($"Error occured: {message}");
        }

        public void Wait()
        {
            Console.Write("Press any key to get back: ");
            Console.ReadKey();
        }
    }
}
