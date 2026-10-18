using DesignPatterns.Patterns.Behavioral;
using DesignPatterns.Patterns.Creational;
using DesignPatterns.Patterns.Structural;

Console.WriteLine("=== Singleton ===");
AppSettings.Demo();

Console.WriteLine();
Console.WriteLine("=== Factory Method ===");
PaymentProcessorDemo.Demo();

Console.WriteLine();
Console.WriteLine("=== Builder ===");
HttpRequestBuilder.Demo();

Console.WriteLine();
Console.WriteLine("=== Adapter ===");
LegacyLoggerAdapter.Demo();

Console.WriteLine();
Console.WriteLine("=== Decorator ===");
await CachingDataService.Demo();

Console.WriteLine();
Console.WriteLine("=== Facade ===");
OrderFacade.Demo();

Console.WriteLine();
Console.WriteLine("=== Strategy ===");
PriceCalculator.Demo();

Console.WriteLine();
Console.WriteLine("=== Observer ===");
StockTicker.Demo();

Console.WriteLine();
Console.WriteLine("=== Command ===");
RemoteControl.Demo();
