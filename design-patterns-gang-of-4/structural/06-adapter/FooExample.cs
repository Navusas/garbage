namespace DesignPatterns.Structural.Adapter;

public class LibraryA
{
    public void Start()
    {
        // Do some work
    }

    public void End()
    {
        // Do some work
    }
}

public class LibraryB
{
    public void Begin()
    {
        // Do some work
    }

    public void Terminate()
    {
        // Do some work
    }
}

public interface ITargetAdapter
{
    void Open();
    void Close();
}

public class AdapterA : ITargetAdapter
{
    private LibraryA A { get; set; }

    public AdapterA( LibraryA a )
    {
        this.A = a;
    }

    public void Open() { this.A.Start(); }
    public void Close() { this.A.End(); }
}

public class AdapterB : ITargetAdapter
{
    private LibraryB B { get; set; }

    public AdapterB( LibraryB a )
    {
        this.B = a;
    }

    public void Open() { this.B.Begin(); }
    public void Close() { this.B.Terminate(); }
}

public class Client
{
    public Client()
    {
        // We new this here instead of constructor, because this is more testable.
        // (We can mock the library, and pass it to Adapter)
        ITargetAdapter adapter = new AdapterA( new LibraryA() );
        adapter.Open();
        adapter.Close();
    }
    public void DoWork( ITargetAdapter adapter )
    {
        adapter.Open();
        // Do some work
        adapter.Close();
    }
}