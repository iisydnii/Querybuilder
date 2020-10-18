using System;
using System.Collections.Generic;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dictionary<String, List<String>>> singleRow = new List<Dictionary<String, List<String>>>();
            List<string> list = new List<string>();
            QueryBuilder queryBuilder = new QueryBuilder();
            queryBuilder.ReadAll("Student");
            list.Add("Sydni");
            list.Add("Ward");
            singleRow.Add(new Dictionary<string, List<string>>() { { "1", list } });
            queryBuilder.Create("Student", singleRow);


        }
    }
}
