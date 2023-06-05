Console.WriteLine("09-Decorator");


var mortgage = new Mortgage();
// Evaluate mortgage eligibility for customer
var customer = new Customer("Ann McKinsey");
var eligible = mortgage.IsEligible(customer, 125000);

internal class Bank
{
    public bool HasSufficientSavings(Customer c, int amount)
    {
        Console.WriteLine("Check bank for " + c.Name);
        return true;
    }
}

internal class Credit
{
    public bool HasGoodCredit(Customer c)
    {
        Console.WriteLine("Check credit for " + c.Name);
        return true;
    }
}

internal class Loan
{
    public bool HasNoBadLoans(Customer c)
    {
        Console.WriteLine("Check loans for " + c.Name);
        return true;
    }
}

internal class Customer
{
    // Constructor
    public Customer(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

/// <summary>
///     The 'Facade' class
/// </summary>
internal class Mortgage
{
    private readonly Bank _bank = new();
    private readonly Credit _credit = new();
    private readonly Loan _loan = new();

    public bool IsEligible(Customer cust, int amount)
    {
        Console.WriteLine("{0} applies for {1:C} loan\n",
            cust.Name, amount);
        var eligible = true;
        // Check creditworthyness of applicant
        if (!_bank.HasSufficientSavings(cust, amount))
            eligible = false;
        else if (!_loan.HasNoBadLoans(cust))
            eligible = false;
        else if (!_credit.HasGoodCredit(cust)) eligible = false;
        return eligible;
    }
}