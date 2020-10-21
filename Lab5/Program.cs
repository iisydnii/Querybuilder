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
            List<string> listforKeys = new List<string>();
            List<string> listforValues = new List<string>();
            List<string> update = new List<string>();
            Dictionary<List<string>, List<string>> create = new Dictionary<List<string>, List<string>>();
            string dbSource = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            Console.WriteLine(dbSource);
            
            
            QueryBuilder queryBuilder = new QueryBuilder();
            //listforKeys.Add("Abbreviation");
            //listforKeys.Add("Name");

            //listforValues.Add("CSCI");
            //listforValues.Add("Computer Science");

            //create.Add(listforKeys, listforValues);
            using (var connection = new SqliteConnection($"Data Source ={dbSource}/Database/myDatabase.db"))
            {
                connection.Open();
                queryBuilder.Read(connection, "Student", "1");
                queryBuilder.Read(connection, "Course", "1");
                //queryBuilder.Create(connection, "Major", create);
                queryBuilder.Update(connection, "Student", 2, "FirstName = 'Sydni'");
                queryBuilder.Update(connection, "Student", 2, "LastName = 'Ward'");
                queryBuilder.Delete(connection, "Student", 5);
            }

        }
    }
}
