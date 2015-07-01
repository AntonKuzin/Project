using System.ComponentModel.DataAnnotations;

namespace test.ViewModels
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "Field cannot be empty")]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field cannto be empty")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }
    }
}