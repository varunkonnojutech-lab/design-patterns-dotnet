namespace DesignPatterns.Patterns.Creational;

/// <summary>
/// Factory Method: the order types decide which payment processor gets created.
/// New order types (and new processors) can be added without touching existing code.
/// </summary>
public interface IPaymentProcessor
{
    string Name { get; }
    string ProcessPayment(decimal amount);
}

public sealed class CreditCardProcessor : IPaymentProcessor
{
    public string Name => "Credit Card";

    public string ProcessPayment(decimal amount) => $"Charged {amount:C} to credit card.";
}

public sealed class PayPalProcessor : IPaymentProcessor
{
    public string Name => "PayPal";

    public string ProcessPayment(decimal amount) => $"Charged {amount:C} via PayPal.";
}

public abstract class OrderProcessor
{
    /// <summary>The factory method: subclasses choose the concrete processor.</summary>
    public abstract IPaymentProcessor CreateProcessor();

    public string Checkout(decimal amount)
    {
        var processor = CreateProcessor();
        return $"[{processor.Name}] {processor.ProcessPayment(amount)}";
    }
}

public sealed class OnlineOrderProcessor : OrderProcessor
{
    public override IPaymentProcessor CreateProcessor() => new CreditCardProcessor();
}

public sealed class SubscriptionOrderProcessor : OrderProcessor
{
    public override IPaymentProcessor CreateProcessor() => new PayPalProcessor();
}

public static class PaymentProcessorDemo
{
    public static void Demo()
    {
        OrderProcessor online = new OnlineOrderProcessor();
        OrderProcessor subscription = new SubscriptionOrderProcessor();

        Console.WriteLine(online.Checkout(99.99m));
        Console.WriteLine(subscription.Checkout(19.99m));
    }
}
