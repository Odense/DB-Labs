using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Class
    {
        private long id;
        private string name;
        private string specialisation;
        private DateTime creationDate;

        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Specialisation { get => specialisation; set => specialisation = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }

        public Class(long id, string name, string specialisation, DateTime creationDate)
        {
            this.id = id;
            this.name = name;
            this.specialisation = specialisation;
            this.creationDate = creationDate;
        }

        public override string ToString()
        {
            return $"Class ID: {this.id}   Name: {this.name}   Specialisation: {this.specialisation}";
        }
    }
}
