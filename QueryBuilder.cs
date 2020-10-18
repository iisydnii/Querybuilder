using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Lab5
{
    public class QueryBuilder
    {
        List<Dictionary<String, List<String>>> database;

        public QueryBuilder()
        {
            database = new List<Dictionary<String, List<String>>>();
            SQLiteConnection = @"Data Source= myDatabase.db";
        }

        public string SQLiteConnection { get; set;}

        public void ReadAll(string tableName)
        {
            using (var connection = new SqliteConnection(SQLiteConnection))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from " + tableName;
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        List<string> list = new List<string>();
                        list.Add(reader[index + 1].ToString());
                        list.Add(reader[index + 2].ToString());

                        database.Add(new Dictionary<String, List<String>>()
                        {{reader[index].ToString() , list}});
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
        }

        public void Read()
        {
        }
        public void Create()
        {
        }
        public void Update()
        {
        }
        public void Delete()
        {
        }


    }
}

