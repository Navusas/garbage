namespace Csharp12;

/*
Title:          Using Alias Type
Description:    Allow using alias directive to reference any kind of Type
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-12.0/using-alias-types.md
*/

public class UsingAliasType
{
    public void DemonstrateBefore() => new UsingAliasType();
    public void DemonstrateAfter() 
    {
        ConsoleWriter.Write("RunAfter started");

        using Point point = new (5, 2);

        ConsoleWriter.Write("RunAfter ended");
    }
}

public record struct Point(int X, int Y) : IDisposable
{
    public readonly ConsoleWriter ConsoleWriter = new("Using Alias Type");

    public void Dispose() {
        ConsoleWriter.Write($"{ToString()} disposed");
    }
    public override string ToString() => $"Point(X: {X}, Y: {Y})";
}