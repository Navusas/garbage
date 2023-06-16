namespace DesignPatterns.Behavioral.Iterator;

public static class SimpleExample
{
    public static void Run()
    {
        var collection = new ConcreteCollection
        {
            [0] = "Item A",
            [1] = "Item B",
            [2] = "Item C",
            [3] = "Item D"
        };

        var iterator = collection.CreateIterator();

        Console.WriteLine("Iterating over collection:");

        for (; iterator.HasNext(); iterator.Next())
        {
            var item = iterator.Current();
            Console.WriteLine(item);
        }
    }
}
/// <summary>
/// Declare an operations requi `red for traversing a collection
/// </summary>
internal interface Iterator
{
    bool HasNext();
    object Current();
    void Next();
}

/// <summary>
/// Declares one or multiple methods for getting iterators compatible with the collection
/// </summary>
interface IterableCollection
{
    Iterator CreateIterator();
}

/// <summary>
/// Return new instances of a particular concrete iterator class each time
/// the client requests one
/// </summary>
class ConcreteCollection : IterableCollection
{
    private readonly List<object> _items = new List<object>();

    public Iterator CreateIterator()
    {
        return new ConcreteIterator(this);
    }

    public int Count => _items.Count;

    public object this[int index]
    {
        get => _items[index];
        set => _items.Insert(index, value);
    }
}
/// <summary>
/// Implementation of specific algorithm for traversing the collection
/// </summary>
class ConcreteIterator : Iterator
{
    private readonly ConcreteCollection _collection;
    private int _current = 0;
    private int _step = 1;

    public ConcreteIterator(ConcreteCollection collection)
    {
        _collection = collection;
    }

    public object Current()
    {
        return _collection[_current];
    }

    public bool HasNext()
    {
        return _current < _collection.Count;
    }

    public void Next()
    {
        _current += _step;
    }
}