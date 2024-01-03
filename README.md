# DIContainer
## Overview
This project is a simple Dependency Injection (DI) container implemented in C#. The container allows you to manage the lifetime and resolution of dependencies in your application through various injection types, including Transient, Singleton, and Scoped.

## Usage
To use the DI container, follow these steps:

1. **Instantiate the Container**:

```csharp
var container = new DIContainer.Container();
```

2.  Choose the lifetime of dependencies:
- **Transient**:
	Dependencies are created each time they are requested.
- **Singleton**:
    A single instance is created and reused for all requests.
- **Scoped**:
	A single instance is created and reused within a specific scope.

## Example 
Here's a simple example of how to use the container:
```csharp
using DIContainer;

internal class Program
{
    static void Main(string[] args)
    {
        Container container = new Container();
        container.AddSingleton<Counter>();

        container.AddEntryPoint<MyProgram>(nameof(MyProgram.Run));

        container.Run();
    }
}

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
```

## Contact
For any inquiries, please contact me at rudenko.r.i15@gmail.com