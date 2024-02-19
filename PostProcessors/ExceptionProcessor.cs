using MyWebApplication.Features.Person.Save;

namespace MyWebApplication.PostProcessors;

public class ExceptionProcessor : IPostProcessor<Request, Response>
{
    public async Task PostProcessAsync(IPostProcessorContext<Request, Response> ctx, CancellationToken cancellationToken)
    {
        if (!ctx.HasExceptionOccurred)
        {
            return;
        }

        if (ctx.ExceptionDispatchInfo.SourceException.GetType() == typeof(FormatException))
        {
            ctx.MarkExceptionAsHandled(); //only if handling the exception here.

            await ctx.HttpContext.Response.SendAsync("The format of an argument is invalid!", 400, cancellation: cancellationToken);
            return;
        }

        ctx.ExceptionDispatchInfo.Throw();
    }
}
