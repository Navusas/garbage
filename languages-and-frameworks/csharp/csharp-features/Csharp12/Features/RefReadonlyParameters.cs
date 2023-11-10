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
    /// Prior to C# 12, you would have to rely on documentation to know if a method is modifying the passed parameter.
    /// 
    /// Example:
    /// This method prints value to the console.
    /// 
    /// !!! DO NOT MODIFY THE PASSED VALUE !!!
    /// 
    /// </summary>
    public void DemonstrateBefore(ref int readOnlyValue) 
    {
        // Passed by reference to avoid copying.
        // The 'in' keyword enforces that this method cannot modify the passed value.
        consoleWriter.Write($"Before: Value is {readOnlyValue}");

        // The following line would cause a compile-time error:
        // readOnlyValue = 10;
    }

    /// <summary>
    /// After C# 12 - using 'ref readonly' to pass a read-only reference
    /// 
    /// The idea is, that we can still use `ref` and don't break any existing code, but we can also enforce the compiler
    /// to prevent any modification to the passed value.
    /// </summary>
    public void DemonstrateAfter(ref readonly int readOnlyValue) 
    {
        // The compiler will now prevent any assignment to readOnlyValue,
        // ensuring that it's truly read-only.
        consoleWriter.Write($"After: Value is {readOnlyValue}");

        // The following line would cause a compile-time error:
        // readOnlyValue = 10;
    }


    /// <summary>
    /// In C# 7 they introduced `in` keywoard. 
    /// 
    /// It is used to specify that an argument is passed by reference and its intended to be read-only.
    /// 
    /// in vs ref readonly.
    /// in              -> used to pass read-only reference to a method. It can be used at both the method & the caller. It indicates that the method should not modify the passed parameter.
    /// ref readonly    -> only for method declaration. The api/caller/client doesn't care and thus won't break existing codebases. Using `readonly` helps to ensure immutability.
    /// </summary>
    public void BonusRound(in int readOnlyValue) 
    {
        // Passed by reference to avoid copying.
        // The 'in' keyword enforces that this method cannot modify the passed value.
        consoleWriter.Write($"Before: Value is {readOnlyValue}");

        // The following line would cause a compile-time error:
        // readOnlyValue = 10;
    }
}