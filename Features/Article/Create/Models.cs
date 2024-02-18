using FluentValidation;

namespace MyWebApplication.Features.Article.Create;

public class Request
{
    public string Name { get; set; } = null!;
}

public class Verifier : Validator<Request>
{
    public Verifier()
    {
        _ = RuleFor(x => x.Name).NotEmpty();
    }
}

public class Response
{
    public string Message { get; set; } = null!;
}
