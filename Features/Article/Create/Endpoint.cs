namespace MyWebApplication.Features.Article.Create;

public class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Post("/article");
        string[] groupNames = ["Author"];
        AccessControl(keyName: "Article_Create", behavior: Apply.ToThisEndpoint, groupNames: groupNames);
        Throttle(hitLimit: 120, durationSeconds: 60);
        Options(x => x.RequireRateLimiting("limiterPolicy"));
        Description(x => x.ClearDefaultProduces(200, 400, 401, 403));
        Tags("include me");
        Description(x => x.WithName("CreateArticle"));
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        string articleId = await Data.CreateArticle(Map.ToEntity(request));

        if (string.IsNullOrEmpty(articleId))
        {
            ThrowError("Article creation failed!");
        }

        Response.Message = $"The article [{request.Name}] has been created with ID: {articleId}";
    }
}
