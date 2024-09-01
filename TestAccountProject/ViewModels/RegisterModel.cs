using System.ComponentModel.DataAnnotations;

namespace TestAccountProject.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Email is not entered")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is not entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
