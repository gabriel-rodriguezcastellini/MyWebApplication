using MyWebApplication.Commands;

namespace MyWebApplication.Features.User.Profile;

public class Mapper : Mapper<Request, Response, User>
{
    public override async Task<Response> FromEntityAsync(User e, CancellationToken ct = default)
    {
        return new()
        {
            Age = e.Age,
            Email = e.Email,
            Name = e.Name,
            Address = await new GetFullAddress
            {
                City = e.Address.City,
                Country = e.Address.Country,
                Street = e.Address.Street
            }.ExecuteAsync(ct: ct),
            Ticks = DateTime.UtcNow.Ticks,
            Message = "this response is cached"
        };
    }
}
