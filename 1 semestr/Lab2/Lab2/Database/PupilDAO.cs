using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2.Model;
using Npgsql;

namespace Lab2.Database
{
    class PupilDAO : DAO<Pupil>
    {
        public struct SearchData
        {
            public DateTime creationDate1;
            public DateTime creationDate2;
            public bool isExcellentPupil;
            public SearchData(DateTime creationDate1, DateTime creationDate2, bool isExcellentPupil)
            {
                this.creationDate1 = creationDate1;
                this.creationDate2 = creationDate2;
                this.isExcellentPupil = isExcellentPupil;
            }
        }

        public PupilDAO() : base() { }

        public override void Create(Pupil entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "INSERT INTO public.pupils (name, surname, class_id, birth_date, excellent_pupil)" +
                " VALUES (:name, :surname, :class_id, :birth_date, :excellent_pupil)";
            command.Parameters.Add(new NpgsqlParameter("name", entity.Name));
            command.Parameters.Add(new NpgsqlParameter("surname", entity.Surname));
            command.Parameters.Add(new NpgsqlParameter("class_id", entity.ClassId));
            command.Parameters.Add(new NpgsqlParameter("birth_date", entity.BirthDate));
            command.Parameters.Add(new NpgsqlParameter("excellent_pupil", entity.ExcellentPupil));
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
            command.CommandText = "DELETE FROM public.pupils WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            command.ExecuteNonQuery();
            dbconnection.Close();
        }

        public override Pupil Get(long id)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.pupils p INNER JOIN public.classes cl ON p.class_id = cl.id" +
                " WHERE p.id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", id));
            NpgsqlDataReader reader = command.ExecuteReader();
            Pupil s = null;
            while (reader.Read())
            {
                s = new Pupil(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToInt64(reader.GetValue(3)),
                                  Convert.ToDateTime(reader.GetValue(4)), Convert.ToBoolean(reader.GetValue(5)),
                                  reader.GetValue(7).ToString(), reader.GetValue(8).ToString(), 
                                  Convert.ToDateTime(reader.GetValue(9)));
            }
            dbconnection.Close();
            return s;
        }

        public override List<Pupil> GetList()
        {
            List<Pupil> pupils = new List<Pupil>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.pupils p INNER JOIN public.classes cl ON p.class_id = cl.id";
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Pupil s = new Pupil(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToInt64(reader.GetValue(3)),
                                  Convert.ToDateTime(reader.GetValue(4)), Convert.ToBoolean(reader.GetValue(5)),
                                  reader.GetValue(7).ToString(), reader.GetValue(8).ToString(), 
                                  Convert.ToDateTime(reader.GetValue(9)));
                pupils.Add(s);
            }
            dbconnection.Close();
            return pupils;
        }

        public override void Update(Pupil entity)
        {
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "UPDATE public.pupils SET name = :name, surname = :surname, " +
                "class_id = :class_id, birth_date = :birth_date, excellent_pupil = :excellent_pupil WHERE id = :id";
            command.Parameters.Add(new NpgsqlParameter("id", entity.Id));
            command.Parameters.Add(new NpgsqlParameter("name", entity.Name));
            command.Parameters.Add(new NpgsqlParameter("surname", entity.Surname));
            command.Parameters.Add(new NpgsqlParameter("class_id", entity.ClassId));
            command.Parameters.Add(new NpgsqlParameter("birth_date", entity.BirthDate));
            command.Parameters.Add(new NpgsqlParameter("excellent_pupil", entity.ExcellentPupil));
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
            }
            catch (PostgresException)
            {
                throw new Exception("Unable to update pupil");
            }
            dbconnection.Close();
        }

        public List<Pupil> StaticSearch(SearchData search)
        {
            if (search.creationDate1 > search.creationDate2) throw new Exception("Wrong creating date diapason");
            List<Pupil> pupils = new List<Pupil>();
            dbconnection.Open();
            NpgsqlCommand command = dbconnection.CreateCommand();
            command.CommandText = "SELECT * FROM public.pupils p INNER JOIN public.classes cl ON p.class_id = cl.id " +
                    "where p.excellent_pupil = :excellent_pupil and cl.creation_date >= :cd1 and cl.creation_date <= :cd2";
            command.Parameters.Add(new NpgsqlParameter("cd1", search.creationDate1));
            command.Parameters.Add(new NpgsqlParameter("cd2", search.creationDate2));
            command.Parameters.Add(new NpgsqlParameter("excellent_pupil", search.isExcellentPupil));
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Pupil s = new Pupil(Convert.ToInt64(reader.GetValue(0)), reader.GetValue(1).ToString(),
                                  reader.GetValue(2).ToString(), Convert.ToInt64(reader.GetValue(3)),
                                  Convert.ToDateTime(reader.GetValue(4)), Convert.ToBoolean(reader.GetValue(5)),
                                  reader.GetValue(7).ToString(), reader.GetValue(8).ToString(),
                                  Convert.ToDateTime(reader.GetValue(9)));
                pupils.Add(s);
            }
            dbconnection.Close();
            return pupils;
        }
    }
}
