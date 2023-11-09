namespace Csharp10;

/*
Title:          Constant Interpolated Strings
Description:    Instead of using + to concatenate strings, use string interpolation 
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-10.0/constant_interpolated_strings.md
*/
public class ConstantInterpolatedStrings
{
    private ConsoleWriter consoleWriter = new ("C# 10 - ConstantInterpolatedStrings");

    const string Greeting = "Greetings";
    const string EmployeeName = "John";
    const string WelcomingMessage = "Welcome to the team!";

    public void DemonstrateBefore()
    {
        const string Message = Greeting + ", " + EmployeeName + "! " + WelcomingMessage;
        consoleWriter.Write($"Before: '{Message}'");
    }

    public void DemonstrateAfter()
    {
        const string Message = $"{Greeting}, {EmployeeName}! {WelcomingMessage}";
        consoleWriter.Write($"After: '{Message}'");
    }
}