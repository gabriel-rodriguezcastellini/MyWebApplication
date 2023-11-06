using FluentValidation;
using MyWebApplication.Binders;
using System.Text.Json.Serialization;

namespace MyWebApplication.Features.Article.Moderate
{
    public class Request : IHasRole
    {
        public string Id { get; set; } = null!;

        [HasPermission(Auth.Allow.Article_Approve)]
        public bool AllowedToUpdate { get; set; }

        [FromHeader]
        [JsonIgnore]
        public string Role { get; set; } = null!;
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
