namespace MyWebApplication.Features.Article.Moderate
{
    public class Mapper : Mapper<Request, Response, Article>
    {
        public override Article UpdateEntity(Request r, Article e)
        {
            e.ArticleState = ArticleState.Approved;
            return e;
        }
    }
}
