using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Npgsql;

namespace Lab2.Database
{
    class LessonDAO
    {
        private NpgsqlConnection dbconnection;

        public LessonDAO()
        {
            dbconnection = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=Ilyinsky02; Database=School;");
        }

        public void Create(Lesson entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.lessons (name, credits, teacher_id) VALUES (:name, :credits, :teacher_id)";
            command.Parameters.Add(new NpgsqlParameter("name", entity.Name));
            command.Parameters.Add(new NpgsqlParameter("credits", entity.Credits));
            command.Parameters.Add(new NpgsqlParameter("teacher_id", entity.TeacherId));
            Console.WriteLine(command.CommandText);
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException e)
            {
                throw new Exception(e.MessageText);
            }
            dbconnection.Close();
        }

        public List<Lesson> GetList()
        {
            List<Lesson> t_list = new List<Lesson>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "select * from public.lessons ls inner join public.teachers tc on ls.teacher_id = tc.id";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Lesson ls = new Lesson(Convert.ToInt32(reader.GetValue(0)), reader.GetValue(1).ToString(), Convert.ToDouble(reader.GetValue(2)),
                    Convert.ToInt32(reader.GetValue(3)), reader.GetValue(5).ToString(),reader.GetValue(6).ToString());

                t_list.Add(ls);
            }
            dbconnection.Close();
            return t_list;
        }
    }
}
