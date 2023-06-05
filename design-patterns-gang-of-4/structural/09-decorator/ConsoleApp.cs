namespace DesignPatterns.Structural.Decorator;


/// <summary>
/// A quick console app fgame which demonstrates the Decorator pattern.
/// The idea is that you can add and remove the decorators as you go, and the final
/// product is a composition of all the decorators.
/// </summary>
public enum DecoratorType
{
    Border,
    Scrollbar
}
public class GameState
{
    private static GameState? _instance;
    private List<DecoratorType> _components;

    private GameState()
    {
        _components = new List<DecoratorType>();
    }
    
    public static GameState GetInstance()
    {
        return _instance ?? new GameState();
    }

    public void Play()
    {
        Console.WriteLine("Hey! Let's see how decorator works in action");
        Console.WriteLine("You can manipulate the imaginary 'textbox' below");
        
        while (true)
        {
            var choice = GetChoice();

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Adding border");
                    _components.Add(DecoratorType.Border);
                    break;
                case 2:
                    Console.WriteLine("Adding scrollbar");
                    _components.Add(DecoratorType.Scrollbar);
                    break;
                case 3:
                    if (_components.Any(x => x == DecoratorType.Border))
                    {
                        var remove = _components.First(x => x == DecoratorType.Border);
                        Console.WriteLine("Removing border");
                        _components.Remove(remove);
                    }
                    else
                    {
                        Console.WriteLine("No border to remove");
                    }
                    break;
                case 4:
                    if (_components.Any(x => x == DecoratorType.Scrollbar))
                    {
                        var remove = _components.First(x => x == DecoratorType.Scrollbar);
                        Console.WriteLine("Removing scrollbar");
                        _components.Remove(remove);
                    }
                    else
                    {
                        Console.WriteLine("No scrollbar to remove");
                        
                    }
                    break;
                case 5:
                    PrintComponents();
                    Console.WriteLine("\n\nDrawing...");
                    var component = ComposeVisualComponent();
                    component.Draw();
                    break;
                case 6:
                    return;
                default:
                    continue;
            }
        }
    }

    private int GetChoice()
    {
        Console.WriteLine("\n\n#############");
        Console.WriteLine("[1]. Add border, [2]. Add scrollbar, [3]. Remove border, [4]. Remove scrollbar, [5]. Draw, [6]. Exit");
        Console.WriteLine("Enter your choice: ");
        var choice = Console.ReadLine();

        if (!int.TryParse(choice, out var result))
            throw new ArgumentException("Invalid choice");
        return result;
    }
    private void PrintComponents()
    {
        Console.WriteLine("Components: ");
        foreach (var component in _components)
        {
            Console.WriteLine(component);
        }
    }

    private VisualComponent ComposeVisualComponent()
    {
        var component = new VisualComponent();
        component = _components.Aggregate(component, (current, decoratorType) => decoratorType switch
        {
            DecoratorType.Border => new BorderDecorator(current, 5),
            DecoratorType.Scrollbar => new ScrollDecorator(current),
            _ => throw new ArgumentOutOfRangeException()
        });
        return component;
    }
}