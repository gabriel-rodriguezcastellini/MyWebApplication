using FluentValidation;
using MyWebApplication.Binders;

namespace MyWebApplication.Features.User.Profile;

public class Request : IHasTenantId
{
    [FromClaim("UserID")]
    public string UserID { get; set; } = null!;

    [FromHeader(IsRequired = false)]
    public string? TenantId { get; set; }
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
    public string Address { get; set; } = null!;
    public long Ticks { get; set; }
    public string Message { get; set; } = null!;
}