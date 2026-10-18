namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Strategy: <see cref="PriceCalculator"/> delegates the discount algorithm to a
/// swappable <see cref="IDiscountStrategy"/>.
/// </summary>
public interface IDiscountStrategy
{
    decimal Apply(decimal price);
}

public sealed class NoDiscountStrategy : IDiscountStrategy
{
    public decimal Apply(decimal price) => price;
}

public sealed class PercentageDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _percent;

    public PercentageDiscountStrategy(decimal percent)
    {
        _percent = percent;
    }

    public decimal Apply(decimal price) => price * (1 - _percent / 100m);
}

public sealed class FixedAmountDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _amount;

    public FixedAmountDiscountStrategy(decimal amount)
    {
        _amount = amount;
    }

    public decimal Apply(decimal price) => Math.Max(0, price - _amount);
}

public sealed class PriceCalculator
{
    public IDiscountStrategy Strategy { get; set; } = new NoDiscountStrategy();

    public decimal Calculate(decimal price) => Strategy.Apply(price);

    public static void Demo()
    {
        const decimal price = 100m;
        var calculator = new PriceCalculator();

        Console.WriteLine($"No discount: {calculator.Calculate(price):C}");

        calculator.Strategy = new PercentageDiscountStrategy(20);
        Console.WriteLine($"20% off: {calculator.Calculate(price):C}");

        calculator.Strategy = new FixedAmountDiscountStrategy(15);
        Console.WriteLine($"$15 off: {calculator.Calculate(price):C}");
    }
}
