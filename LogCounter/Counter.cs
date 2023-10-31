
/// <summary>
/// A simple Counter class designed for counting and returning sequential numbers.
/// </summary>
public class Counter
{
    public int counter;
    public Counter()
    {
        counter = 0;
    }
    public int GetNextNumber() => counter++;
}