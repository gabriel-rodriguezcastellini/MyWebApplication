using FluentValidation;

namespace MyWebApplication.Features.User.SignUp
{
    public class Request
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Age { get; set; }
    }

    public class Verifier : Validator<Request>
    {
        public Verifier()
        {
            _ = RuleFor(x => x.Name).NotEmpty();
            _ = RuleFor(x => x.Email).MinimumLength(5);
            _ = RuleFor(x => x.Password).MinimumLength(5).MaximumLength(20);
            _ = RuleFor(x => x.Age).GreaterThan(15);
        }
    }

    public class Response
    {
        public string Message { get; set; } = null!;
    }
}
