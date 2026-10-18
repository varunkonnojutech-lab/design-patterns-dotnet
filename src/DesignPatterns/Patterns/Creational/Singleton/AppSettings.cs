namespace DesignPatterns.Patterns.Creational;

/// <summary>
/// Singleton: guarantees a single, lazily-initialized instance of application settings.
/// <see cref="Lazy{T}"/> is thread-safe by default (ExecutionAndPublication mode).
/// </summary>
public sealed class AppSettings
{
    private static readonly Lazy<AppSettings> _lazy = new(() => new AppSettings());

    private AppSettings()
    {
        AppName = "DesignPatterns Demo";
        MaxRetries = 3;
    }

    public static AppSettings Instance => _lazy.Value;

    public string AppName { get; }

    public int MaxRetries { get; }

    public static void Demo()
    {
        var first = Instance;
        var second = Instance;

        Console.WriteLine($"AppName: {first.AppName}, MaxRetries: {first.MaxRetries}");
        Console.WriteLine($"First instance hash:  {first.GetHashCode()}");
        Console.WriteLine($"Second instance hash: {second.GetHashCode()}");
        Console.WriteLine($"Same reference: {ReferenceEquals(first, second)}");
    }
}
