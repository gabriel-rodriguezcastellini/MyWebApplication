namespace MyWebApplication.Features.User.Authentication.Login
{
    public class Request
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
