using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Semaphore : IEntity
{
    private Counter counter;

    static SemaphoreSlim semaphore = new SemaphoreSlim(2); // max 2

    public Semaphore(Counter counter)
    {
        this.counter = counter;
    }

    private async Task Start()
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

    private async Task DoWork()
    {
        Console.WriteLine($"{counter.GetNextNumber()}> Поток {Thread.CurrentThread.ManagedThreadId} создан.");

        await semaphore.WaitAsync();

        try
        {
            Console.WriteLine($"{counter.GetNextNumber()}> Поток {Thread.CurrentThread.ManagedThreadId} получил доступ к ресурсу и начал работу.");

            await Task.Delay(2000);
        }
        finally
        {
            semaphore.Release();
            Console.WriteLine($"{counter.GetNextNumber()}> Поток {Thread.CurrentThread.ManagedThreadId} освободил ресурс.");
        }
    }

    public void Log(string message)
    {
        Console.WriteLine($"{counter.GetNextNumber()}> {GetType()} {message}");
        Start().Wait();
    }
}
