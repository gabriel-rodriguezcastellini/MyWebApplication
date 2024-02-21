namespace MyWebApplication.Commands;

public class GetFullAddress : ICommand<string>
{
    public required string Country { get; set; }

    public required string City { get; set; }

    public required string Street { get; set; }
}
