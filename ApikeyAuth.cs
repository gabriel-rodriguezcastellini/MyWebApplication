using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace MyWebApplication;

internal sealed class ApikeyAuth(IOptionsMonitor<AuthenticationSchemeOptions> options,
                        ILoggerFactory logger,
                        UrlEncoder encoder,
                        IConfiguration config)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    internal const string _schemeName = "ApiKey";
    internal const string _headerName = "x-api-key";
    private readonly string _apiKey = config["Auth:ApiKey"] ?? throw new InvalidOperationException("Api key not set in appsettings.json");

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        _ = Request.Headers.TryGetValue(_headerName, out Microsoft.Extensions.Primitives.StringValues extractedApiKey);

        if (!IsPublicEndpoint() && !extractedApiKey.Equals(_apiKey))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API credentials!"));
        }

        ClaimsIdentity identity = new(
            claims: [new Claim("ClientID", "Default")],
            authenticationType: Scheme.Name);
        GenericPrincipal principal = new(identity, roles: null);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private bool IsPublicEndpoint()
    {
        return Context.GetEndpoint()?.Metadata.OfType<AllowAnonymousAttribute>().Any() is null or true;
    }
}
