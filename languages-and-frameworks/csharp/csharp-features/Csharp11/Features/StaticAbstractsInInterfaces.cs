namespace Csharp11;


/*
Title:          Static Abstract In Interfaces
Description:    Allow interfaces to declare static abstract members
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-11.0/static-abstracts-in-interfaces.md
*/
public class StaticAbstractsInInterfaces
{
    public void DemonstrateBefore()
    {
        // Rely on some sort of non-generic static methods / classes 
    }

    public void DemonstrateAfter()
    {
        int result = IntAddable.Add(5, 10);
        Console.WriteLine($"After: {result}");
    }
}


public interface IAddable<T>
{
    static abstract T Add(T a, T b);
}

// Implement the interface in a class
public class IntAddable : IAddable<int>
{
    public static int Add(int a, int b) => a + b;
}