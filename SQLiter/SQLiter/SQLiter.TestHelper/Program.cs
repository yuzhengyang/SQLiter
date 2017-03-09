using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiter.TestHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + "test.db";
            string createTable = "create table if not exists student(id text primary key, name text, sex integer, birthday text)";
            string queryStudent = "select * from student;";
            SQLiteParameter[] queryStudentParameters = new SQLiteParameter[]{
                new SQLiteParameter("@id","1001"),
            };

            //SQLiteHelper.CreateDB(dbPath);
            SQLiteHelper sh = new SQLiteHelper(dbPath);
            int row = sh.ExecuteNonQuery(createTable, null);
            SQLiteDataReader reader = sh.ExecuteReader(queryStudent, null);
            while (reader.Read())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(reader.GetValue(0) + " - ");
                sb.Append(reader.GetValue(1) + " - ");
                sb.Append(reader.GetInt32(2) + " - ");
                sb.Append(reader.GetValue(3));
                Console.WriteLine(sb);
            }
            Console.ReadLine();
        }
    }
}
