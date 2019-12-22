using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Lab2.Model;

namespace Lab2.Database
{
    abstract class DAO<T>
    {
        protected NpgsqlConnection dbconnection;

        public DAO()
        {
            dbconnection = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=Ilyinsky02; Database=School;");
        }

        public abstract T Get(long id);
        public abstract List<T> GetList();
        //public abstract List<T> GetList(int page);
        public abstract void Create(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(long id);

        public static void RandomDB(ClassDAO classDAO, TeacherDAO teacherDAO, LessonDAO lessonDAO, PupilDAO pupilDAO, JunctionDAO junctionDAO)
        {
            RandomGen random = new RandomGen();

            string[] lessonNames = random.getLessons();
            for (int i = 0; i < lessonNames.Length; i++)
            {
                Teacher t = new Teacher(-1, random.getRandomName(), random.getRandomSurname());
                teacherDAO.Create(t);
            }

            List<Teacher> teachers = teacherDAO.GetList();
            
            for (int i = 0; i < teachers.Count; i++)
            {
                Lesson l = new Lesson(-1, lessonNames[i], random.getRandomDouble(10), teachers.ElementAt(i).Id);
                lessonDAO.Create(l);
            }
            List<Lesson> lessons = lessonDAO.GetList();
            List<long> lessonIndexes = new List<long>();

            foreach (Lesson t in lessons)
            {
                lessonIndexes.Add(t.Id);
            }

            string[] classNames = random.getClassnames();
            for (int i = 0; i < classNames.Length; i++)
            {
                Class c = new Class(-1, classNames[i], random.getRandomSpec(), random.getRandomCreationDate());
                classDAO.Create(c);
            }
            List<Class> classes = classDAO.GetList();
            foreach (Class c in classes)
            {
                int pupilAmount = random.getRandomInt(20, 31);
                for (int i = 0; i < pupilAmount; i++)
                {
                    Pupil p = new Pupil(-1, random.getRandomName(), random.getRandomSurname(), c.Id,
                        random.getRandomBirthDate(), random.getRandomBoolean());
                    pupilDAO.Create(p);
                }

                for (int i = 0; i < 7; i++)
                {
                    Junction j = new Junction(-1, c.Id, random.getRandomIndex(lessonIndexes));
                    junctionDAO.Create(j);
                }
            }
        }
    }
}
