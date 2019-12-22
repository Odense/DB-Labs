using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Model
{
    class Junction
    {
        private long id;
        private long classId;
        private long lessonId;

        public long Id { get => id; set => id = value; }
        public long ClassId { get => classId; set => classId = value; }
        public long LessonId { get => lessonId; set => lessonId = value; }

        public Junction(long id, long classId, long lessonId)
        {
            this.id = id;
            this.classId = classId;
            this.lessonId = lessonId;
        }

        public Junction(long id, long classId, long lessonId,
            string class_name, string specialisation, DateTime cdate,
            string lesson_name, double credits, long teacherId,
            string teacher_name, string teacher_surname)
        {
            this.id = id;
            this.classId = classId;
            this.lessonId = lessonId;
            this.Class = new Class(classId, class_name, specialisation, cdate);
            this.Lesson = new Lesson(lessonId, lesson_name, credits, teacherId, teacher_name, teacher_surname);
        }

        public override string ToString()
        {
            return $"Junction: {this.id}\nClass: {Class.ToString()}\nLesson: {Lesson.ToString()}";
        }

        public Class Class { get; set; }
        public Lesson Lesson { get; set; }
    }
}
