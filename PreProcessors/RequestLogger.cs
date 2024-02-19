namespace MyWebApplication.PreProcessors;

public class RequestLogger<TRequest> : IPreProcessor<TRequest>
{
    public Task PreProcessAsync(IPreProcessorContext<TRequest> ctx, CancellationToken ct)
    {
        ILogger<TRequest> logger = ctx.HttpContext.Resolve<ILogger<TRequest>>();

        logger.LogInformation("request:{@FullName} path: {@Path}", ctx.Request.GetType().FullName, ctx.HttpContext.Request.Path);

        return Task.CompletedTask;
    }
}
