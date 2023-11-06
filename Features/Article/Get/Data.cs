using MongoDB.Entities;

namespace MyWebApplication.Features.Article.Get
{
    public static class Data
    {
        public static Task<Article> GetArticle(string id)
        {
            return DB.Find<Article>().Match(id).ExecuteSingleAsync()!;
        }
    }
}
