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
        public string title { get; private set; }
        public string story { get; private set; }
        public string email { get; private set; }
        public string ename { get; private set; }
        public string post_date  { get; private set; }
        public bool error;
        public Story()
        {
            error = false;
        }

        public void Add()
        {

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
                ename = email.Substring(0, email.IndexOf('@'));
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
                error = true;
                return;
            }
        }

        public bool IsError()
        {
            return error;
        }
    }

    

}