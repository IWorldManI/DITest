#undef DEBUG_MODE

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

#if DEBUG_MODE
        #region SimpleScopeTest
        using (var scope = new Scope())
        {
            Console.WriteLine($"\u001b[36m[DEBUGSCOPE]\u001b[0m " + "Creating scope...");

            var program = container.GetService<MyProgram>();
            program.Run();

            Console.WriteLine($"\u001b[36m[DEBUGSCOPE]\u001b[0m " + "Scope is about to be disposed...");
        }

        Console.WriteLine($"\u001b[36m[DEBUGSCOPE]\u001b[0m " + "Scope has been disposed.");
        #endregion
#endif
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