using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskboard.API.Models.Auth
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Full_Name { get; set; } 
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public required string Salt { get; set; }
        public RefreshTokens? refreshToken { get; set; }
    }
}
