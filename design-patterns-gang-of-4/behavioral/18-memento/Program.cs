
var originator = new Originator() as IOriginator;

var state = originator.CreateMemento();
Console.WriteLine(originator.GetValue());

originator.ChangeValue();
Console.WriteLine(originator.GetValue());

originator.SetMemento(state);
Console.WriteLine(originator.GetValue());


interface IOriginator
{
    int GetValue();
    void ChangeValue();
    IMemento CreateMemento();
    void SetMemento(IMemento memento);
}

interface IMemento  // Discuss this
{
}

class Originator : IOriginator
{
    private readonly Random _random = new Random();
    private int _state;

    public Originator()
    {
        _state = _random.Next(55);
    }

    public int GetValue() => _state;

    public void ChangeValue() => _state = _random.Next(55);

    public IMemento CreateMemento()
    {
        return new Memento(_state);
    }

    public void SetMemento(IMemento memento)
    {
        if (memento is Memento ourMemento)
        {
            _state = ourMemento._state;
            return;
        }

        throw new InvalidOperationException("Wrong type of memento supplied");
    }

    private class Memento : IMemento
    {
        public readonly int _state;
        public Memento(int state)
        {
            _state = state;
        }
    }
}