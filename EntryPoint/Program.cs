using DIContainer;
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

        using (var scope = new Scope())
        {
            Console.WriteLine("Creating scope...");

            var program = container.GetService<MyProgram>();
            program.Run();

            Console.WriteLine("Scope is about to be disposed...");
        }

        Console.WriteLine("Scope has been disposed.");
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