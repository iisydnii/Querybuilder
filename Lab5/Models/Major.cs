using System;
using System.Collections.Generic;

namespace Lab5
{
    public class Major : ClassModel
    {
        List<Major> list;
        public new long Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }

        public Major()
        { }
        public Major(long Id, string Abbreviation, string Name)
        {
            list = new List<Major>();
            this.Id = Id;
            this.Abbreviation = Abbreviation;
            this.Name = Name;
        }   
    }
}
