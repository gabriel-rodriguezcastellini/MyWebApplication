using MongoDB.Entities;

namespace MyWebApplication.Features.Article.Create;

public static class Data
{
    public static async Task<string> CreateArticle(Article article)
    {
        await article.SaveAsync();
        return article.ID!;
    }
}
