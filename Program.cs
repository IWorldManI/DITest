using DIContainer;

public class Counter
{
    public int counter;

    public Counter()
    {
        counter = 0;
    }
    public int GetNextNumber() => counter++;
}

public interface ILogger
{
    async Task Log(string message) { }
}

public class Logger : ILogger
{
    private Counter counter;

    public Logger(Counter counter)
    {
        this.counter = counter;
    }

    public async Task Log(string message)
    {
        Console.WriteLine(counter.GetNextNumber() + "> " + message);
    }
}
public class Logger2 : ILogger
{
    private Counter counter;
    public Logger _logger { get; set; }

    public Logger2(Counter counter, Logger logger)
    {
        this.counter = counter;
        _logger = logger;
    }


    public async Task Log(string message)
    {
        Console.WriteLine(counter.GetNextNumber() + "> " + message);
        Console.ForegroundColor = ConsoleColor.Blue;
        if (_logger is not null)
            Console.WriteLine(_logger.GetType());
        else
        {
            Console.WriteLine("None");
        }
    }
}
class SemaphoreTest : ILogger
{
    static SemaphoreSlim semaphore = new SemaphoreSlim(2); // max 2

    static async Task Start()
    {
        // test tasks
        var tasks = new Task[5];
        for (int i = 0; i < 5; i++)
        {
            tasks[i] = Task.Run(DoWork);
        }

        await Task.WhenAll(tasks);

        Console.WriteLine("Работа завершена.");
    }

    static async Task DoWork()
    {
        Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} создан.");

        await semaphore.WaitAsync();

        try
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} получил доступ к ресурсу и начал работу.");

            await Task.Delay(2000);
        }
        finally
        {
            semaphore.Release();
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} освободил ресурс.");
        }
    }

    public async Task Log(string message)
    {
        await Start();
    }
}

public class MyProgram
{
    private ILogger[] loggers { get; set; }
    public MyProgram(ILogger[] loggers)
    {
        this.loggers = loggers;
    }

    public async Task Run()
    {
        foreach (var logger in loggers)
        {
            logger.Log("Hello");
            logger.Log("World!");
        }
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        Container container = new Container();
        container.AddSingleton<Counter>();
        container.AddTransient<ILogger, Logger>();
        container.AddTransient<ILogger, Logger2>();
        container.AddTransient<ILogger, SemaphoreTest>();

        container.AddEntryPoint<MyProgram>(nameof(MyProgram.Run));

        container.Run();
    }
}