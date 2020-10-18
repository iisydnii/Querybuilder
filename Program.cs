using System;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryBuilder queryBuilder = new QueryBuilder();
            queryBuilder.ReadAll("Student");
        }
    }
}
