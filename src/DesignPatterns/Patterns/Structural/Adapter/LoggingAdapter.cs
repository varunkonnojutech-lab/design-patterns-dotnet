namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Adapter: lets a legacy logger work behind the modern <see cref="ILogger"/> interface
/// without changing the legacy code.
/// </summary>
public interface ILogger
{
    void Log(string message);
}

/// <summary>Pre-existing class we cannot (or will not) change.</summary>
public sealed class LegacyLogger
{
    public void WriteLog(string message, int severity)
    {
        Console.WriteLine($"[severity={severity}] {message}");
    }
}

/// <summary>Adapts <see cref="LegacyLogger"/> to <see cref="ILogger"/>.</summary>
public sealed class LegacyLoggerAdapter : ILogger
{
    private readonly LegacyLogger _legacy;

    public LegacyLoggerAdapter(LegacyLogger legacy)
    {
        _legacy = legacy;
    }

    public void Log(string message)
    {
        _legacy.WriteLog(message, severity: 1);
    }

    public static void Demo()
    {
        ILogger logger = new LegacyLoggerAdapter(new LegacyLogger());
        logger.Log("Order placed successfully.");
    }
}
