## Stack trace integrity in nested exception throwing

## Problem
Does using boolean flag help to maintain the integrity of the initial stack trace, when in nested try/catch block?
Or is just not having it at all would do the trick? 

Example:
```csharp
try 
{
    await someTaskWhichMayThrow(); 
}
catch 
{
    try
    {
        await someTaskWhichMayThrow(privileggedAccess = true);
    }
    catch
    {
        throwOriginalException = true;
    }

    if (throwOriginalException)
    {
        throw;
    }
}
```

## Hypothesis
Using boolean flag is better, because it preserves the original stack trace. 

## Results
It does indeed have a different stack trace. Seems like the way async exceptions are rethrown in .NET is not as easy as I iniaitially thought.
From what I understand, when the exception is caught, and then another task is awaited within that same catch statement, .NET just essentially starts a brand new stack trace and does not preserve the old stack trace. 

Here is a code with stack traces:
```csharp
namespace ExceptionStackTraceIntegrity;

public class OtherClass
{
    private readonly AnotherOtherClass _anotherOtherClass = new();

    /// <summary>
    /// StackTrace:
    ///     at ExceptionStackTraceIntegrity.AnotherOtherClass.ThrowArgumentException() in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/AnotherOtherClass.cs:line 7
    ///     at ExceptionStackTraceIntegrity.OtherClass.ChildCLassThrowsExceptionOutsideCatch() in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/OtherClass.cs:line 18
    ///     at ExceptionStackTraceIntegrity.OtherClass.ChildCLassThrowsExceptionOutsideCatch() in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/OtherClass.cs:line 35
    ///     at Program.<Main>$(String[] args) in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/Program.cs:line 11
    /// </summary>
    public async Task ChildCLassThrowsExceptionOutsideCatch()
    {
        try
        {
            await _anotherOtherClass.ThrowArgumentException(); // 2 & inner for 3
        }
        catch
        {

            var throwOriginalException = false;
            try
            {
                await _anotherOtherClass.ThrowArgumentException();
            }
            catch
            {
                throwOriginalException = true;
            }

            if (throwOriginalException)
            {
                throw;                 // 1
            }
        }

    }

   /// <summary>
   /// Stack Trace:
   ///      at ExceptionStackTraceIntegrity.AnotherOtherClass.ThrowArgumentException() in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/AnotherOtherClass.cs:line 7
   ///      at ExceptionStackTraceIntegrity.OtherClass.ChildCLassThrowsExceptionInCatch() in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/OtherClass.cs:line 55
   ///      at Program.<Main>$(String[] args) in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/Program.cs:line 25
   /// </summary>
    public async Task ChildCLassThrowsExceptionInCatch()
    {
        try
        {
            await _anotherOtherClass.ThrowArgumentException();
        }
        catch
        {
            await _anotherOtherClass.ThrowArgumentException(); // 1 & inner for 2
        }
    }


   /// <summary>
   /// Stack Trace:
   ///      [Essentially like option 2] 
   ///      at ExceptionStackTraceIntegrity.AnotherOtherClass.ThrowArgumentException() in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/AnotherOtherClass.cs:line 7
   ///      at ExceptionStackTraceIntegrity.OtherClass.<ChildCLassThrowsExceptionInInlineMethod>g__CallWithPrivillegedAccess|3_0() in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/OtherClass.cs:line 71
   ///      at ExceptionStackTraceIntegrity.OtherClass.ChildCLassThrowsExceptionInInlineMethod() in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/OtherClass.cs:line 79
   ///      at Program.<Main>$(String[] args) in /Users/domg/dev/repos/my/playground/languages-and-frameworks/csharp/ExceptionStackTraceIntegrity/ExceptionStackTraceIntegrity/Program.cs:line 38
   /// </summary>
    public async Task ChildCLassThrowsExceptionInInlineMethod()
    {
        async Task CallWithPrivillegedAccess()
        {
            await _anotherOtherClass.ThrowArgumentException(); // 2 & inner for 3
        }
        try
        {
            await _anotherOtherClass.ThrowArgumentException();
        }
        catch
        {
            await CallWithPrivillegedAccess(); // 1
        }

    }
}
```
