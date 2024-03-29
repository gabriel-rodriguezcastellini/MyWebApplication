﻿namespace MyWebApplication.Features.User.SignUp;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Post("/user/signup");
        AllowAnonymous();
        DontThrowIfValidationFails();
        Tags("include me");
        Options(x => x.WithTags("Users"));
        Description(x => x.WithName("SignUp"));
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        ThrowIfAnyErrors();
        string userID = await Data.CreateUser(Map.ToEntity(r));

        if (string.IsNullOrEmpty(userID))
        {
            ThrowError("User creation failed!");
        }

        Response.Message = $"The user [{r.Name}] has been created with ID: {userID}";
    }
}
