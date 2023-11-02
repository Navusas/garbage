namespace Csharp10;

public record ConsoleWriter(string Prefix)
{
    public void Write<T>(T message)
    {
        Console.WriteLine($"[{Prefix}]: {message}");
    }
}