using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ORM;

namespace test.ViewModels
{
    public class PictureViewModel
    {
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Путь к файлу")]
        public string Url { get; set; }

        public int Id { get; set; }

        public ORM.Likes Like { get; set; }

        public int Rating { get; set; }

        public int UserId { get; set; }

        public string UserEmail { get; set; }
    }
}