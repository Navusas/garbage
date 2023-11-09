namespace Csharp12;

/*
Title:          Ref Readonly Parameters
Description:    Enforce compiler to prevent a method from modifying the passed readonly parameter
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-12.0/ref-readonly-parameters.md
*/

public class RefReadonlyParams
{
    private ConsoleWriter consoleWriter = new ("C# 12 - RefReadonlyParams");

    /// <summary>
    /// Prior to C# 12
    /// </summary>
    public void DemonstrateBefore(in int readOnlyValue) 
    {
        // Passed by reference to avoid copying.
        // The 'in' keyword enforces that this method cannot modify the passed value.
        consoleWriter.Write($"Before: Value is {readOnlyValue}");

        // The following line would cause a compile-time error:
        // readOnlyValue = 10;
    }

    /// <summary>
    /// After C# 12 - using 'ref readonly' to pass a read-only reference
    /// </summary>
    public void DemonstrateAfter(ref readonly int readOnlyValue) 
    {
        // The compiler will now prevent any assignment to readOnlyValue,
        // ensuring that it's truly read-only.
        consoleWriter.Write($"After: Value is {readOnlyValue}");

        // The following line would cause a compile-time error:
        // readOnlyValue = 10;
    }
}