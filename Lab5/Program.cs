//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Project: Lab 5
// File Name:   Program.cs
// Description: Driver 
// Course: CSCI-2910-940 - Server Side Web Prog
// Author: Sydni Ward
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> create;
            create = new Dictionary<string, string>();

            string dbSource = Directory.GetParent                               //dbSource is for getting the root directory 
                (Directory.GetCurrentDirectory()).Parent.Parent.ToString();
           
            using (var connection = new SqliteConnection                        //estasblishing a connection to the database
                ($"Data Source ={dbSource}/Database/myDatabase.db"))
            {
                QueryBuilder queryBuilder = new QueryBuilder(connection);
                queryBuilder.ReadAll<Student>();
                //Creating a instance of QueryBuilder class
                //queryBuilder.Read("Student", "1");                  //Reading single line of Student table 
                //queryBuilder.Read(connection, "Course", "1");                   //Reading single line of Course table
                //create.Add("Abbreviation", "ACCT");
                //create.Add("Name", "Accounting");
                //queryBuilder.Create("Major", create);
                //create.Clear();
                //create.Add("Abbreviation", "MATH");
                //create.Add("Name", "Math");
                //queryBuilder.Create("Major", create);
                //create.Clear();
                //create.Add("MajorId", "2");
                //create.Add("CourseNumber", "101");
                //create.Add("Name", "Accounting");
                //create.Add("CreditHour", "3");
                //queryBuilder.Create("Course", create);
                //create.Clear();
                //create.Add("FirstName", "Edward");
                //create.Add("LastName", "Hall");
                //queryBuilder.Create("Student", create);
                //create.Clear();
                //create.Add("FirstName", "Reagan");
                //create.Add("LastName", "Mullins");
                //queryBuilder.Create("Student", create);
                //create.Clear();
                //create.Add("FirstName", "Nick");
                //create.Add("LastName", "Sels");
                //queryBuilder.Create("Student", create);
                //create.Clear();
                //queryBuilder.Update(student = new Student(2, "Sydni", "Ward", "E00594751"));
                //queryBuilder.Create(student = new Student(0, "Alex", "Ramsey", "E00593569"));
                //queryBuilder.Delete(student = new Student(6, "Edward", "Hall", ""));
                //queryBuilder.Update("Student", 8,
                //    "LastName = 'Sells'");
                //queryBuilder.Delete("Student", 1);
            }

        }
    }
}
