using FluentValidation;

namespace MyWebApplication.Features.User.Profile
{
    public class Request
    {
        [FromClaim("UserID")]
        public string UserID { get; set; } = null!;
    }

    public class Verifier : Validator<Request>
    {
        public Verifier()
        {
            _ = RuleFor(x => x.UserID).NotEmpty();
        }
    }

    public class Response
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
    }
}
