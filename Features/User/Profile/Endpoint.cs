using MyWebApplication.Features.User.Authentication;

namespace MyWebApplication.Features.User.Profile;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Get("/user/profile");
        Permissions(Allow.User_Profile_View);
        RequestBinder(new Binder());
        ResponseCache(60); //cache for 60 seconds
        Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
        Tags("include me");
        Options(x => x.WithTags("Users"));
        Description(x => x.WithName("Profile"));
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        User? user = await Data.GetUser(r.UserID);

        if (user is null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            await SendAsync(Map.FromEntity(user), cancellation: ct);
        }
    }
}
