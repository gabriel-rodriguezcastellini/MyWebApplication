namespace MyWebApplication.Features.Article.Moderate
{
    public class Endpoint : Endpoint<Request, Response, Mapper>
    {
        /// <summary>
        /// Article moderation endpoint
        /// </summary>
        public override void Configure()
        {
            Post("/article/moderate");
            AccessControl(keyName: "Article_Approve", behavior: Apply.ToThisEndpoint, groupNames: "Administrator");
        }

        public override async Task HandleAsync(Request request, CancellationToken ct)
        {
            Article? article = await Data.GetArticle(request.Id);

            if (article == null)
            {
                await SendNotFoundAsync(ct);
            }

            await Data.ModerateArticle(Map.UpdateEntity(request, article!));
            Response.Message = $"The article [{request.Id}] has been approved";
        }
    }
}
