namespace MyWebApplication.Features.User.Profile;

public class Binder : RequestBinder<Request>
{
    public override async ValueTask<Request> BindAsync(BinderContext ctx, CancellationToken cancellation)
    {
        _ = await base.BindAsync(ctx, cancellation);
        return new Request
        {
            UserID = ctx.HttpContext.User.ClaimValue("UserID")!,
            TenantId = ctx.HttpContext.Request.Headers["TenantId"].ToString()!
        };
    }
}
