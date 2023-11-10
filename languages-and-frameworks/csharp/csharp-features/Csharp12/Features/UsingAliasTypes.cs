namespace Csharp12;

/*
Title:          Using Alias Type
Description:    Allow using alias directive to reference any kind of Type
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-12.0/using-alias-types.md
*/

// Prior to C# 12, we were limited to named types. 
// tuples, pointer types, array types, etc were not supported.
using StringDictionary = Dictionary<string, string>;

// In C# 12, you can do: 
using Point = (int x, int y);

public class UsingAliasType
{
    private ConsoleWriter consoleWriter = new ("C# 12 - UsingAliasType");

    /// <summary>
    /// In earlier versions of C#, you could only use aliasing for named types and namespaces. 
    /// This meant that for complex types like tuples, you would have to use their full definition each time.
    /// </summary>
    public void DemonstrateBefore() 
    {
        (int x, int y) point = (5, 2);
        consoleWriter.Write($"Before: Point coordinates: {point.x}, {point.y}");
    }

    /// <summary>
    /// You can now create aliases for any type, including tuples
    /// </summary>
    public void DemonstrateAfter() 
    {
        Point point = (5, 2);
        consoleWriter.Write($"After: Point coordinates: {point.x}, {point.y}");
    }
}