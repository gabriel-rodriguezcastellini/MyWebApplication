using MyWebApplication.Commands;

namespace MyWebApplication.CommandHandlers;

public class FullAddressHandler : CommandHandler<GetFullAddress, string>
{
    public override Task<string> ExecuteAsync(GetFullAddress command, CancellationToken ct)
    {
        if (command.Country.Length < 5)
        {
            AddError(c => c.Country, "country is too short!");
        }

        ThrowIfAnyErrors();

        return Task.FromResult(command.Country + " " + command.City + " " + command.Street);
    }
}
