using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Lesson
    {
        private long id;
        private string name;
        private double credits;
        private long teacher_id;

        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Credits { get => credits; set => credits = value; }
        public long TeacherId { get => teacher_id; set => teacher_id = value; }

        public Teacher Teacher { get; set; }

        public Lesson(long id, string name, double credits, long teacher_id)
        {
            this.id = id;
            this.name = name;
            this.credits = credits;
            this.teacher_id = teacher_id;
        }

        public Lesson(long id, string name, double credits, long teacher_id, string teacher_name,
            string teacher_surname)
        {
            this.id = id;
            this.name = name;
            this.credits = credits;
            this.teacher_id = teacher_id;
            this.Teacher = new Teacher(teacher_id, teacher_name, teacher_surname);
        }

        public override string ToString()
        {
            return $"Id: {this.id}   Name of lesson: {this.name}\nTeacher: {Teacher.ToString()}";
        }
    }
}
