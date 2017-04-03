using System;
using System.Data;

// МОДЕЛЬ
namespace mynb.Models
{
    class ArStory
    {
         internal string[,] stor_story = new string[,]
            { { "В чем разница между Array() и ArrayList()","Array фиксирован по длине и хранит данные одного типа, ArrayList - динамичен по размеру и разные типы" },
              { "Разница между const и readonly переменных в C#","const - значение определяется при инициализации,  дальнейшем его изменить не возможно, readonly - значение можно изменить во время работы приложения через переменную или конструктор" },
              { "Различия finalize() и finally","1е - метод использован для сборщика мусора, 2е - выполняет код независимо от наличия исключения в блоках try{} Catch{}" }
            };
        public ArStory() { }

    }
    public class Story 
    {

        public string id { get; private set; }
        public string title { get; set; }
        public string story { get; set; }
        public string email { get; set; }
        public string ename { get; private set; }
        public string post_date  { get; private set; }

        public Story[] list { get; private set; }

        public string error { get; private set; }
        public Story()
        {
            error = "";
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
            DataTable table = MySQL.Select(
                        @"SELECT id, title, story, email, post_date
                        FROM story 
                        ORDER BY post_date DESC 
                        LIMIT " + limit);
            list = new Story[table.Rows.Count];
            for (int j = 0; j < list.Length; j++)
            {
                list[j] = new Story();
                list[j].ExtractRow(table, j);
            }
        }

        public void Add()
        {
            long id = MySQL.Insert(
                @"INSERT INTO story (title, story, email, post_date)
                VALUES ('" + MySQL.addSlashes(title) +
                         "', '" + MySQL.addSlashes(story) +
                         "', '" + MySQL.addSlashes(email) +
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
            DataTable table = MySQL.Select(
                    @"SELECT id, title, story, email, post_date
                        FROM story 
                        ORDER BY RAND() 
                        LIMIT 1");
            ExtractRow(table);
        }
      

        public void Number(string id)
        {
            DataTable table = MySQL.Select(
                   @"SELECT id, title, story, email, post_date 
                            FROM story 
                            WHERE id = '" + MySQL.addSlashes(id) + "'");
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