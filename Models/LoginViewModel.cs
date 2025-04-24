using System.ComponentModel.DataAnnotations;

namespace ASPProject.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}