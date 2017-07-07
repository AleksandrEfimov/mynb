using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Configuration;

namespace mynb.Models
{

     /// <summary>
     /// this section is the bridge to the DataBase
     /// 
     /// </summary>
     public class MySQL
    {
        private MySqlConnection con;
        public string error { get; private set; }
        public string query { get; private set; }

        // constructor for class have the conn.string, try/cacth and destructor
        #region
        public MySQL()
        {   // on hosting using next connect string:
            // con = new MySqlConnection("server=localhost; UserID=root;Password=mysql;database=STORY;Charset=utf8");
            

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
                // reset con
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
        #endregion

        // create constructor for queries
        // it`s compleate query to DB and save result in object "table".
        #region
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
        #endregion

        // Get status for checking user status
        #region
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
        #endregion

        // section bellow describe Insert and Update methods editing raw in DB
        #region
        public long Insert(string myquery)
        {
            try
            {
                query = myquery;
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();     
                // return story id
                return cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return -1;
            }
        }

        public long Update(string myquery)
        {
            try
            {
                query = myquery;
                MySqlCommand cmd = new MySqlCommand(query, con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return -1;
            }
        }
        #endregion

        public bool IsError()
        {   
            return error != "";
        }

        // this method protects against SQL-injection 
        public string addSlashes(string text)
        {
            return text.Replace("\'", "\\\'");
        }

    }
}