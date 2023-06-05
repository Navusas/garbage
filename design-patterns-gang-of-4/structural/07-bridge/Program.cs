var x = new RefinedAbstraction(new ConcreteB());
x.Operation();

interface IAbstraction
{
    public void Operation();
}

class RefinedAbstraction : IAbstraction
{
    private readonly IImplementor _imp;

    public RefinedAbstraction(IImplementor imp)
    {
        _imp = imp;
    }
    public void Operation()
    {
        _imp.OperationImp();
    }
}

internal interface IImplementor
{
    public void OperationImp();
}

class ConcreteA : IImplementor
{
    public void OperationImp()
    {
        Console.WriteLine("concreteA");
    }
}

class ConcreteB : IImplementor
{
    public void OperationImp()
    {
        Console.WriteLine("concreteB");
    }
}