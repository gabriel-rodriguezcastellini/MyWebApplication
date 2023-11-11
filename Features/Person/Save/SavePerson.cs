using System.Globalization;

namespace MyWebApplication.Features.Person.Save
{
    public class SavePerson : EndpointWithMapping<Request, Response, Person>
    {
        public override void Configure()
        {
            Put("/person");
        }

        public override Task HandleAsync(Request r, CancellationToken ct)
        {
            Person entity = MapToEntity(r);
            Response = MapFromEntity(entity);
            return SendAsync(Response, cancellation: ct);
        }

        public override Person MapToEntity(Request r)
        {
            return new()
            {
                Id = r.Id,
                DateOfBirth = DateOnly.Parse(r.BirthDay, new CultureInfo("en-US")),
                FullName = $"{r.FirstName} {r.LastName}"
            };
        }

        public override Response MapFromEntity(Person e)
        {
            return new()
            {
                Id = e.Id,
                FullName = e.FullName,
                UserName = $"USR{e.Id:0000000000}",
                Age = (DateOnly.FromDateTime(DateTime.UtcNow).DayNumber - e.DateOfBirth.DayNumber) / 365,
            };
        }
    }
}
