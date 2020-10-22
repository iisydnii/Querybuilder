using System;
namespace Lab5
{
    public class Course : ClassModel 
    {
        public Course()
        {
        }
        public new int Id { get; set; }
        public int MajorId { get; set; }
        public int CourseNumber { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
    }
}
