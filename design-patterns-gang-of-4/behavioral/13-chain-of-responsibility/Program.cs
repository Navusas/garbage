var mainWindow = new MainWindow { HelpText = "Main Window" };
var layoutWindow = new LayoutWindow { Parent = mainWindow, HelpText = "Layout Window" };
var button = new Button { Parent = layoutWindow, HelpText = "Button" };

Console.WriteLine(button.HelpMessage());

abstract class Window
{
    public Window? Parent { get; init; }
    public string? HelpText { get; init; }
    public string? HelpMessage() => HelpText ?? Parent?.HelpMessage();
}

class MainWindow : Window
{

}

class LayoutWindow : Window
{
}

class Button : Window
{
}