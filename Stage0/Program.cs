using System;

namespace Stage0;

internal class Program
{
    static void Main(string[] args)
    {
        Greeting("Yair");
        Greeting("Doron");
        Console.ReadLine();
    }

    /// <summary>
    /// Displays a greeting message to the specified user.
    /// </summary>
    /// <remarks>This method writes the greeting message to the console. Ensure that the <paramref
    /// name="name"/> parameter  is not null or empty to avoid unexpected behavior.</remarks>
    /// <param name="name">The name of the user to include in the greeting. Cannot be null or empty.</param>
    private static void Greeting(string name)
    {
        Console.WriteLine($"Hello {name}");
    }
}