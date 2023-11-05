namespace MyWebApplication.Features.Article.Create
{
    public class Mapper : Mapper<Request, Response, Article>
    {
        public override Article ToEntity(Request r)
        {
            return new()
            {
                Name = r.Name,
                ArticleState = ArticleState.Created
            };
        }
    }
}
