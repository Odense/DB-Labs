using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Pupil
    {
        private long id;
        private string name;
        private string surname;
        private DateTime birth_date;
        private bool excellent_pupil;
        private long class_id;

        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public DateTime BirthDate { get => birth_date; set => birth_date = value; }
        public bool ExcellentPupil { get => excellent_pupil; set => excellent_pupil = value; }
        public long ClassId { get => class_id; set => class_id = value; }

        public Class Class { get; set; }

        public Pupil(long id, string name, string surname, long class_id, DateTime birth_date, bool excellent_pupil)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.class_id = class_id;
            this.birth_date = birth_date;
            this.excellent_pupil = excellent_pupil;
        }

        public Pupil(long id, string name, string surname, long class_id, DateTime birth_date, bool excellent_pupil, string class_name, string specialisation, DateTime creationDate)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.class_id = class_id;
            this.birth_date = birth_date;
            this.excellent_pupil = excellent_pupil;
            this.Class = new Class(class_id, class_name, specialisation, creationDate);
        }

        public override string ToString()
        {
            return $"Pupil ID: {this.id}\nName: {this.name}\nSurname: {this.surname}\nClass: {Class.ToString()}";
        }
    }
}
