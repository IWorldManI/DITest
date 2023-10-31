using DIContainer;
using static DITest.Output.CustomConsole;
using System.Diagnostics.Metrics;
using static System.Formats.Asn1.AsnWriter;

/// <summary>
/// Entry point of the application that sets up dependency injection and runs the program.
/// </summary>
internal class Program
{
    static void Main(string[] args)
    {
        Container container = new Container();
        container.AddSingleton<Counter>();
        container.AddScope<IEntity, Entity2>();
        container.AddTransient<IEntity, Entity1>();
        container.AddTransient<IEntity, Semaphore>();

        container.AddEntryPoint<MyProgram>(nameof(MyProgram.Run));

        container.Run();
    }
}

/// <summary>
/// Main program class that orchestrates the execution of various IEntity implementations.
/// </summary>
public class MyProgram
{
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