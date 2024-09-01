using System.ComponentModel.DataAnnotations;

namespace TestAccountProject.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email is not entered")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is not entered")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
