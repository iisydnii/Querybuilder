using System;
using System.Collections.Generic;

namespace Lab5
{
    public class Course : ClassModel 
    {
        List<Course> list;
        public new long Id { get; set; }
        public long MajorId { get; set; }
        public string CourseNumber { get; set; }
        public string Name { get; set; }
        public int CreditHour { get; set; }

        public Course()
        {
            list = new List<Course>();
        }
        public Course(long Id, long MajorId, string CourseNumber, string Name ,int CreditHour)
        {
            list = new List<Course>();
            this.Id = Id;
            this.MajorId = MajorId;
            this.CourseNumber = CourseNumber;
            this.CreditHour = CreditHour;
            this.Name = Name;
        }
    }
}
