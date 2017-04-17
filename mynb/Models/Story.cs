using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
// МОДЕЛЬ
namespace mynb.Models
{
    
    public class Story 
    {

        
        [Required(ErrorMessage = "Введите заголовок")]
        public string title { get;  set; }
        [Required(ErrorMessage = "Введите текст истории")]
        public string story { get; set; }

        [Required(ErrorMessage = "Введите электропочту")]
        [RegularExpression(
            @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@"+
            @"([a-z0-9_-]+\.)+[a-z]{2,6}", 
            //@"[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}",
            ErrorMessage = "Адрес эл.почты введён не корректно")]
        public string email { get; set; }

        public string id { get; private set; }
        public string ename { get; private set; }
        public string post_date  { get; private set; }

        public Story[] list { get; private set; }

        public string error { get; private set; }
        private MySQL sql;
        public Story(MySQL sql)
        {
            error = "";
            this.sql = sql;
        }
        // тестируем инициализацию Story post
        public Story()
        {
            
        }

        // ВНИМАНИЕ! БДИ SQL-инъекции
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

        private void ExtractRow(DataTable table)
        {
            ExtractRow(table, 0);
        }
        private void ExtractRow(DataTable table, int nr)
        {
            try
            {

                this.id = table.Rows[nr]["id"].ToString();
                title = table.Rows[nr]["title"].ToString();
                story = table.Rows[nr]["story"].ToString();
                email = table.Rows[nr]["email"].ToString();
                int pos = email.IndexOf('@');
                    if ( pos < 0)
                    {
                        ename = email;
                    }
                    else
                    {
                        ename = email.Substring(0, pos);
                    }
                post_date = ((DateTime)table.Rows[nr]["post_date"]).ToString("yyyy-MM-dd"); ;

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

        public bool IsError()
        {
            return error != "";
        }

        
    }

    

}