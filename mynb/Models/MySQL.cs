﻿using System;
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
        static public string error { get; private set; }
        static public string query { get; private set; }

        static MySQL()
        {   // строка подключения к БД
            // con = new MySqlConnection("server=localhost; UserID=root;Password=mysql;database=STORY;Charset=utf8");
            //создаем подключение
            try
            {
                error = "";
                query = 
                con = new MySqlConnection(
                    WebConfigurationManager.ConnectionStrings["conn"].ConnectionString);
                con.Open();
            } catch (Exception ex)
            {
                error = ex.Message;
                query = "OPEN CONNECTION TO MySQL";
                con = null;
            }

            
        }
        static public DataTable Select(string myquery)
        {
            if (IsError()) return null;
            try
            {
                DataTable table = new DataTable();
                MySqlCommand cmd = new MySqlCommand(myquery, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
                return table;
            }
            catch
            { }
        }
        static public bool IsError()
        {
            
            return error != "";
        }


    }
}