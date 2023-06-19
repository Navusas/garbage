
public static class RealLifeExample
{
    public static void Run()
    {

        var FormRequest = new FormRequest(7000, 555, 2, 1, 2, 45000, 1200);
        var processor = new BankRequestsProcessor(new AcceptForCreditCard());
        var response = processor.Process(FormRequest);
        Console.WriteLine(response.Message);

        processor = new BankRequestsProcessor(new AcceptForLoanStrategy());
        response = processor.Process(FormRequest);
        Console.WriteLine(response.Message);

    }
}

record FormRequest(int AskingAmount, int CreditScore, int YearsAtAddress, int YearsAtBank, int YearsEmployed, int Salary, int MonthlyLivingExpenses);
record FormResponse(bool IsAccepted, string Message, int AmountIfDifferent = 0, float MonthlyPayment = 0.0f);

class BankRequestsProcessor {
    private readonly IUserDefaultStrategy _strategy;

    public BankRequestsProcessor(IUserDefaultStrategy strategy) {
        _strategy = strategy;
    }
    
    public FormResponse Process(FormRequest request) {
        if (_strategy.CanIssue(request.CreditScore, request.YearsAtAddress, request.YearsAtBank, request.YearsEmployed, request.Salary, request.MonthlyLivingExpenses)) {
            var maxAmount = _strategy.GetMaxAmount(request.AskingAmount, request.CreditScore, request.Salary, request.MonthlyLivingExpenses);
            if (maxAmount >= request.AskingAmount) {
                return new FormResponse(true, "Request accepted, amount requested: " + request.AskingAmount + ", amount approved: " + maxAmount + ", monthly payment: " + (maxAmount / 12));
            }
            else {
                return new FormResponse(true, "Request accepted, but amount is different", maxAmount);
            }
        }
        else {
            return new FormResponse(false, "Request rejected");
        }
    }
}

interface IUserDefaultStrategy {
    bool CanIssue(int creditScore, int yearsAtAddress, int yearsAtBank, int yearsEmployed, int salary, int monthlyLivingExpenses);
    int GetMaxAmount(int requestedAmount, int creditScore, int salary, int monthlyLivingExpenses);
}

class AcceptForLoanStrategy : IUserDefaultStrategy
{
    public bool CanIssue(int creditScore, int yearsAtAddress, int yearsAtBank, int yearsEmployed, int salary, int monthlyLivingExpenses)
    {
        Console.WriteLine("AcceptForLoanStrategy");
        Console.WriteLine($"Required criteria: creditScore > 600 && yearsAtAddress > 2 && yearsAtBank > 2 && yearsEmployed > 2");
        if(creditScore >= 600 && yearsAtAddress >= 2 && yearsAtBank >= 2 && yearsEmployed >= 2) {
            Console.WriteLine("Loan accepted");
            return true;
        }
        else {
            Console.WriteLine("Loan rejected");
            Console.WriteLine($"creditScore: {creditScore} >= 600, yearsAtAddress: {yearsAtAddress} >= 2, yearsAtBank: {yearsAtBank} >= 2, yearsEmployed: {yearsEmployed} >= 2");
            return false;
        }
    }

    public int GetMaxAmount(int requestedAmount, int creditScore, int salary, int monthlyLivingExpenses)
    {
        Console.WriteLine("GetMaxAmount");
        int multiplier = salary / monthlyLivingExpenses * 12 / 1000;
        
        if(creditScore > 700) {
            Console.WriteLine($"creditScore > 700, multiplier *= 2, current multiplier: {multiplier}");
            multiplier *= 2;
        }

        Console.WriteLine($"Max amount: {multiplier} * {requestedAmount} = {multiplier * requestedAmount}");
        return multiplier * requestedAmount;
    }
}

class AcceptForCreditCard : IUserDefaultStrategy
{
    public bool CanIssue(int creditScore, int yearsAtAddress, int yearsAtBank, int yearsEmployed, int salary, int monthlyLivingExpenses)
    {
        Console.WriteLine("AcceptForCreditCard: CanIssue");
        Console.WriteLine($"AcceptForCreditCard: Required criteria: creditScore > 500 && yearsAtAddress > 1 && yearsAtBank > 1 && yearsEmployed > 1");
        if(creditScore >= 500 && yearsAtAddress >= 1 && yearsAtBank >= 1 && yearsEmployed >= 1) {
            Console.WriteLine("AcceptForCreditCard: Credit card accepted");
            return true;
        }
        else {
            Console.WriteLine("AcceptForCreditCard: Credit card rejected");
            Console.WriteLine($"AcceptForCreditCard: creditScore: {creditScore} >= 500, yearsAtAddress: {yearsAtAddress} >= 1, yearsAtBank: {yearsAtBank} >= 1, yearsEmployed: {yearsEmployed} >= 1");
            return false;
        }
    }

    public int GetMaxAmount(int requestedAmount, int creditScore, int salary, int monthlyLivingExpenses)
    {
        Console.WriteLine("AcceptForCreditCard: GetMaxAmount");
        const int defaultCreditCardLimit = 1500;
        var limitGiven = defaultCreditCardLimit;

        if(creditScore < 600) {
            limitGiven -= 500;
            Console.WriteLine($"AcceptForCreditCard: creditScore < 600, limitGiven -= 500, current limitGiven: {limitGiven}");
        }
        if(creditScore > 700) {
            limitGiven += 500;
            Console.WriteLine($"AcceptForCreditCard: creditScore > 700, limitGiven += 500, current limitGiven: {limitGiven}");
        }

        if(salary - (monthlyLivingExpenses * 12) > 40000) {
            limitGiven += 2000;
            Console.WriteLine($"AcceptForCreditCard: salary - (monthlyLivingExpenses * 12) > 40000, limitGiven += 500, current limitGiven: {limitGiven}");
        }

        Console.WriteLine($"AcceptForCreditCard: Max amount: {limitGiven}");
        return limitGiven;
    }
}
