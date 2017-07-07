using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Web.Mvc;
using System.Collections.Generic;
// МОДЕЛЬ
namespace mynb.Models
{


    /// <summary>
    /// This partition contain methods SelectWaitStory, Approve and Decline, GenerateList, Add, Random
    /// todo: research for SQL-injection
    /// todo: 
    /// </summary>
    public class Story 
    {

        // fild and requirments 
        #region
        // requirments for send story Title
        [Required(ErrorMessage = "Введите заголовок")]
        public string title { get;  set; }
        // requirments for send story Content
        [Required(ErrorMessage = "Введите текст истории")]
        public string story { get; set; }
        // requirments for send story email
        #region
        [Required(ErrorMessage = "Введите электропочту")]
        [RegularExpression(
            @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@"+
            @"([a-z0-9_-]+\.)+[a-z]{2,6}", 
            //@"[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}",
            ErrorMessage = "Адрес эл.почты введён не корректно")]
        #endregion
        public string email { get; set; }
        #endregion
        // service fields and variables
        #region
        public string id { get; private set; }
        public string ename { get; private set; }
        public string post_date  { get; private set; }

        public Story[] list { get; private set; }

        public string error { get; private set; }
        //public IEnumerable<SelectListItem> Categories { get { return (IEnumerable<SelectListItem>)categories; } }

        //private SelectListItem categories =  { "Csharp", "ASPNET", "JS", "jQ", "CSS", "SQL", "NotCategory" };

              

        private MySQL sql;
        #endregion

        public Story(MySQL sql)
        {
            error = "";
            this.sql = sql;
        }
        public Story()
            {
            }

        // SelectWaitStory, Approve and Decline 
        #region
        public bool SelectWaitStory()
        {
             DataTable table = sql.Select(
                   @"SELECT id, title, story, email, post_date 
                            FROM story 
                            WHERE status = 'wait'
                            ORDER BY post_date ASC 
                            LIMIT 1" );
            if (table == null || table.Rows.Count == 0)
                return false;
            ExtractRow(table);
            return true;
        }


        public void Approve( string id)
        {
            sql.Update(
                  @"UPDATE story
                        SET status = 'show' 
                        WHERE id = '"+sql.addSlashes(id) + "' LIMIT 1");
            
        }

        public void Decline(string id)
        {
            sql.Update(
                @"UPDATE story
                      SET status = 'drop' 
                      WHERE id = '" + sql.addSlashes(id) + "' LIMIT 1");
        }
        #endregion

        //  GenerateList select list of stories number of relevant "mylimit"
        #region
        public void GenerateList(string mylimit)
        {
            int limit;
            try
            {
                limit = int.Parse(mylimit);
            }
            catch
            {
                limit = 10;
            }
            DataTable table = sql.Select(
                        @"SELECT id, title, story, email, post_date
                        FROM story 
                        WHERE status = 'show'
                        ORDER BY post_date DESC 
                        LIMIT " + limit);
            try
            {
                list = new Story[table.Rows.Count];
                for (int j = 0; j < list.Length; j++)
                {
                    list[j] = new Story(sql);
                    list[j].ExtractRow(table, j);
                }
            }
            catch(Exception ex)
            {
                error = "GenereteList does not return any rows: "+ex.Message;
            }
        }
        #endregion

        //  methods: add story, getting Random and Number(id) 
        #region
        public void Add()
        {
            if ( (email ?? "").IndexOf('@') == -1 )
            {
                error = "Incorrect email address";
                return;
            }
            long id = sql.Insert(
                @"INSERT INTO story (title, story, email, post_date)
                VALUES ('" + sql.addSlashes(title) +
                         "', '" + sql.addSlashes(story) +
                         "', '" + sql.addSlashes(email) +
                         "', NOW())");
            if (id == -1)
            {
                error = "Error inserting record to database";
                return;
            }
            this.id = id.ToString();

        }

        public void Random()
        {
            DataTable table = sql.Select(
                    @"SELECT id, title, story, email, post_date
                        FROM story 
                        ORDER BY RAND() 
                        LIMIT 1");
            ExtractRow(table);
        }
      
        public void Number(string id)
        {
            DataTable table = sql.Select(
                   @"SELECT id, title, story, email, post_date 
                            FROM story 
                            WHERE id = '" + sql.addSlashes(id) + "'");
            ExtractRow(table);
        }
        #endregion


        // Extract data from row from got table
        #region
        private void ExtractRow(DataTable table)
        {
            // starting from the zero row in table 
            ExtractRow(table, 0);
        }
        private void ExtractRow(DataTable table, int nr)
        {
            try
            {

                this.id = table.Rows[nr]["id"].ToString();
                title = table.Rows[nr]["title"].ToString();
                story = table.Rows[nr]["story"].ToString();
                post_date = ((DateTime)table.Rows[nr]["post_date"]).ToString("yyyy-MM-dd"); 
                email = table.Rows[nr]["email"].ToString();

                // creating ename - short name, which will be presented to the author
                #region
                int pos = email.IndexOf('@');
                    if ( pos < 0)
                    {
                        ename = email;
                    }
                    else
                    {
                        ename = email.Substring(0, pos);
                    }
                #endregion 
            }
            catch
            {
                this.id = "";
                title = "";
                story = "";
                email = "";
                ename = "";
                post_date = "";
                error = "Record not found";
                return;
            }
        }
        #endregion

        public bool IsError()
        {
            return error != "";
        }

        
    }

    

}