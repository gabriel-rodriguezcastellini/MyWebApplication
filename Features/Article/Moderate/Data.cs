using MongoDB.Entities;

namespace MyWebApplication.Features.Article.Moderate
{
    public static class Data
    {
        public static async Task ModerateArticle(Article article)
        {
            await article.SaveAsync();
        }

        public static async Task<Article?> GetArticle(string id)
        {
            return await DB.Find<Article>().Match(x => x.ID == id).ExecuteSingleAsync();
        }
    }
}
