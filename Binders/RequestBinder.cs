using System.Text.Json;

namespace MyWebApplication.Binders
{
    public class RequestBinder<TRequest> : IRequestBinder<TRequest> where TRequest : notnull, new()
    {
        public async ValueTask<TRequest> BindAsync(BinderContext ctx, CancellationToken ct)
        {
            if (ctx.HttpContext.Request.HasJsonContentType())
            {
                TRequest? req = await JsonSerializer.DeserializeAsync<TRequest>(
                  ctx.HttpContext.Request.Body, ctx.SerializerOptions, ct);

                if (req is IHasTenantId r)
                {
                    r.TenantId = ctx.HttpContext.Request.Headers["x-tenant-id"];
                }

                return req!;
            }
            return new();
        }
    }

}
