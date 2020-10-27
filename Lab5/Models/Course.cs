using System;
using System.Collections.Generic;

namespace Lab5
{
    public class Course : ClassModel 
    {
        List<Course> list;
        public new long Id { get; set; }
        public Major MajorId { get; set; }
        public string CourseNumber { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }

        public Course() { }
        public Course(long Id, Major MajorId, string CourseNumber, int CreditHours)
        {
            list = new List<Course>();
            this.Id = Id;
            this.MajorId = MajorId;
            this.CourseNumber = CourseNumber;
            this.CreditHours = CreditHours;
        }
    }
}
