var state = new [] {1,2,3};
var commandStack = new Stack<ICommand>();

ICommand command1 = new Modifier(state, 0, 1, 3);
ICommand command2 = new Modifier(state, 2, 3, 1);

foreach (var command in new[] { command1, command2 })
{
    command.Execute();
    commandStack.Push(command);
}

while (commandStack.Count > 0)
{
    commandStack.Pop().Undo();
}


interface ICommand
{
    void Execute();
    void Undo();
}
class Modifier : ICommand
{
    private readonly int[] _state;
    private readonly int _item;
    private readonly int _from;
    private readonly int _to;

    public Modifier(int[] state, int item, int from, int to)
    {
        _state = state;
        _item = item;
        _from = from;
        _to = to;
    }
    public void Execute()
    {
        if (_state[_item] != _from)
        {
            throw new InvalidOperationException("Invalid start state");
        }

        _state[_item] = _to;
    }
    public void Undo()
    {
        if (_state[_item] != _to)
        {
            throw new InvalidOperationException("Invalid start state");
        }

        _state[_item] = _from;
    }
}