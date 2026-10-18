namespace DesignPatterns.Patterns.Structural;

/// <summary>
/// Facade: hides the coordination of inventory, payment, and shipping
/// behind a single <c>PlaceOrder</c> call.
/// </summary>
public sealed class InventoryService
{
    public bool Reserve(string sku, int qty)
    {
        Console.WriteLine($"  - Reserved {qty} x {sku} from inventory.");
        return true;
    }
}

public sealed class PaymentService
{
    public bool Charge(decimal amount)
    {
        Console.WriteLine($"  - Charged {amount:C}.");
        return true;
    }
}

public sealed class ShippingService
{
    public string Ship(string address)
    {
        var trackingId = "TRK-" + Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        Console.WriteLine($"  - Shipped to {address}. Tracking: {trackingId}");
        return trackingId;
    }
}

public sealed class OrderFacade
{
    private readonly InventoryService _inventory = new();
    private readonly PaymentService _payment = new();
    private readonly ShippingService _shipping = new();

    /// <summary>Returns the tracking ID, or null when the order cannot be placed.</summary>
    public string? PlaceOrder(string sku, int qty, decimal amount, string address)
    {
        if (!_inventory.Reserve(sku, qty))
        {
            Console.WriteLine("Order failed: could not reserve inventory.");
            return null;
        }

        if (!_payment.Charge(amount))
        {
            Console.WriteLine("Order failed: payment declined.");
            return null;
        }

        var trackingId = _shipping.Ship(address);
        Console.WriteLine($"Order placed. Tracking ID: {trackingId}");
        return trackingId;
    }

    public static void Demo()
    {
        new OrderFacade().PlaceOrder("ABC-123", 2, 49.98m, "123 Main St, New York, NY");
    }
}
