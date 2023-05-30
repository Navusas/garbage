using DesignPatterns.Creational.Singleton;
using Autofac;

// Initialise the singleton instance
var maze = MazeFactory.GetInstance();



// I think these days, in .NET world, you would instead be using AutoFac?

// Like so:
// 1. Register any singleton you want as interface
var builder = new ContainerBuilder();
builder.RegisterType<BombedMazeSingleton>().As<ISingleton>().SingleInstance();
var container = builder.Build();

// 2. Access it like so in console apps, or in ASP.NET Core, you would inject it into your controller
var singleton = container.Resolve<ISingleton>();
// 3. Call a methd
singleton.ToString();


// C# guarantees, that when you use static constructor, and when it's called, it's thread safe, because C# ensures that it is only executed by one thread.
// So, you don't need to worry about thread safety.
class Load
{
    private static Load _instance;
    static Load()
    {
        _instance = new Load();
        // The only gotcha is that you only have 1 chance to initialise it, because if it ever throws inside static constructor,
        // The C# will never call it again, because it caches the exception, and you will have to restart the application.
    }
}


// Option 2 would be to use Lazy in C#.
// Using Lazy<T> is thread safe, and it's also lazy, so it will only be initialised when you call it.
// And also Lazy ensures that only one person can be running this initialisation code at the time.
// And when the first person finishes, then the value gets cached, and whenever second person gets unblocked
// it will just return the cached value.
class LoadLazy
{
    private Lazy<LoadLazy> _instance = new Lazy<LoadLazy>(() => new LoadLazy());
    public LoadLazy Instance => _instance.Value;
    
}