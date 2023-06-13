var counter = new Counter();
Console.WriteLine(counter.Adjust());
Console.WriteLine(counter.Adjust());
Console.WriteLine(counter.Adjust());

counter.ChangeToDecrementMode();
Console.WriteLine(counter.Adjust());
Console.WriteLine(counter.Adjust());
Console.WriteLine(counter.Adjust());


class Counter
{
    private int _count = 0;
    private CounterState _state = new IncrementMode();

    public int Adjust()
    {
        return (_count = _state.Adjust(_count));
    }

    public void ChangeToIncrementMode() =>
        _state = new IncrementMode();
    public void ChangeToDecrementMode() =>
        _state = new DecrementMode();

    abstract class CounterState
    {
        public abstract int Adjust(int state);
    }

    class IncrementMode : CounterState
    {
        public override int Adjust(int state)
        {
            return state + 1;
        }
    }

    class DecrementMode : CounterState
    {
        public override int Adjust(int state)
        {
            return state - 1;
        }
    }

}