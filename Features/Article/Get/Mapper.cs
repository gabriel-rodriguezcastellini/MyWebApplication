namespace MyWebApplication.Features.Article.Get;

public class Mapper : ResponseMapper<Response, Article>
{
    public override Response FromEntity(Article e)
    {
        return new()
        {
            Name = e.Name,
            ArticleState = (ArticleState)e.ArticleState
        };
    }
}
