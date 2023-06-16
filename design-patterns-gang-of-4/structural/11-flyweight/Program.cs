var colors  = new[]{ "Red", "Green", "Blue", "White", "Black" };

for(int i = 0 ; i < 10; i++)
{
    var circle = (Circle)ShapeFactory.GetCircle(colors[new Random().Next(0, 5)]);
    circle.Draw(i, i+1);
}

public interface IShape
{
    void Draw(int x, int y);
}

public class Circle : IShape
{
    public string Color { get; init; }
    public Circle(string color)
    {
        Color = color;
    }

    public void Draw(int x, int y)
    {
        Console.WriteLine($"Drawing a {Color} circle");
    }
}

public class ShapeFactory
{
    private static Dictionary<string, Circle> CircleMap = new();

    public static IShape GetCircle(string color)
    {
        CircleMap.TryGetValue(color, out var circle);

        if (circle != null) return circle;
        
        circle = new Circle(color);
        CircleMap.Add(color, circle);
        Console.WriteLine($"Creating circle of color : {color}");
        return circle;
    }
}