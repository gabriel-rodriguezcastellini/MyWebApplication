using MyWebApplication.Events;

namespace MyWebApplication.EventHandlers;

public class ArticleCreationHandler(ILogger<ArticleCreationHandler> logger) : IEventHandler<ArticleCreatedEvent>
{
    public Task HandleAsync(ArticleCreatedEvent eventModel, CancellationToken ct)
    {
        logger.LogInformation("article created event received: [{@ArticleID}]", eventModel.ArticleID);
        return Task.CompletedTask;
    }
}
