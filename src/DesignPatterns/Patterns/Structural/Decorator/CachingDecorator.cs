using System.Diagnostics;

namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Decorator: adds caching to any <see cref="IDataService"/> by wrapping it.
/// The inner service knows nothing about the cache.
/// </summary>
public interface IDataService
{
    Task<string> GetDataAsync(string key);
}

/// <summary>The slow underlying service we want to speed up.</summary>
public sealed class SlowDataService : IDataService
{
    public async Task<string> GetDataAsync(string key)
    {
        await Task.Delay(500);
        return $"Data for {key}";
    }
}

/// <summary>The decorator: same interface, adds an in-memory cache.</summary>
public sealed class CachingDataService : IDataService
{
    private readonly IDataService _inner;
    private readonly Dictionary<string, string> _cache = new();

    public CachingDataService(IDataService inner)
    {
        _inner = inner;
    }

    public async Task<string> GetDataAsync(string key)
    {
        if (_cache.TryGetValue(key, out var cached))
        {
            return cached;
        }

        var result = await _inner.GetDataAsync(key).ConfigureAwait(false);
        _cache[key] = result;
        return result;
    }

    public static async Task Demo()
    {
        IDataService service = new CachingDataService(new SlowDataService());

        var stopwatch = Stopwatch.StartNew();
        Console.WriteLine(await service.GetDataAsync("order-123"));
        Console.WriteLine($"First call: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch.Restart();
        Console.WriteLine(await service.GetDataAsync("order-123"));
        Console.WriteLine($"Second call (cached): {stopwatch.ElapsedMilliseconds} ms");
    }
}
