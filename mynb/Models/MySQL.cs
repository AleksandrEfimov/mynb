using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Configuration;

namespace mynb.Models
{
     public class MySQL
    {
        private MySqlConnection con;
        public string error { get; private set; }
        public string query { get; private set; }

        public MySQL()
        {   // строка подключения к БД
            // con = new MySqlConnection("server=localhost; UserID=root;Password=mysql;database=STORY;Charset=utf8");
            //создаем подключение
            try
            {
                error = "";
                query = "OPEN CONNECTION TO MySQL";
                con = new MySqlConnection(
                    WebConfigurationManager.ConnectionStrings["conn"].ConnectionString);
                con.Open();
            } catch (Exception ex)
            {
                error = ex.Message;
                con = null;
            }
        }
        ~MySQL()
        {
            try
            {
                con.Close();
            }
            catch
            { }
        }
        public DataTable Select(string myquery)
        {
            if (IsError()) return null;
            try
            {
                query = myquery;
                DataTable table = new DataTable();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
                return table;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public string Scalar(string myquery)
        {
            if (IsError()) return null;
            try
            {
                query = myquery;
                DataTable table = new DataTable();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
                if (table.Rows.Count == 0)
                    return "";
                return table.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public long Insert(string myquery)
        {
            
            try
            {
                query = myquery;
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                
                return cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return -1;
            }
        }

        public bool IsError()
        {
            
            return error != "";
        }

        // защита от sql инъекций
        public string addSlashes(string text)
        {
            return text.Replace("\'", "\\\'");
        }

    }
}