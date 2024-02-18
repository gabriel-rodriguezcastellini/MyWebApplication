namespace MyWebApplication.Features.Article.Get;

public class Response
{
    public string Name { get; set; } = null!;
    public ArticleState ArticleState { get; set; }
}

public enum ArticleState
{
    Created,
    Approved,
    Rejected
}
