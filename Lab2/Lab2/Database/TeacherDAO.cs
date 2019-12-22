using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Npgsql;

namespace Lab2.Database
{
    class TeacherDAO : DAO<Teacher>
    {
        public TeacherDAO() : base() { }

        public override void Create(Teacher entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.teachers (name, surname) VALUES (:name, :surname)";
            command.Parameters.Add(new NpgsqlParameter("name", entity.Name));
            command.Parameters.Add(new NpgsqlParameter("surname", entity.Surname));
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

        public override void Delete(long id)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "DELETE FROM public.teachers WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Teacher Get(long id)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.teachers WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Teacher t = null;
            while (reader.Read())
            {
                t = new Teacher(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString());
            }
            dbconnection.Close();
            return t;
        }

        public override List<Teacher> GetList()
        {
            List<Teacher> teachers = new List<Teacher>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.teachers";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Teacher t = new Teacher(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString());
                teachers.Add(t);
            }
            dbconnection.Close();
            return teachers;
        }

        /*public override List<Teacher> GetList(int page)
        {
            List<Teacher> movies = new List<Teacher>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.movie LIMIT 10 OFFSET :offset";
            command.Parameters.Add(new NpgsqlParameter("offset", page * 10));
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Teacher mov = new Teacher(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                    reader.GetValue(2).ToString(), Convert.ToDateTime(reader.GetValue(3)),
                    Convert.ToInt16(reader.GetValue(4)));
                movies.Add(mov);
            }
            dbconnection.Close();
            return movies;
        }*/

        public override void Update(Teacher entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "UPDATE public.teachers SET name = :name, surname = :surname WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("name", entity.Name));
            command.Parameters.Add(new NpgsqlParameter("surname", entity.Surname));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to update teacher");
            }
            dbconnection.Close();
        }
    }
}
