using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Teacher
    {
        private long id;
        private string name;
        private string surname;

        public Teacher(long id, string name, string surname)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
        }

        public override string ToString()
        {
            return $"Teacher ID: {this.id}   Name: {this.name}   Surname: {this.surname}";
        }

        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
    }
}