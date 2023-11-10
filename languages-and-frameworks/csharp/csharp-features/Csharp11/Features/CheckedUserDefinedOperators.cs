namespace Csharp11;


/*
Title:          Checked User Defined Operators
Description:    TBD
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-11.0/static-abstracts-in-interfaces.md


Supported operators:
  ++, --, - unary operators
  +, -, *, / binary operators
  explicit conversion operators​


checked operator - throws an exception if the result is outside the range of the type
unchecked operator - return truncated result if the result is outside the range of the type
*/
public class CheckedUserDefinedOperators
{
    /// <summary>
    /// No way write a type which supported both - checked and unchecked - operators.
    /// You would have to write 2 different structs / types.
    /// It made it difficult to port some algorithms (like high-precision financial calculations),
    /// or expose user-defined types like Int128 or UInt128
    /// </summary>
    public void DemonstrateBefore()
    {
        Int128Before num1 = new (1, ulong.MaxValue); 
        Int128Before num2 = new (2, 1);

        // Unchecked addition
        Int128Before resultUnchecked = num1 + num2;
        Console.WriteLine($"Unchecked addition result: High = {resultUnchecked.High}, Low = {resultUnchecked.Low}");

        try
        {
            // Checked addition
            Int128Before resultChecked = checked(num1 + num2);
            Console.WriteLine($"Checked addition result: High = {resultChecked.High}, Low = {resultChecked.Low}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Overflow occurred: {ex.Message}");
        }
    }

    public void DemonstrateAfter()
    {
        Int128 num1 = new (1, ulong.MaxValue); 
        Int128 num2 = new (2, 1);

        // Unchecked addition
        Int128 resultUnchecked = num1 + num2;
        Console.WriteLine($"Unchecked addition result: High = {resultUnchecked.High}, Low = {resultUnchecked.Low}");

        try
        {
            // Checked addition
            Int128 resultChecked = checked(num1 + num2);
            Console.WriteLine($"Checked addition result: High = {resultChecked.High}, Low = {resultChecked.Low}");
        }
        catch (OverflowException ex)
        {
            Console.WriteLine($"Overflow occurred: {ex.Message}");
        }
    }
}



public struct Int128(long high, ulong low)
{
    public long High = high;
    public ulong Low = low;

    public static Int128 operator checked +(Int128 lhs, Int128 rhs)
    {
        long high = lhs.High + rhs.High;
        ulong low = lhs.Low + rhs.Low;
        if (low < lhs.Low || (high == long.MaxValue && low == ulong.MaxValue && (lhs.High < 0 || rhs.High < 0)))
        {
            throw new OverflowException("Overflow in checked addition.");
        }
        return new Int128(high, low);
    }

    public static Int128 operator +(Int128 lhs, Int128 rhs)
    {
        long high = lhs.High + rhs.High;
        ulong low = lhs.Low + rhs.Low;
        return new Int128(high, low);
    }
}


public struct Int128Before(long high, ulong low)
{
    public long High = high;
    public ulong Low = low;

    public static Int128Before operator +(Int128Before lhs, Int128Before rhs)
    {
        long high = lhs.High + rhs.High;
        ulong low = lhs.Low + rhs.Low;

        if (low < lhs.Low || (high == long.MaxValue && low == ulong.MaxValue && (lhs.High < 0 || rhs.High < 0)))
        {
            throw new OverflowException("Overflow in addition.");
        }

        return new Int128Before(high, low);
    }
}