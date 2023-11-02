namespace Csharp10;

/*
Title:          Constant Interpolated Strings
Description:    Instead of using 
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-10.0/constant_interpolated_strings.md
*/
public class ConstantInterpolatedStrings
{
    const string Greeting = "Greetings";
    const string EmployeeName = "John";
    const string WelcomingMessage = "Welcome to the team!";

    public void DemonstrateBefore()
    {
        const string Message = Greeting + ", " + EmployeeName + "! " + WelcomingMessage;
        Console.WriteLine(Message);
    }

    public void DemonstrateAfter()
    {
        const string Message = $"{Greeting}, {EmployeeName}! {WelcomingMessage}";
        Console.WriteLine(Message);
    }
}