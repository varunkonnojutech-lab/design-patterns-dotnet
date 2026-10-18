namespace DesignPatterns.Patterns.Behavioral;

/// <summary>
/// Command: button presses are encapsulated as objects on an undo stack,
/// so every action can be reversed in LIFO order.
/// </summary>
public interface ICommand
{
    void Execute();
    void Undo();
}

public sealed class Light
{
    public bool IsOn { get; private set; }

    public void TurnOn() => IsOn = true;

    public void TurnOff() => IsOn = false;
}

public sealed class LightOnCommand : ICommand
{
    private readonly Light _light;

    public LightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute() => _light.TurnOn();

    public void Undo() => _light.TurnOff();
}

public sealed class LightOffCommand : ICommand
{
    private readonly Light _light;

    public LightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute() => _light.TurnOff();

    public void Undo() => _light.TurnOn();
}

public sealed class RemoteControl
{
    private readonly Light _light = new();
    private readonly Stack<ICommand> _history = new();

    public void PressOn()
    {
        var command = new LightOnCommand(_light);
        command.Execute();
        _history.Push(command);
    }

    public void PressOff()
    {
        var command = new LightOffCommand(_light);
        command.Execute();
        _history.Push(command);
    }

    public void UndoLast()
    {
        if (_history.TryPop(out var command))
        {
            command.Undo();
        }
    }

    public bool IsLightOn => _light.IsOn;

    public static void Demo()
    {
        var remote = new RemoteControl();

        remote.PressOn();
        Console.WriteLine($"Light on? {remote.IsLightOn}");

        remote.PressOff();
        Console.WriteLine($"Light on? {remote.IsLightOn}");

        remote.UndoLast();
        Console.WriteLine($"After undo: Light on? {remote.IsLightOn}");

        remote.UndoLast();
        Console.WriteLine($"After second undo: Light on? {remote.IsLightOn}");
    }
}
