using System.Text.Json.Serialization;

namespace MyWebApplication.Features.User.UpdateAddress;

[JsonSerializable(typeof(UpdateAddressRequest))]
[JsonSerializable(typeof(Response))]
public partial class UpdateAddressCtx : JsonSerializerContext { }
