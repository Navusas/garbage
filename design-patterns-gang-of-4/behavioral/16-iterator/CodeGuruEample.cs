using System.Collections;

namespace DesignPatterns.Behavioral.Iterator;

/// <summary>
/// Things to note:
/// 1. The iterator doesn't have to have a full collection to work (when somebody calls MoveNext(), you can fetch a value, or run some code, etc)
/// 2. You would normally use C# built-in iterators instead of implementing your own (yield return)
/// </summary>
public static class StringTraversalExample
{
    public static void Run()
    {
        var collection = new WordsCollection();
        collection.AddItem("First");
        collection.AddItem("Second");
        collection.AddItem("Third");

        Console.WriteLine("Straight traversal:");

        foreach (var element in collection)
        {
            Console.WriteLine(element);
        }

        Console.WriteLine("\nReverse traversal:");

        collection.ReverseDirection();

        foreach (var element in collection)
        {
            Console.WriteLine(element);
        }
    }
}

abstract class Iterator2 : IEnumerator
{
    object IEnumerator.Current => Current();

    // Returns the key of the current element
    public abstract int Key();

    // Returns the current element
    public abstract object Current();

    // Move forward to next element
    public abstract bool MoveNext();

    // Rewinds the Iterator to the first element
    public abstract void Reset();
}

abstract class IteratorAggregate : IEnumerable
{
    // Returns an Iterator or another IteratorAggregate for the implementing
    // object.
    public abstract IEnumerator GetEnumerator();
}

// Concrete Iterators implement various traversal algorithms. These classes
// store the current traversal position at all times.
class AlphabeticalOrderIterator : Iterator2
{
    private readonly WordsCollection _collection;

    // Stores the current traversal position. An iterator may have a lot of
    // other fields for storing iteration state, especially when it is
    // supposed to work with a particular kind of collection.
    private int _position = -1;

    private readonly bool _reverse = false;

    public AlphabeticalOrderIterator(WordsCollection collection, bool reverse = false)
    {
        _collection = collection;
        _reverse = reverse;

        if (reverse)
        {
            _position = collection.GetItems().Count;
        }
    }

    public override object Current() => _collection.GetItems()[_position];

    public override int Key() => _position;

    public override bool MoveNext()
    {
        var updatedPosition = _position + (_reverse ? -1 : 1);

        if (updatedPosition < 0 || updatedPosition >= _collection.GetItems().Count) return false;
        _position = updatedPosition;
        return true;
    }

    public override void Reset()
    {
        _position = _reverse ? _collection.GetItems().Count - 1 : 0;
    }
}

// Concrete Collections provide one or several methods for retrieving fresh
// iterator instances, compatible with the collection class.
class WordsCollection : IteratorAggregate
{
    readonly List<string> _collection = new();

    private bool _direction = false;

    public void ReverseDirection()
    {
        _direction = !_direction;
    }

    public List<string> GetItems() => _collection;

    public void AddItem(string item) => _collection.Add(item);

    public override IEnumerator GetEnumerator()
        => new AlphabeticalOrderIterator(this, _direction);
}