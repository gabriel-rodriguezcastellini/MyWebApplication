namespace MyWebApplication.Features.User.Profile
{
    public class Mapper : Mapper<Request, Response, User>
    {
        public override Response FromEntity(User e)
        {
            return new()
            {
                Age = e.Age,
                Email = e.Email,
                Name = e.Name,
                Address = new()
                {
                    City = e.Address.City,
                    Country = e.Address.Country,
                    Street = e.Address.Street
                },
                Ticks = DateTime.UtcNow.Ticks,
                Message = "this response is cached"
            };
        }
    }
}
