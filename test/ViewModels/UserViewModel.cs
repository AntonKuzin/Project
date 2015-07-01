using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace test.ViewModels
{
    public class UserViewModel
    {

        [Display(Name = "Username")]
        public string Name { get; set; }

        [Display(Name = "User email")]
        public string Email { get; set; }

        public int Id { get; set; }

        [Display(Name = "User roles")]
        public IEnumerable<ORM.Roles> Roles { get; set; }

        
    }
}