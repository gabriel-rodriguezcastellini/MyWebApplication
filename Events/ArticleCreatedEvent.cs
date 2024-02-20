namespace MyWebApplication.Events;

public class ArticleCreatedEvent : IEvent
{
    public required string ArticleID { get; set; }

    public required string UserID { get; set; }

    public required string ArticleName { get; set; }
}
