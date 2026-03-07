
using System.ComponentModel.DataAnnotations;

namespace CreditCardAPI.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
       
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? Role { get; set; }
    }
}