using FluentValidation;

namespace MyWebApplication.Features.User.SignUp
{
    public class Request
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Age { get; set; }
        public Address Address { get; set; } = null!;
    }

    public class Verifier : Validator<Request>
    {
        public Verifier()
        {
            _ = RuleFor(x => x.Name).NotEmpty();
            _ = RuleFor(x => x.Email).MinimumLength(5);
            _ = RuleFor(x => x.Password).MinimumLength(5).MaximumLength(20);
            _ = RuleFor(x => x.Age).GreaterThan(15);
            _ = RuleFor(x => x.Address).NotNull();
            _ = RuleFor(x => x.Address.Street).MinimumLength(5).MaximumLength(20);
            _ = RuleFor(x => x.Address.City).MinimumLength(5).MaximumLength(20);
            _ = RuleFor(x => x.Address.Country).MinimumLength(5).MaximumLength(20);
        }
    }

    public class Response
    {
        public string Message { get; set; } = null!;
    }

    public class Address
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
