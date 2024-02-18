namespace MyWebApplication.Features.User.SignUp;

public class Mapper : Mapper<Request, Response, User>
{
    public override User ToEntity(Request r)
    {
        return new()
        {
            Age = r.Age,
            Email = r.Email,
            Password = r.Password, //never store clear passwords in db. always hash/salt before saving.
            Name = r.Name,
            Address = new()
            {
                Street = r.Address.Street,
                City = r.Address.City,
                Country = r.Address.Country
            }
        };
    }
}
