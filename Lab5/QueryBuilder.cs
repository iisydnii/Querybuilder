//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Project: Lab 5
// File Name: QueryBuilder
// Description: Class for SQL Command builds and executions
// Course: CSCI-2910-940 - Server Side Web Prog
// Author: Sydni Ward
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Lab5
{
    public class QueryBuilder
    {
        
        List<string> list = new List<string>();
        List<string> list2 = new List<string>();
        List<Dictionary<int, List<String>>> database;
        List<Dictionary<int, List<String>>> selectiveQuery;

        /// <summary>
        /// Parameterized constructor: QueryBuilder
        /// </summary>
        /// <param name="SqliteConnection connection">Connection to the database</param>
        public QueryBuilder(SqliteConnection connection)
        {
            database = new List<Dictionary<int, List<String>>>();
            selectiveQuery = new List<Dictionary<int, List<string>>>();
            this.SQLiteConnection = connection;                                      //setting connection
        }

        /// <summary>
        /// Property : for establiching connection 
        /// </summary>
        public SqliteConnection SQLiteConnection { get; set; }  //Open and Return Connection


        /// <summary>
        /// ReadAll - Read entire of selected table
        /// </summary>
        /// <param name="string tableName">selected table</param>
        /// <returns> Database dictionary </returns>
        public List<Dictionary<int, List<String>>> ReadAll(string tableName)
        {
            SQLiteConnection.Open();
            database.Clear();                                                   //Empty the dictionary 
            var command = SQLiteConnection.CreateCommand();
            command.CommandText = $"select * from {tableName}";                 //Select all SQL Command
            SqliteDataReader reader = command.ExecuteReader();                  //Execute reader

            if (reader.HasRows)                                                 //If there are rows in database
            {
                while (reader.Read())
                {

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        list.Add(reader.GetName(i) + ": " + reader.GetValue(i)); //GetName of colmun and that value of that row 
                        database.Add(new Dictionary<int, List<String>>()         //Add it to a list then add that list to a dictionary key
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

        /// <summary>
        /// Read - Read single line of selected table
        /// </summary>
        /// <param name="string tableName"> selected table </param>
        /// <param name="string key"> selected key </param>
        /// <returns> selectiveQuery dictionary </returns>
        public List<Dictionary<int, List<String>>> Read                         //Reading single line
            (string tableName, string id)
        {
            SQLiteConnection.Open();
            SqliteCommand command = SQLiteConnection.CreateCommand();
            command.CommandText = $"select * from {tableName} where id = {id}";//Select * where the id = key input - SQL Command
            SqliteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)                                                 //If there are rows in database
            {
                int index = 0;
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        list.Add(reader.GetName(i).ToString() + ": " +          //GetName of colmun and that value of that row 
                            reader.GetValue(i).ToString());
                    }
                    selectiveQuery.Add(new Dictionary<int, List<String>>()      //Add it to a list then add that list to a dictionary key
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

        /// <summary>
        /// Create - Insert new row
        /// </summary>
        /// <param name="string tableName"> selected table </param>
        /// <param name="Dictionary<string, string> create"> selected insert values</param>
        public void Create(string tableName, Dictionary<string, string> create)
        {
            SQLiteConnection.Open();

            list.Clear();
            list2.Clear();
            string updateCol = "";
            string updateVal = "";
            
            var command = SQLiteConnection.CreateCommand();
            foreach (var item in create)                                        //seperating the keys and value into their own lists
            {
                list.Add(item.Key);
                list2.Add(item.Value);
            }
            
            for (int i = 0; i < list.Count; i++)                                // turn the key lists into one single string 
            {
                if (i == 0)
                {
                    updateCol = " " + list[i].ToString();
                }
                else
                {
                    updateCol += ", " + list[i].ToString();
                }
            }
            for (int i = 0; i < list2.Count; i++)                               // turn the values lists into one single string 
            {
                if (i == 0)
                {
                    updateVal = "'" + list2[i].ToString() + "'";
                }
                else
                {
                    updateVal += ", '" + list2[i].ToString() + "'";
                }            }
            command.CommandText = $"INSERT INTO {tableName} ( {updateCol} ) " + //insert sql statment with given data
                $"VALUES ( {updateVal} )";
            command.ExecuteNonQuery();                                          //Excute

            ReadAll(tableName);
        }

        /// <summary>
        /// Update - Update selected col/row of selected table
        /// </summary>
        /// <param name="string tableName"> selected table </param>
        /// <param name="string key"> selected key </param>
        /// <param name="string update"> selected col/row update</param>
        public void Update(string tableName, int id, string update)
        {
            SQLiteConnection.Open();
            ReadAll(tableName);
            var command = SQLiteConnection.CreateCommand();

            command.CommandText = $"UPDATE {tableName} SET {update} " +         //UPDATE selected table SET to where and what update say where the id = key input - SQL Command
                $"WHERE id = {id} ";
            command.ExecuteNonQuery();                                          //Execute a non-query
            ReadAll(tableName);
        }

        /// <summary>
        /// Delete - Delete selected row of selected table with selected key
        /// </summary>
        /// <param name="string tableName"> selected table </param>
        /// <param name="string key"> selected key </param>
        public void Delete(string tableName,int id)
        {
            SQLiteConnection.Open();
            ReadAll(tableName);
            var command = SQLiteConnection.CreateCommand();
            command.CommandText = $"delete from {tableName} where id = {id}";  //Delete selected table where the id = key input - SQL Command
            command.ExecuteNonQuery();                                          ////Execute a non-query
            ReadAll(tableName);
        }

    }
}

