using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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