using MyWebApplication.Features.Article.Create;

namespace MyWebApplication.Summaries;

public class ArticleCreate : Summary<Features.Article.Create.Endpoint>
{
    public ArticleCreate()
    {
        Summary = "short summary goes here";
        Description = "long description goes here";
        ExampleRequest = new Request { Name = nameof(Request.Name) };
        Response<Response>(200, "ok response with body", example: new() { Message = nameof(Features.Article.Create.Response.Message) });
        Response<ErrorResponse>(400, "validation failure");
    }
}
