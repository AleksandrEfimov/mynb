using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Configuration;

namespace mynb.Models
{
    static public class MySQL
    {
        static private MySqlConnection con;

        static MySQL()
        {   // строка подключения к БД
            // con = new MySqlConnection("server=localhost; UserID=root;Password=mysql;database=STORY;Charset=utf8");
            //создаем подключение
            con = new MySqlConnection(
                WebConfigurationManager.ConnectionStrings["conn"].ConnectionString);
            con.Open();
        }
        static public DataTable Select(string myquery)
        {
            DataTable table = new DataTable();
            MySqlCommand cmd = new MySqlCommand(myquery, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            table.Load(reader);
            return table;
        }

    }
}