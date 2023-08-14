using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskPr.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        [MaxLength(25, ErrorMessage = "Name cannot be more than 25 characters")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Minimum eight characters, at least one letter and one number")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Uncorrect password")]
        public string ConfirmPassword { get; set; }

        public List<UrlModel> Urls { get; set; } = new List<UrlModel>();
    }
}
