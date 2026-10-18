using DesignPatterns.Patterns.Behavioral;
using DesignPatterns.Patterns.Creational;
using DesignPatterns.Patterns.Structural;
using Xunit;

namespace DesignPatterns.Tests;

public sealed class PatternTests
{
    [Fact]
    public void Singleton_ReturnsSameInstance()
    {
        Assert.Same(AppSettings.Instance, AppSettings.Instance);
        Assert.Equal("DesignPatterns Demo", AppSettings.Instance.AppName);
    }

    [Fact]
    public void Builder_BuildsRequestWithAllParts()
    {
        var request = new HttpRequestBuilder()
            .WithMethod("POST")
            .WithUrl("https://api.example.com/orders")
            .AddHeader("Content-Type", "application/json")
            .WithBody("{\"sku\":\"ABC-123\"}")
            .Build();

        Assert.Equal("POST", request.Method);
        Assert.Equal("https://api.example.com/orders", request.Url);
        Assert.Equal("application/json", request.Headers["Content-Type"]);
        Assert.Equal("{\"sku\":\"ABC-123\"}", request.Body);
    }

    [Fact]
    public void Builder_DefaultsToEmptyBodyAndGet()
    {
        var request = new HttpRequestBuilder()
            .WithUrl("https://api.example.com")
            .Build();

        Assert.Equal("GET", request.Method);
        Assert.Null(request.Body);
        Assert.Empty(request.Headers);
    }

    [Fact]
    public void Builder_ThrowsWhenUrlMissing()
    {
        var builder = new HttpRequestBuilder().WithMethod("POST");

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Theory]
    [InlineData("none", 100)]
    [InlineData("percent-20", 80)]
    [InlineData("fixed-15", 85)]
    public void Strategy_AppliesEachDiscountCorrectly(string kind, decimal expected)
    {
        IDiscountStrategy strategy = kind switch
        {
            "percent-20" => new PercentageDiscountStrategy(20),
            "fixed-15" => new FixedAmountDiscountStrategy(15),
            _ => new NoDiscountStrategy(),
        };

        Assert.Equal(expected, strategy.Apply(100m));
    }

    [Fact]
    public async Task Decorator_CachesSoInnerServiceIsCalledOnce()
    {
        var inner = new CountingDataService();
        IDataService service = new CachingDataService(inner);

        var first = await service.GetDataAsync("order-123");
        var second = await service.GetDataAsync("order-123");

        Assert.Equal(first, second);
        Assert.Equal(1, inner.CallCount);
    }

    [Fact]
    public void Observer_NotifiesSubscriber()
    {
        var ticker = new StockTicker();
        var received = new List<(string Symbol, decimal Price)>();

        ticker.Subscribe((symbol, price) => received.Add((symbol, price)));
        ticker.SetPrice("MSFT", 150m);

        Assert.Single(received);
        Assert.Equal(("MSFT", 150m), received[0]);
    }

    [Fact]
    public void Observer_DoesNotNotifyAfterUnsubscribe()
    {
        var ticker = new StockTicker();
        var count = 0;

        void handler(string symbol, decimal price) => count++;

        ticker.Subscribe(handler);
        ticker.Unsubscribe(handler);
        ticker.SetPrice("MSFT", 150m);

        Assert.Equal(0, count);
    }

    [Fact]
    public void Command_UndoRestoresLightState()
    {
        var remote = new RemoteControl();

        remote.PressOn();
        Assert.True(remote.IsLightOn);

        remote.PressOff();
        Assert.False(remote.IsLightOn);

        remote.UndoLast();
        Assert.True(remote.IsLightOn);

        remote.UndoLast();
        Assert.False(remote.IsLightOn);
    }

    [Fact]
    public void Factory_CreatesCorrectProcessorPerOrderType()
    {
        OrderProcessor online = new OnlineOrderProcessor();
        OrderProcessor subscription = new SubscriptionOrderProcessor();

        Assert.IsType<CreditCardProcessor>(online.CreateProcessor());
        Assert.IsType<PayPalProcessor>(subscription.CreateProcessor());
    }

    private sealed class CountingDataService : IDataService
    {
        public int CallCount { get; private set; }

        public Task<string> GetDataAsync(string key)
        {
            CallCount++;
            return Task.FromResult($"Data for {key}");
        }
    }
}
