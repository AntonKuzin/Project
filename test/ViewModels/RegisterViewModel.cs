using System;
using System.ComponentModel.DataAnnotations;

namespace test.ViewModels
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required field")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password")]
        [StringLength(100, ErrorMessage = "Password cannot be less than {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat password")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Captcha { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        public string AvatarPath { get; set; }
    }
}