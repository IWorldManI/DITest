using static DITest.Output.CustomConsole;

/// <summary>
/// The Semaphore class, implementing the IEntity interface, is designed to simulate working with semaphores and threads.
/// </summary>
internal class Semaphore : IEntity
{
    private Counter counter;
    private SemaphoreSlim semaphore;
    private int CurrentThreadId => Thread.CurrentThread.ManagedThreadId;

    public Semaphore(Counter counter)
    {
        this.counter = counter;
        this.semaphore = new SemaphoreSlim(2);
    }

    /// <summary>
    /// Implementation of the WakeUp method from the IEntity interface, simulating the activation of an entity.
    /// </summary>
    public async Task WakeUp(string message)
    {
        Start(message).Wait();
        await Task.Yield();
    }

    /// <summary>
    /// Internal Start method that launches multiple tasks to simulate working with threads.
    /// </summary>
    private async Task Start(string message)
    {
        var tasks = new Task[5];
        for (int i = 0; i < 5; i++)
        {
            tasks[i] = Task.Run(Update);
        }

        await Log(message);

        await Task.WhenAll(tasks);

        LogToCustomConsole(counter.GetNextNumber(), ConsoleTypeEnum.Exit, CurrentThreadId);
    }

    /// <summary>
    /// Internal Update method simulating thread work.
    /// </summary>
    private async Task Update()
    {
        LogToCustomConsole(counter.GetNextNumber(),ConsoleTypeEnum.Debug, CurrentThreadId);

        await semaphore.WaitAsync();

        try
        {
            LogToCustomConsole(counter.GetNextNumber(),ConsoleTypeEnum.Busy, CurrentThreadId);

            await Task.Delay(2000);
        }
        finally
        {
            semaphore.Release();
            LogToCustomConsole(counter.GetNextNumber(), ConsoleTypeEnum.Released, CurrentThreadId);
        }

        await Task.Yield();
    }

    /// <summary>
    /// Internal Log method for logging messages.
    /// </summary>
    private async Task Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{counter.GetNextNumber()}> {GetType()} {message}");
        Console.ResetColor();

        await Task.Yield();
    }
}
