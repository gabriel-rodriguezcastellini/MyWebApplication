using System.Diagnostics;

namespace MyWebApplication;

public class StateBag
{
    private readonly Stopwatch _sw = new();

    public bool IsValidAge { get; set; }
    public string Status { get; set; } = null!;
    public long DurationMillis => _sw.ElapsedMilliseconds;

    public StateBag()
    {
        _sw.Start();
    }
}
