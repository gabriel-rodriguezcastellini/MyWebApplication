namespace MyWebApplication.PreProcessors;

public class SecurityProcessor<TRequest> : IPreProcessor<TRequest>
{
    public Task PreProcessAsync(IPreProcessorContext<TRequest> ctx, CancellationToken ct)
    {
        if (!ctx.HttpContext.Request.Headers.TryGetValue("x-api-key", out _))
        {
            ctx.ValidationFailures.Add(
                new("MissingHeaders", "The [x-api-key] header needs to be set!"));

            //sending response here
            return ctx.HttpContext.Response.SendErrorsAsync(ctx.ValidationFailures, cancellation: ct);
        }

        return Task.CompletedTask;
    }
}
