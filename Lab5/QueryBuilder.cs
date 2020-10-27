﻿//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace Lab5
{
    public class QueryBuilder
    {
        /// <summary>
        /// Parameterized constructor: QueryBuilder
        /// </summary>
        /// <param name="SqliteConnection connection">Connection to the database</param>
        public QueryBuilder(SqliteConnection connection)
        {
            this.SQLiteConnection = connection;                                      //setting connection
            SQLiteConnection.Open();
        }

        /// <summary>
        /// Property : for establiching connection 
        /// </summary>
        public SqliteConnection SQLiteConnection { get; set; }  //Open and Return Connection

        /// <summary>
        /// ReadAll - Read entire of selected table
        /// </summary>
        /// <param name="T"> object T </param>
        /// <returns> Database dictionary </returns>
        public T ReadAll<T>() where T : new()
        {
            List<T> list = new List<T>();
            var t = new T();
            var command = SQLiteConnection.CreateCommand();
            command.CommandText = $"select * from {typeof(T).Name}";            //Select all SQL Command
            SqliteDataReader reader = command.ExecuteReader();                  //Execute reader

            if (reader.HasRows)                                                 //If there are rows in database
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            
            return t;
        }

        /// <summary>
        /// Read - Read single line of selected table
        /// </summary>
        /// <param name="T"> object T </param>
        /// <param name="int id"> selected key </param>
        /// <returns> selectiveQuery dictionary </returns>
        public T Read<T>(int id) where T : new()                   //Reading single line
        {
            List<T> list = new List<T>();
            var t = new T();
            SqliteCommand command = SQLiteConnection.CreateCommand();
            command.CommandText = $"select * from {typeof(T).Name} where Id = {id}";//Select * where the id = key input - SQL Command
            SqliteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)                                                 //If there are rows in database
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            return t;
        }

        /// <summary>
        /// Create - Insert new row
        /// </summary>
        /// <param name="ClassModel update"> Object input </param>
        public void Create(ClassModel update)
        {
            string updateCol = "";
            string updateVal = "";
            PropertyInfo[] properties = update.GetType().GetProperties();
            int index = 0;

            var command = SQLiteConnection.CreateCommand();

            foreach (var item in properties)                                    //seperating the keys and value into their own lists
            {
                if (item.Name.ToString() != "Id" && index == 1)
                {
                    updateCol += " " + item.Name.ToString();
                    updateVal += " '" + item.GetValue(update).ToString() + "'";
                }
                if (item.Name.ToString() != "Id" && index > 1)
                {
                    updateCol += ", " + item.Name.ToString();
                    updateVal += ", '" + item.GetValue(update).ToString() + "'";
                }
                index++;
            }
            command.CommandText = $"INSERT INTO {update.GetType().Name} ( {updateCol} ) " + //insert sql statment with given data
                $"VALUES ( {updateVal} )";
            command.ExecuteNonQuery();                                          //Excute
        }

        /// <summary>
        /// Update - Update selected col/row of selected table
        /// </summary>
        /// <param name="ClassModel update"> Object input </param>
        public void Update(ClassModel update)
        {
            PropertyInfo[] properties = update.GetType().GetProperties();
            string changes = "";
            int Id = 0;

            var command = SQLiteConnection.CreateCommand();

            foreach (var item in properties)
            {
                if (item.Name.ToString() == "Id")
                {
                    Id = (int)item.GetValue(update);
                    changes += " " + item.Name.ToString() + " = '" + item.GetValue(update).ToString() + "'";
                }
                else
                {
                    changes += ", " + item.Name.ToString() + " = '" + item.GetValue(update).ToString() + "'";
                }
            }
            command.CommandText = $"UPDATE {update.GetType().Name} SET {changes} " +         //UPDATE selected table SET to where and what update say where the id = key input - SQL Command
                $"WHERE id = {Id} ";
            command.ExecuteNonQuery();                                          //Execute a non-query
        }

        /// <summary>
        /// Delete - Delete selected row of selected table with selected key
        /// </summary>
        /// <param name="ClassModel update"> Object input </param>
        public void Delete(ClassModel update)
        {
            PropertyInfo[] properties = update.GetType().GetProperties();
            var Id = 0;

            var command = SQLiteConnection.CreateCommand();

            foreach (var item in properties)
            {
                if (item.Name.ToString() == "Id")
                {
                    Id = (int)item.GetValue(update);
                    break;
                }
            }
            command.CommandText = $"delete from {update.GetType().Name} where id = {Id}";  //Delete selected table where the id = key input - SQL Command
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Dispose - Close connection
        /// </summary>
        public void Dispose()
        {
            SQLiteConnection.Close();
        }
    }
}

