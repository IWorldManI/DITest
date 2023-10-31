
public class Entity2 : IEntity
{
    private Counter counter;
    public Entity1 _logger { get; set; }

    public Entity2(Counter counter, Entity1 logger)
    {
        this.counter = counter;
        _logger = logger;
    }

    public async Task WakeUp(string message)
    {
        Log(message);
        await Task.Yield();
    }

    private void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        if (_logger is not null) Console.WriteLine($"{counter.GetNextNumber()}> {GetType()}  {message}");
        else Console.WriteLine("Reference for Logger = null");
        Console.ResetColor();
    }
}
