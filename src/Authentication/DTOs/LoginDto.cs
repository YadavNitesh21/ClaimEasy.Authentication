using System.ComponentModel.DataAnnotations;

namespace Authentication.DTOs
{
    public class LoginDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username should be between 3 and 100 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters long.")]
        public string Password { get; set; }
    }

}
