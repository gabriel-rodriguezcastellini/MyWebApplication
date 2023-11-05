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
                Name = e.Name
            };
        }
    }
}
