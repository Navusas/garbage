
// Lots of interesting implementation details to check
//   - observer polling, dangling references, potential high cost, errors while notifying


var observable = new Observable();

using var sub1 = observable.AddObserver(() => Console.WriteLine("First observer"));

observable.Increment();

{
    using var sub2 = observable.AddObserver(() => Console.WriteLine("Second observer"));

    observable.Increment();
}

observable.Increment();



var x = new Foo();
x.Updated += s => Console.WriteLine("First handler");
x.Updated += s => Console.WriteLine("Second handler");

x.Increment();

// .NET events

class Foo
{
    public delegate void MyDelegate(string s);
    public event MyDelegate Updated = delegate {};
    private int _state = 0;

    public void Increment()
    {
        _state++;
        Updated("_state");
    }
}


class Observable
{
    private int _state = 0;
    private readonly HashSet<Action> _observers = new HashSet<Action>();

    public void Increment()
    {
        _state++;
        foreach (Action action in _observers)
        {
            action();
        }
    }

    public IDisposable AddObserver(Action todo)
    {
        _observers.Add(todo);
        return new UnSubscriber(_observers, todo);
    }

    class UnSubscriber : IDisposable
    {
        private readonly HashSet<Action> _observers;
        private readonly Action _observer;

        public UnSubscriber(HashSet<Action> observers, Action observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            _observers.Remove(_observer);
        }
    }

}

