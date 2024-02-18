using MyWebApplication.Features.User.Authentication.RefreshToken;

namespace MyWebApplication.Features.User.Authentication.Login;

public class Endpoint : Endpoint<Request, MyTokenResponse>
{
    public override void Configure()
    {
        Post("user/auth/login");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "summary";
            s.Description = "description";
            s.RequestParam(r => r.Email, "username");
        });
        Tags("include me");
        Options(x => x.WithTags("Users"));
        Description(x => x.WithName("Login"));
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        string? userID = await Data.GetUserID(r.Email, r.Password);

        if (string.IsNullOrEmpty(userID))
        {
            ThrowError("Invalid user credentials!");
        }

        Response = await CreateTokenWith<UserTokenService>(userID, p =>
        {
            p.Claims.Add(new("UserID", userID));
            p.Permissions.AddRange(new Allow().AllCodes());
            p.Permissions.AddRange(Auth.Allow.AllCodes());
            p.Roles.Add("Manager");
        });
    }
}
