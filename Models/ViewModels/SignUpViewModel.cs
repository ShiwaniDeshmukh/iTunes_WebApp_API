using System.ComponentModel.DataAnnotations;

namespace iTunes_WebApp_API.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
