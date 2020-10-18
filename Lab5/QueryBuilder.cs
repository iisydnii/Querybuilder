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

        public List<Dictionary<String, List<String>>> ReadAll(string tableName)
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
                        switch (tableName)
                        {
                            case "Course":
                                list.Add(reader["MajorId"].ToString());
                                list.Add(reader["CourseNumber"].ToString());
                                list.Add(reader["Name"].ToString());
                                list.Add(reader["CreditHour"].ToString());
                                break;
                            case "Major":
                                list.Add(reader["Abbreviation"].ToString());
                                list.Add(reader["Name"].ToString());
                                break;
                            case "sqlite_sequence":
                                list.Add(reader["seq"].ToString());
                                break;
                            case "Student":
                                list.Add(reader["FirstName"].ToString());
                                list.Add(reader["LastName"].ToString());
                                break;
                            default:
                                Console.WriteLine("Error!");
                                break;
                        }

                        database.Add(new Dictionary<String, List<String>>()
                        {{reader[index].ToString() , list}});
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                return database;
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

