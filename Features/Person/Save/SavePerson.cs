using MyWebApplication.PostProcessors;
using MyWebApplication.PreProcessors;
using System.Globalization;

namespace MyWebApplication.Features.Person.Save;

public class SavePerson : EndpointWithMapping<Request, Response, Person>
{
    public override void Configure()
    {
        Put("/person");
        Tags("include me");
        Description(x => x.WithName("SavePerson"));
        PreProcessor<SecurityProcessor<Request>>();
        PostProcessor<ResponseLogger<Request, Response>>();
        PostProcessor<ExceptionProcessor>();
        PreProcessor<PreProcessors.StateBag>();
        PostProcessor<DurationLogger>();
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        StateBag state = ProcessorState<StateBag>();
        Logger.LogInformation("endpoint executed at {@duration} ms.", state.DurationMillis);
        await Task.Delay(100, ct);
        Person entity = MapToEntity(r);
        Response = MapFromEntity(entity);
        Response.Status = state.Status;
        await SendAsync(Response, cancellation: ct);
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
            Age = (DateOnly.FromDateTime(DateTime.UtcNow).DayNumber - e.DateOfBirth.DayNumber) / 365
        };
    }
}
