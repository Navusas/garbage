namespace DesignPatterns.Behavioral.Strategy;

/// <summary>
/// This is more of a Java-like style, where you have HasNext() and MoveNext() 
/// </summary>
public static class SimpleExample
{
    public static void Run()
    {
        var composition = new Composition(new SimpleCompositor());
        composition.Repair();

        composition = new Composition(new TeXCompositor());
        composition.Repair();

        composition = new Composition(new ArrayCompositor(100));
        composition.Repair();
    }
}

class Composition
{
    private readonly Compositor _compositor;
    private List<Component> _components = new(); // represents text and graphical elements in a document
    private int _lineWidth;

    public Composition(Compositor compositor)
    {
        _compositor = compositor;
    }

    public void Repair()
    {
        _lineWidth = 100;
        int[] natural = new int[100];
        int[] stretchability = new int[100];
        int[] shrinkability = new int[100];
        int[] breakPenalty = new int[100];

        // determine where the breaks are
        var breakCount = _compositor.Compose(natural, stretchability, shrinkability, _components.Count, _lineWidth, breakPenalty);

        // lay out components according to breaks
    }
}

abstract class Compositor
{
    public abstract int Compose(int[] natural, int[] stretch, int[] shrink, int componentCount, int lineWidth, int[] breaks);
}

class SimpleCompositor : Compositor
{
    public override int Compose(int[] natural, int[] stretch, int[] shrink, int componentCount, int lineWidth, int[] breaks)
    {
        Console.WriteLine("SimpleCompositor");
        return 0;
    }
}

class TeXCompositor : Compositor
{
    public override int Compose(int[] natural, int[] stretch, int[] shrink, int componentCount, int lineWidth, int[] breaks)
    {
        Console.WriteLine("TeXCompositor");
        return 0;
    }
}

class ArrayCompositor : Compositor
{
    private int _interval;

    public ArrayCompositor(int interval)
    {
        _interval = interval;
    }

    public override int Compose(int[] natural, int[] stretch, int[] shrink, int componentCount, int lineWidth, int[] breaks)
    {
        Console.WriteLine("ArrayCompositor");
        return 0;
    }
}

class Component
{
}
