using System;
namespace Lab5
{
    public class Student : ClassModel
    {
        public Student()
        { 
        }
        public new int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ENumber { get; set; }
    }
}
