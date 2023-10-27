
public class Logger2 : IEntity
{
    private Counter counter;
    public Logger _logger { get; set; }

    public Logger2(Counter counter, Logger logger)
    {
        this.counter = counter;
        _logger = logger;
    }

    public void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        if (_logger is not null)
            Console.WriteLine($"{counter.GetNextNumber()}> {GetType()} {message}");
        else
            Console.WriteLine("Reference for Logger = null");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
