using FluentValidation;
using System.Text.Json.Serialization;

namespace MyWebApplication.Features.User.UpdateAddress;

public class UpdateAddressRequest
{
    [FromClaim("UserID")]
    public string UserID { get; set; } = null!;

    [JsonPropertyName("address")]
    public Address UserAddress { get; set; } = null!;
}

public class Verifier : Validator<UpdateAddressRequest>
{
    public Verifier()
    {
        _ = RuleFor(x => x.UserID).NotEmpty();
    }
}

public class Response
{
    public string Message { get; set; } = null!;
}

public class Address
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
}
