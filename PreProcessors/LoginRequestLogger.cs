using MyWebApplication.Features.User.Authentication.Login;

namespace MyWebApplication.PreProcessors;

public class LoginRequestLogger : IPreProcessor<Request>
{
    public Task PreProcessAsync(IPreProcessorContext<Request> context, CancellationToken ct)
    {
        ILogger<Request> logger = context.HttpContext.Resolve<ILogger<Request>>();

        logger.LogInformation("email value:{@Email}", context.Request.Email);

        return Task.CompletedTask;
    }
}
