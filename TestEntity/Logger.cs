
public class Logger : IEntity
{
    private Counter counter;

    public Logger(Counter counter)
    {
        this.counter = counter;
    }

    public void Log(string message)
    {
        Console.WriteLine($"{counter.GetNextNumber()}> {GetType()} {message}");
    }
}