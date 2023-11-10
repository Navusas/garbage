namespace Csharp11;


public static class StaticAbstractsInInterfacesDemo
{
    public static void Demonstrate()
    {
        var example = new StaticAbstractsInInterfaces();
        example.DemonstrateBefore();
        example.DemonstrateAfter();
    }
}

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
        int intResult = IntArithmetic.Add(5, 10);
        double doubleResult = DoubleArithmetic.Add(5.5, 10.5);
        Console.WriteLine($"[StaticAbstractsInInterfaces] After: Int result: {intResult}, Double result: {doubleResult}");
    
        var dodgeCoin = new DodgeCoinCurrency(5.5m); // .m -> decimal
        var dodgeCoinResult = DodgeCoin.Add(dodgeCoin, dodgeCoin);
        Console.WriteLine($"[StaticAbstractsInInterfaces] After: Your dodgecoin balance: {dodgeCoinResult.Value}");
    }
}




public interface ISimpleArithmetic<T>
{
    static abstract T Add(T a, T b);
    static abstract T Subtract(T a, T b);
    static abstract T Multiply(T a, T b);
    static abstract T Divide(T a, T b);
}

public class IntArithmetic : ISimpleArithmetic<int>
{
    public static int Add(int a, int b) => a + b;
    public static int Subtract(int a, int b) => a - b;
    public static int Multiply(int a, int b) => a * b;
    public static int Divide(int a, int b) => a / b;
}

public class DoubleArithmetic : ISimpleArithmetic<double>
{
    public static double Add(double a, double b) => a + b;
    public static double Subtract(double a, double b) => a - b;
    public static double Multiply(double a, double b) => a * b;
    public static double Divide(double a, double b) => a / b;
}



public struct DodgeCoinCurrency
{
    public decimal Value;

    public DodgeCoinCurrency(decimal value)
    {
        Value = value;
    }

    // Implementing arithmetic operations for DodgeCoinCurrency
    public static DodgeCoinCurrency operator +(DodgeCoinCurrency a, DodgeCoinCurrency b) => new (a.Value + b.Value);
    public static DodgeCoinCurrency operator -(DodgeCoinCurrency a, DodgeCoinCurrency b) => new (a.Value - b.Value);
    public static DodgeCoinCurrency operator *(DodgeCoinCurrency a, DodgeCoinCurrency b) => new (a.Value * b.Value);
    public static DodgeCoinCurrency operator /(DodgeCoinCurrency a, DodgeCoinCurrency b) => new (a.Value / b.Value);
}

public class DodgeCoin : ISimpleArithmetic<DodgeCoinCurrency>
{
    public static DodgeCoinCurrency Add(DodgeCoinCurrency a, DodgeCoinCurrency b) => a + b;
    public static DodgeCoinCurrency Subtract(DodgeCoinCurrency a, DodgeCoinCurrency b) => a - b;
    public static DodgeCoinCurrency Multiply(DodgeCoinCurrency a, DodgeCoinCurrency b) => a * b;
    public static DodgeCoinCurrency Divide(DodgeCoinCurrency a, DodgeCoinCurrency b) => a / b;
}