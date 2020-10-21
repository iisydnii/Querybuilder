using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace Lab5
{
    public class QueryBuilder
    {
        
        List<string> list = new List<string>();
        List<Dictionary<int, List<String>>> database;
        List<Dictionary<int, List<String>>> selectiveQuery;
        

        public QueryBuilder()
        {
            database = new List<Dictionary<int, List<String>>>();
            selectiveQuery = new List<Dictionary<int, List<String>>>();
            this.SQLiteConnection = SQLiteConnection;
        }

        public string SQLiteConnection { get; set;}

        public List<Dictionary<int, List<String>>> ReadAll(SqliteConnection connection, string tableName)
        {
            database.Clear();
            var command = connection.CreateCommand();
            command.CommandText = $"select * from {tableName}";
            SqliteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                        
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        list.Add(reader.GetName(i) + ": " + reader.GetValue(i));
                        database.Add(new Dictionary<int, List<String>>()
                        {{ i , list}});
                    }
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            
            return database;
        }

        public List<Dictionary<int, List<String>>> Read(SqliteConnection connection, string tableName, string key)
        {
            var command = connection.CreateCommand();
            command.CommandText = $"select count(*) from {tableName} where id = {key}";
            var totalRow = command.ExecuteScalar();
            int.TryParse(totalRow.ToString(), out int j);
            command.CommandText = $"select * from {tableName} where id = {key}";
            SqliteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                int index = 0;
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        list.Add(reader.GetName(i).ToString() + ": " + reader.GetValue(i).ToString());
                    }
                    selectiveQuery.Add(new Dictionary<int, List<String>>()
                            {{ index , list}});
                    index = index + 1;
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            return selectiveQuery;
        }

        //public void Create(SqliteConnection connection, string tableName, Dictionary<List<string>, List<string>> create)
        //{
        //    ReadAll(connection, tableName);
        //    var command = connection.CreateCommand();
        //    for (int i = 0; i < create.Count())

        //    command.CommandText = $"INSERT INTO {tableName} ( {create.Keys} ) VALUES ( {create.TryGetValue(create.Keys)} )";
        //    command.ExecuteNonQuery();
        //    ReadAll(connection, tableName);
        //}

        public void Update(SqliteConnection connection, string tableName, int key , string update)
        {
            ReadAll(connection, tableName);
            var command = connection.CreateCommand();

            command.CommandText = $"UPDATE {tableName} SET {update} WHERE id = {key} ";
            command.ExecuteNonQuery();
            ReadAll(connection, tableName);
        }

        public void Delete(SqliteConnection connection, string tableName, int key)
        {
            ReadAll(connection, tableName);
            var command = connection.CreateCommand();
            command.CommandText = $"delete from {tableName} where id = {key}";
            command.ExecuteNonQuery();
            ReadAll(connection, tableName);
        }

    }
}

