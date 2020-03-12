using Lab2.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Database
{
    class JunctionDAO
    {
        private NpgsqlConnection dbconnection;

        public JunctionDAO()
        {
            dbconnection = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=Ilyinsky02; Database=School;");
        }

        public List<Junction> GetList()
        {
            List<Junction> js = new List<Junction>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.junctions j inner join public.classes cl on j.class_id = cl.id" +
                " inner join public.lessons l on j.lesson_id = l.id inner join public.teachers t on l.teacher_id = t.id";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Junction j = new Junction(Convert.ToInt64(reader.GetValue(0)), Convert.ToInt64(reader.GetValue(1)), 
                    Convert.ToInt64(reader.GetValue(2)), reader.GetValue(4).ToString(), reader.GetValue(5).ToString(),
                    Convert.ToDateTime(reader.GetValue(6)), reader.GetValue(8).ToString(), Convert.ToDouble(reader.GetValue(9)),
                    Convert.ToInt64(reader.GetValue(10)), reader.GetValue(12).ToString(), reader.GetValue(13).ToString());
                js.Add(j);
            }
            dbconnection.Close();
            return js;
        }

        public void Create(Junction entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.junctions (class_id, lesson_id) VALUES (:class_id, :lesson_id)";
            command.Parameters.Add(new NpgsqlParameter("class_id", entity.ClassId));
            command.Parameters.Add(new NpgsqlParameter("lesson_id", entity.LessonId));
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
    }
}
