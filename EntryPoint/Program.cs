using DIContainer;

/// <summary>
/// Entry point of the application that sets up dependency injection and runs the program.
/// </summary>
internal class Program
{
    static void Main(string[] args)
    {
        Container container = new Container();
        container.AddSingleton<Counter>();
        container.AddTransient<IEntity, Entity1>();
        container.AddTransient<IEntity, Entity2>();
        container.AddTransient<IEntity, Semaphore>();

        container.AddEntryPoint<MyProgram>(nameof(MyProgram.Run));

        container.Run();
    }
}


public class MyProgram
{
    /// <summary>
    /// Main program class that orchestrates the execution of various IEntity implementations.
    /// </summary>
    private IEntity[] entity { get; set; }
    public MyProgram(IEntity[] entities)
    {
        this.entity = entities;
    }

    public void Run()
    {
        foreach (var entity in entity)
        {
            entity.WakeUp($"Started");
        }
    }
}