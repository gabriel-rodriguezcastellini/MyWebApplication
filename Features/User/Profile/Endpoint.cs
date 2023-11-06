using MyWebApplication.Features.User.Authentication;

namespace MyWebApplication.Features.User.Profile
{
    public class Endpoint : Endpoint<Request, Response, Mapper>
    {
        public override void Configure()
        {
            Get("/user/profile");
            Permissions(Allow.User_Profile_View);
            RequestBinder(new Binder());
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
}
