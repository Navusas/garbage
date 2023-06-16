Console.WriteLine("15-interpreter");

const string roman = "MCMXXVIII";
var context = new Context(roman);
// Build the 'parse tree'
var tree = new List<Expression>
{
    new ThousandExpression(),
    new HundredExpression(),
    new TenExpression(),
    new OneExpression()
};
// Interpret
foreach (var exp in tree)
{
    exp.Interpret(context);
}

Console.WriteLine("{0} = {1}", roman, context.Output);


/// <summary>
/// The 'Context' class
/// </summary>
public class Context
{
    public string Input { get; set; }
    public int Output { get; set; }

    // Constructor
    public Context(string input)
    {
        Input = input;
    }
}

/// <summary>
/// The 'AbstractExpression' class
/// </summary>
public abstract class Expression
{
    public void Interpret(Context context)
    {
        if (context.Input.Length == 0)
            return;
        if (context.Input.StartsWith(Nine()))
        {
            context.Output += (9 * Multiplier());
            context.Input = context.Input.Substring(2);
        }
        else if (context.Input.StartsWith(Four()))
        {
            context.Output += (4 * Multiplier());
            context.Input = context.Input.Substring(2);
        }
        else if (context.Input.StartsWith(Five()))
        {
            context.Output += (5 * Multiplier());
            context.Input = context.Input.Substring(1);
        }

        while (context.Input.StartsWith(One()))
        {
            context.Output += (1 * Multiplier());
            context.Input = context.Input.Substring(1);
        }
    }

    protected abstract string One();
    protected abstract string Four();
    protected abstract string Five();
    protected abstract string Nine();
    protected abstract int Multiplier();
}

/// <summary>
/// A 'TerminalExpression' class
/// <remarks>
/// Thousand checks for the Roman Numeral M 
/// </remarks>
/// </summary>
public class ThousandExpression : Expression
{
    protected override string One() => "M";
    protected override string Four() => " ";
    protected override string Five() =>  " ";
    protected override string Nine() => " ";
    protected override int Multiplier() => 1000;
}

/// <summary>
/// A 'TerminalExpression' class
/// <remarks>
/// Hundred checks C, CD, D or CM
/// </remarks>
/// </summary>
public class HundredExpression : Expression
{
    protected override string One() => "C";
    protected override string Four() => "CD";
    protected override string Five() =>  "D";
    protected override string Nine() => "CM";
    protected override int Multiplier() => 100;
}

/// <summary>
/// A 'TerminalExpression' class
/// <remarks>
/// Ten checks for X, XL, L and XC
/// </remarks>
/// </summary>
public class TenExpression : Expression
{
    protected override string One() => "X";
    protected override string Four() => "XL";
    protected override string Five() =>  "L";
    protected override string Nine() => "XC";
    protected override int Multiplier() => 10;
}

/// <summary>
/// A 'TerminalExpression' class
/// <remarks>
/// One checks for I, II, III, IV, V, VI, VI, VII, VIII, IX
/// </remarks>
/// </summary>
public class OneExpression : Expression
{
    protected override string One() => "I";
    protected override string Four() => "IV";
    protected override string Five() =>  "V";
    protected override string Nine() => "IX";
    protected override int Multiplier() => 1;
}