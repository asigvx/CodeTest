using System.ComponentModel.DataAnnotations;

namespace VechiclesInformation.Models
{
    public class AuthenticateModel
    {
        [Required]
        [Key]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
