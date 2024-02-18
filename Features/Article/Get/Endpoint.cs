namespace MyWebApplication.Features.Article.Get;

public class Endpoint : EndpointWithoutRequest<Response, Mapper>
{
    public override void Configure()
    {
        Get("/article/{ArticleID}");
        AllowAnonymous();
        Tags("include me");
        Description(x => x.WithName("GetArticle"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string articleID = Route<string>("ArticleID")!;
        Article? article = await Data.GetArticle(articleID);

        if (article is null)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            await SendAsync(Map.FromEntity(article), cancellation: ct);
        }
    }
}
