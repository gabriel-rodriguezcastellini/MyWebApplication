using MongoDB.Entities;

namespace MyWebApplication.Features.Article;

public class Article : Entity
{
    public string Name { get; set; } = null!;
    public ArticleState ArticleState { get; set; }

    static Article()
    {
        _ = DB.Index<Article>().Key(x => x.Name, KeyType.Ascending).CreateAsync();
    }
}
