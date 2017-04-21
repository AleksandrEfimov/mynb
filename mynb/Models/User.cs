using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mynb.Models
{
    public class User
    {
        private MySQL sql;

        [Required(ErrorMessage = "Введите логин")]
        public string login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string passw { get; set; }

        public string status;

        public User(MySQL sql)
        {
            this.sql = sql;
            status = "";
        }
        public User()
        {
            sql = null;
            status = "";
        }
        public void CheckLogin()
        {
            this.status = sql.Scalar(
                @"SELECT COUNT(*) FROM user
                        WHERE login = '" + sql.addSlashes(login) +
                       "' AND passw = '" + sql.addSlashes(passw) + 
                       "' AND status > 0");
        }


    }
}