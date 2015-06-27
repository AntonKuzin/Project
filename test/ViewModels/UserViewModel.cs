using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace test.ViewModels
{
    public class UserViewModel
    {

        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }

        [Display(Name = "Email пользователя")]
        public string Email { get; set; }

        public int Id { get; set; }

        [Display(Name = "Роль пользователя в системе")]
        public IEnumerable<ORM.Roles> Roles { get; set; }

        
    }
}