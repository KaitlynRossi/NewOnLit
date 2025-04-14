using System.ComponentModel.DataAnnotations;

namespace ASPProject.Models
{
    public class Customer
    {
        public int id { get; set; } = default!;

        [Required(ErrorMessage = "First name is required.")]
        public string firstName { get; set; } = default!;

        [Required(ErrorMessage = "Last name is required.")]
        public string lastName { get; set; } = default!;

        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Please confirm your email.")]
        [Compare("Email", ErrorMessage = "Emails do not match.")]
        public string confirmEmail { get; set; } = default!;

    }
}
