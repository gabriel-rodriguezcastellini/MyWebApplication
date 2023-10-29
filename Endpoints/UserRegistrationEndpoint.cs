using FastEndpoints;
using MyWebApplication.Constants;
using MyWebApplication.Requests;
using MyWebApplication.Services;

namespace MyWebApplication.Endpoints
{
    public class UserRegistrationEndpoint : Endpoint<RegistrationRequest>
    {
        private readonly IAuthenticationService _authenticationService;

        public UserRegistrationEndpoint(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public override void Configure()
        {
            Post("/registration");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RegistrationRequest request, CancellationToken ct)
        {
            (int, string) registration = await _authenticationService.Registration(new Models.RegistrationModel
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                UserName = request.UserName
            }, RoleConstants.Manager);
            await SendAsync(registration.Item2);
        }
    }
}
