using MyWebApplication.Features.Person.Save;

namespace MyWebApplication.PreProcessors;

public class StateBag : PreProcessor<Request, MyWebApplication.StateBag>
{
    public override Task PreProcessAsync(IPreProcessorContext<Request> context, MyWebApplication.StateBag state, CancellationToken ct)
    {
        state.Status = $"person saved at {state.DurationMillis} ms.";

        return Task.CompletedTask;
    }
}
