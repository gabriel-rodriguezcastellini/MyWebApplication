using FastEndpoints;
using MyWebApplication.Requests;
using MyWebApplication.Services;

namespace MyWebApplication.Endpoints
{
    public class UserLoginEndpoint : Endpoint<LoginRequest>
    {
        private readonly IAuthenticationService _authenticationService;

        public UserLoginEndpoint(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public override void Configure()
        {
            Post("/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest request, CancellationToken ct)
        {
            (int, string) login = await _authenticationService.Login(new Models.LoginModel()
            {
                UserName = request.UserName,
                Password = request.Password
            });
            if (login.Item1 == 1)
            {
                await SendAsync(new
                {
                    Username = request.UserName,
                    Token = login.Item2
                });
            }
            else
            {
                ThrowError("The supplied credentials are invalid!");
            }
        }
    }
}
