var clive = new Individual("clive");
var dom = new Individual("Dom");

var meeting = new Collection(new[] { clive, dom });

clive.DoIt();
dom.DoIt();

meeting.DoIt();

interface IThing
{
    void DoIt();
}

class Individual : IThing
{
    private readonly string _name;

    public Individual(string name) => _name = name;
    
    public void DoIt()
    {
        Console.WriteLine($"Individual {_name}");
    }
}

class Collection : IThing
{
    private readonly List<IThing> _items;

    public Collection(IEnumerable<IThing> items)
    {
        _items = items.ToList();
    }
    public void DoIt()
    {
        Console.WriteLine("Collection");
        foreach (var item in _items)
        {
            item.DoIt();
        }
    }
}
