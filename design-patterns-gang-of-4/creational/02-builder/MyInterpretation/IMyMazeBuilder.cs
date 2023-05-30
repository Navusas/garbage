using Common.Maze;

namespace DesignPatterns.Creational.Builder.MyInterpretation;

/// <summary>
/// I like this one more, because it gives me a LINQ type of experience
/// However, the drawback is that the user MUST put stuff in the order that the builder expects.
///
/// So now, saying that... I don't like this one. I don't like 'expecting' and 'hoping'.
/// </summary>
public interface IMyMazeBuilder
{
    public IMyMazeBuilder BuildMaze();
    public IMyMazeBuilder BuildRoom(int roomNumber);
    public IMyMazeBuilder BuildDoor(int roomFrom, int roomTo);
    public Maze GetMaze();
}

public class MyMazeBuilder : IMyMazeBuilder
{
    private Maze _maze = new();
    public IMyMazeBuilder BuildMaze()
    {
        _maze = new Maze();
        return this;
    }

    public IMyMazeBuilder BuildRoom(int roomNumber)
    {
        // some code here
        return this;
    }

    public IMyMazeBuilder BuildDoor(int roomFrom, int roomTo)
    {
        // some code here
        return this;
    }

    public Maze GetMaze()
    {
        // Work finished, let's return full blown object
        return _maze;
    }
}