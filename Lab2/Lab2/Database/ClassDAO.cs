using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Npgsql;

namespace Lab2.Database
{
    class ClassDAO
    {
        private NpgsqlConnection dbconnection;

        public ClassDAO()
        {
            dbconnection = new NpgsqlConnection("Server=127.0.0.1; Port=5432; User Id=postgres; Password=Ilyinsky02; Database=School;");
        }

        public List<Class> GetList()
        {
            List<Class> classes = new List<Class>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.classes";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Class c = new Class(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), Convert.ToDateTime(reader.GetValue(3)));
                classes.Add(c);
            }
            dbconnection.Close();
            return classes;
        }

        public void Create(Class entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.classes (name, specialisation, creation_date) VALUES (:name, :specialisation, :creation_date)";
            command.Parameters.Add(new NpgsqlParameter("name", entity.Name));
            command.Parameters.Add(new NpgsqlParameter("specialisation", entity.Specialisation));
            command.Parameters.Add(new NpgsqlParameter("creation_date", entity.CreationDate));
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
