namespace MyWebApplication.PostProcessors;

public class ResponseLogger<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
{
    public Task PostProcessAsync(IPostProcessorContext<TRequest, TResponse> context, CancellationToken ct)
    {
        ILogger<TResponse> logger = context.HttpContext.Resolve<ILogger<TResponse>>();

        if (context.Response is Features.Person.Save.Response response)
        {
            logger.LogWarning("person saved: {@Id}", response.Id);
        }

        return Task.CompletedTask;
    }
}
