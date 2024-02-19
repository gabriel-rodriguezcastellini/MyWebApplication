using MyWebApplication.Features.Person.Save;

namespace MyWebApplication.PostProcessors;

public class DurationLogger : PostProcessor<Request, StateBag, object>
{
    public override Task PostProcessAsync(IPostProcessorContext<Request, object> ctx,
                                          StateBag state,
                                          CancellationToken ct)
    {
        ctx.HttpContext.Resolve<ILogger<DurationLogger>>()
           .LogInformation("request took {@duration} ms.", state.DurationMillis);

        return Task.CompletedTask;
    }
}
