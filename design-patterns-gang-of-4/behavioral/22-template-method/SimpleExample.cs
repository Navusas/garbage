namespace DesignPatterns.Behavioral.TemplateMethod;

public static class SimpleExample
{
    public static void Run()
    {
        AbstractClass c = new ConcreteClassA();
        c.TemplateMethod();

        c = new ConcreteClassB();
        c.TemplateMethod();
    }
}

abstract class AbstractClass
{
    public void TemplateMethod()
    {
        PrimitiveOperation1();
        PrimitiveOperation2();
    }

    protected abstract void PrimitiveOperation1();
    protected abstract void PrimitiveOperation2();
}

class ConcreteClassA : AbstractClass
{
    protected override void PrimitiveOperation1()
    {
        Console.WriteLine("ConcreteClassA.PrimitiveOperation1()");
    }

    protected override void PrimitiveOperation2()
    {
        Console.WriteLine("ConcreteClassA.PrimitiveOperation2()");
    }
}

class ConcreteClassB : AbstractClass
{
    protected override void PrimitiveOperation1()
    {
        Console.WriteLine("ConcreteClassB.PrimitiveOperation1()");
    }

    protected override void PrimitiveOperation2()
    {
        Console.WriteLine("ConcreteClassB.PrimitiveOperation2()");
    }
}