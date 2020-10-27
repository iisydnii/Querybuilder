using System;
using System.Collections.Generic;

namespace Lab5
{
    public class Student : ClassModel
    {
        List<Student> list;
        public new long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ENumber { get; set; }

        public Student() { }
        public Student(long Id, string FirstName, string LastName, string ENumber)
        {
            list = new List<Student>();
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.ENumber = ENumber;
        }
    }
}
