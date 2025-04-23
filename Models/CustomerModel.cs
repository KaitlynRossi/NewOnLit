using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPProject.Models
{
    [Table("User")]
    public class Customer
    {
        [Key]
        [Column("UserID")]
        public int UserID { get; set; }

        [Required, Column("email")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, Column("UserName")]
        public string UserName { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("memberRole")]
        public int MemberRole { get; set; } = 0;
    }
}
