// See https://aka.ms/new-console-template for more information

using ExceptionStackTraceIntegrity;

var attempt = new OtherClass();


// In this case, we have boolean flag & do throw inside nested try/catch block
try
{
    await attempt.ChildCLassThrowsExceptionOutsideCatch();
}
catch (Exception ex)
{
    // Log the stack trace of the exception to the console
    Console.WriteLine(ex.StackTrace);
}

Console.WriteLine("-----------");


// In this case we are simply not having try/catch around the 2nd exception
try
{
    await attempt.ChildCLassThrowsExceptionInCatch();
}
catch (Exception ex)
{
    // Log the stack trace of the exception to the console
    Console.WriteLine(ex.StackTrace);
}

Console.WriteLine("-----------");

// In this case we are simply not having try/catch around the 2nd exception
try
{
    await attempt.ChildCLassThrowsExceptionInInlineMethod();
}
catch (Exception ex)
{
    // Log the stack trace of the exception to the console
    Console.WriteLine(ex.StackTrace);
}