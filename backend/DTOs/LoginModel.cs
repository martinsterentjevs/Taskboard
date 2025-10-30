namespace Taskboard.API.DTOs
{
    public class LoginModel
    {
        public required string Identifier { get; set; }
        public required string Password { get; set; }
    }
}
