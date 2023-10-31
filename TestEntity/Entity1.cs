
public class Entity1 : IEntity
{
    private Counter counter;

    public Entity1(Counter counter)
    {
        this.counter = counter;
    }

    public async Task WakeUp(string message)
    {
        Log(message);
        await Task.Yield();
    }

    private void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{counter.GetNextNumber()}> {GetType()} {message}");
        Console.ResetColor();
    }
}