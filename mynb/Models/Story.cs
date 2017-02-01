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

        public string title { get; private set; }
        public string story { get; private set; }
        public string title1 { get; private set; }
        public string story1 { get; private set; }
        ArStory ArSt = new ArStory();

        public Story()
        {
            
            //title = ArSt.stor_story[0, 0];
            //story = ArSt.stor_story[0, 1];
            //title1 = ArSt.stor_story[1, 0];
            //story1 = ArSt.stor_story[1, 1];
            DataTable table = MySQL.Select("SELECT title, story FROM story");
            title = table.Rows[0]["title"].ToString();
            story = table.Rows[0]["story"].ToString();


        }

        public void add()
        {

        }

        public void Random()
        {
            Random rnd = new Random();
            int x = rnd.Next(0, 3);
            title = ArSt.stor_story[x, 0];
            story = ArSt.stor_story[x, 1];
        }
    }

    

}