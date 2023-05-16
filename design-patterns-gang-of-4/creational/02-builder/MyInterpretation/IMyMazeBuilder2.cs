using Common.Maze;

namespace DesignPatterns.Builder.MyInterpretation;

/// <summary>
/// OK, this isn't so great, because BuildDoor depends on BuildRoom.
/// Smashing it all in the one last Build() object may not be the best idea.
/// </summary>
public interface IMyMazeBuilder2
{
    public IMyMazeBuilder2 BuildMaze();
    public IMyMazeBuilder2 BuildRoom(int roomNumber);
    public IMyMazeBuilder2 BuildDoor(int roomFrom, int roomTo);
    public Maze Build();
}

public class MyMazeBuilder2 : IMyMazeBuilder2
{
    private bool _buildMazeCalled;
    private List<int> _roomNumbersToAdd = new();
    

    public IMyMazeBuilder2 BuildMaze()
    {
        _buildMazeCalled = true;
        return this;
    }

    public IMyMazeBuilder2 BuildRoom(int roomNumber)
    {
        _roomNumbersToAdd.Add(roomNumber);
        // some code here
        return this;
    }

    public IMyMazeBuilder2 BuildDoor(int roomFrom, int roomTo)
    {
        // some code here
    }

    public Maze Build()
    {
        // create maze
        if (!_buildMazeCalled) throw new InvalidOperationException("Must call `BuildMaze()` method") 
        var maze = new Maze();

        if (_roomNumbersToAdd.Any())
        {
            _roomNumbersToAdd.Select(x => new Room(x)).ToList().ForEach(x => maze.AddRoom(x));
        }
        
        // Before adding doors, validate that the room exist

        // Work finished, let's return full blown object
        return maze;
    }
}