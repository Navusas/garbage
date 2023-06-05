namespace DesignPatterns.Structural.Decorator;


public class GameState
{
    private static GameState? _instance;
    private VisualComponent _baseComponent;
    // private IEnumerable<VisualComponent> _components;

    private GameState()
    {
        _baseComponent = new VisualComponent();
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
                    _baseComponent = new BorderDecorator(_baseComponent, 5);
                    break;
                case 2:
                    _baseComponent = new ScrollDecorator(_baseComponent);
                    break;
                case 3:
                    _baseComponent.Draw();
                    break;
                case 4:
                    return;
                default:
                    continue;
            }
        }
    }

    private int GetChoice()
    {
        Console.WriteLine("1. Add border");
        Console.WriteLine("2. Add scrollbar");
        Console.WriteLine("3. Draw");
        Console.WriteLine("4. Exit");
        Console.WriteLine("Enter your choice: ");
        var choice = Console.ReadLine();

        if (!int.TryParse(choice, out var result))
            throw new ArgumentException("Invalid choice");
        return result;
    }
}