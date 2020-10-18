using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace Lab5
{
    public class QueryBuilder
    {
        List<Dictionary<String, List<String>>> database;
        List<Dictionary<String, List<String>>> singleRow;
        string key;
        int row;


        public QueryBuilder()
        {
            database = new List<Dictionary<String, List<String>>>();
            singleRow = new List<Dictionary<String, List<String>>>();
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

        public List<Dictionary<String, List<String>>> Read(string tableName, string primaryKey)
        {
            using (var connection = new SqliteConnection(SQLiteConnection))
            {
                connection.Open();
                GetKey(tableName);
                var command = connection.CreateCommand();
                command.CommandText = "select * from " + tableName +
                        "where " + key + " = " + primaryKey;
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
                        singleRow.Add(new Dictionary<String, List<String>>()
                        {{reader[index].ToString() , list}});
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                return singleRow;
            }
        }

        public void Create(string tableName, List<Dictionary<String, List<String>>> create)
        {
            using (var connection = new SqliteConnection(SQLiteConnection))
            {
                connection.Open();
                GetKey(tableName);
                var command = connection.CreateCommand();
                // PROBLEM With the AUTO-INCREMENTING ID
                //command.CommandText = "select count(Id) from " + tableName;
                //SqliteDataReader reader = command.ExecuteReader();

                //if (reader.HasRows)
                //{
                //    int index = 0;
                //    while (reader.Read())
                //    {
                //        key = reader[index].ToString();
                //        row = Int32.Parse(key);
                //    }
                //}
                //// PROBLEM With the AUTO-INCREMENTING ID
                switch (tableName)
                {
                    case "Course":
                        command.CommandText = "INSERT INTO " + tableName +
                        " (Id, MajorId, CourseNumber, Name, CreditHour) VALUES ("
                        + row.ToString() + create[1] + create[2] + create[3] + create[4]
                         + ")";
                        command.Parameters.AddWithValue("Id", row.ToString());
                        command.Parameters.AddWithValue("MajorId", create[1]);
                        command.Parameters.AddWithValue("CourseNumber", create[2]);
                        command.Parameters.AddWithValue("Name", create[3]);
                        command.Parameters.AddWithValue("CreditHour", create[4]);
                        break;
                    case "Major":
                        command.CommandText = "INSERT INTO " + tableName +
                    " (Id, Abbreviation, Name) VALUES (" + row.ToString()
                    + create[1] + create[2] + ")";
                        command.Parameters.AddWithValue("Id", row.ToString());
                        command.Parameters.AddWithValue("Abbreviation", create[1]);
                        command.Parameters.AddWithValue("Name", create[2]);
                        break;
                    case "sqlite_sequence":
                        command.CommandText = "INSERT INTO " + tableName +
                    " (name, seq) VALUES (" + create[0] + create[1]
                     + ")";
                        command.Parameters.AddWithValue("name", create[0]);
                        command.Parameters.AddWithValue("seg", create[1]);
                        break;
                    case "Student":
                        command.CommandText = "INSERT INTO " + tableName +
                    " (Id, FirstName, LastName) VALUES (" + row.ToString()
                    + create[1] + create[2] + ")";
                        command.Parameters.AddWithValue("Id", row.ToString());
                        command.Parameters.AddWithValue("FirstName", create[1]);
                        command.Parameters.AddWithValue("LastName", create[2]);
                        break;
                    default:
                        Console.WriteLine("Can't find table Error!");
                        break;
                }


            }
        }

        public void Update()
        {
        }
        public void Delete()
        {
        }
        private string GetKey(String tableName)
        {
            switch (tableName)
            {
                case "Course":
                    key = "Id";
                    break;
                case "Major":
                    key = "Id";
                    break;
                case "sqlite_sequence":
                    key = "name";
                    break;
                case "Student":
                    key = "Id";
                    break;
                default:
                    key = "";
                    Console.WriteLine("Can't find table Error!");
                    break;
            }

            return key;

        }
    }
}

