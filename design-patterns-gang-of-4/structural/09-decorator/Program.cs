using DesignPatterns.Structural.Decorator;

Console.WriteLine("09-Decorator");

// Below is the example from the book.
// The idea is that you have a base component, and decorate it with other components, if needed.
// The decorators are also components, can be added at runtime, and can be nested.

var component = new VisualComponent();
var borderDecorator = new BorderDecorator(component, 5);
var scrollDecorator = new ScrollDecorator(borderDecorator);
scrollDecorator.Draw();

// My playground:
// var gamestate = GameState.GetInstance();
// gamestate.Play();

// Questions: When you want to remove a decorator, do you reconstruct the entire object from scratch?
// ^ If so, how do you keep a state of what has been called? 
// ^ How do you do that with multiple decorators being added of the same type?


class VisualComponent
{
    public virtual void Draw()
    {
        Console.WriteLine("Drawing");
    }
    public virtual void Resize()
    {
        Console.WriteLine("Resizing");
    }
}

class Decorator : VisualComponent
{
    private readonly VisualComponent _component;

    public Decorator(VisualComponent component)
    {
        _component = component;
    }

    public override void Draw()
    {
        _component.Draw();
    }

    public override void Resize()
    {
        _component.Resize();
    }
}
class BorderDecorator : Decorator
{
    private readonly int _width;

    public BorderDecorator(VisualComponent component, int borderWidth) : base(component)
    {
        _width = borderWidth;
    }

    public override void Draw()
    {
        base.Draw();
        DrawBorder(_width);
    }

    private void DrawBorder(int width)
    {
        Console.WriteLine($"Drawing border of width {width}");
    }
}

class ScrollDecorator : Decorator
{
    public ScrollDecorator(VisualComponent component) : base(component)
    {
    }

    public override void Draw()
    {
        base.Draw();
        DrawScroll();
    }

    private void DrawScroll()
    {
        Console.WriteLine("Drawing scroll");
    }
}
