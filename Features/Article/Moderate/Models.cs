using FluentValidation;

namespace MyWebApplication.Features.Article.Moderate
{
    public class Request
    {
        public string Id { get; set; } = null!;
    }

    public class Verifier : Validator<Request>
    {
        public Verifier()
        {
            _ = RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class Response
    {
        public string Message { get; set; } = null!;
    }
}
