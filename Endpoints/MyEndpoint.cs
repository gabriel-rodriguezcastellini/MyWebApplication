using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MyWebApplication.Endpoints
{
    public class MyEndpoint : EndpointWithoutRequest<NotFound>
    {
        public override void Configure()
        {
            Get("/person");
            AllowAnonymous();
        }

        public override async Task<NotFound> ExecuteAsync(CancellationToken ct)
        {
            await Task.CompletedTask;
            return TypedResults.NotFound();
        }
    }
}
