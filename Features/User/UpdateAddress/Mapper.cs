namespace MyWebApplication.Features.User.UpdateAddress;

public class Mapper : Mapper<UpdateAddressRequest, Response, User>
{
    public override User UpdateEntity(UpdateAddressRequest r, User e)
    {
        e.Address.Street = r.UserAddress.Street;
        e.Address.City = r.UserAddress.City;
        e.Address.Country = r.UserAddress.Country;
        return e;
    }
}
