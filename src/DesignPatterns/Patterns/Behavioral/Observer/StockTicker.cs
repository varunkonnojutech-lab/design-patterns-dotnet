namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Observer: <see cref="StockTicker"/> notifies all subscribers whenever a price
/// changes. Subscribers can come and go without touching the ticker.
/// </summary>
public sealed class StockTicker
{
    private readonly List<Action<string, decimal>> _subscribers = new();

    public void Subscribe(Action<string, decimal> handler) => _subscribers.Add(handler);

    public void Unsubscribe(Action<string, decimal> handler) => _subscribers.Remove(handler);

    public void SetPrice(string symbol, decimal price)
    {
        // Snapshot so a subscriber unsubscribing mid-notification is safe.
        foreach (var subscriber in _subscribers.ToArray())
        {
            subscriber(symbol, price);
        }
    }

    public static void Demo()
    {
        var ticker = new StockTicker();

        // Subscriber 1: always shows the price.
        ticker.Subscribe((symbol, price) =>
            Console.WriteLine($"Display: {symbol} is now {price:C}"));

        // Subscriber 2: only alerts above a threshold.
        const decimal threshold = 150m;
        ticker.Subscribe((symbol, price) =>
        {
            if (price > threshold)
            {
                Console.WriteLine($"ALERT: {symbol} exceeded {threshold:C} at {price:C}!");
            }
        });

        ticker.SetPrice("MSFT", 148.50m);
        ticker.SetPrice("MSFT", 152.75m);
    }
}
