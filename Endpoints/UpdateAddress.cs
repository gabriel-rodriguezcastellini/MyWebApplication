using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using MyWebApplication.Constants;
using MyWebApplication.Requests;
using MyWebApplication.Responses;

namespace MyWebApplication.Endpoints
{
    [HttpPost("/my-endpoint")]
    [Authorize(Roles = RoleConstants.Manager)]
    public class UpdateAddress : Endpoint<MyRequest, MyResponse>
    {
        public override async Task HandleAsync(MyRequest request, CancellationToken ct)
        {
            await SendAsync(new MyResponse { });
        }
    }
}
