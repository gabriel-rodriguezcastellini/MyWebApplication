using FastEndpoints;

namespace MyWebApplication
{
    public class MyEndpoint : Endpoint<EmptyRequest, EmptyResponse>
    {
        public override void Configure()
        {
            Post("/user/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(EmptyRequest request, CancellationToken ct)
        {
            await SendAsync(new(), cancellation: ct);
        }
    }
}
