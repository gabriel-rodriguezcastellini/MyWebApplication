namespace MyWebApplication.Features.User.UpdateAddress
{
    public class Endpoint : Endpoint<UpdateAddressRequest, Response, Mapper>
    {
        public override void Configure()
        {
            Post("/user/update-address");
            AccessControl(keyName: "User_UpdateAddress", behavior: Apply.ToThisEndpoint, groupNames: "Administrator");
            SerializerContext<UpdateAddressCtx>();
        }

        public override async Task HandleAsync(UpdateAddressRequest request, CancellationToken ct)
        {
            User? user = await Data.GetUser(request.UserID);

            if (user is null)
            {
                await SendNotFoundAsync(ct);
            }

            await Data.UpdateAddress(Map.UpdateEntity(request, user!));
            Response.Message = $"The address of the user [{request.UserID}] has been updated";
        }
    }
}
