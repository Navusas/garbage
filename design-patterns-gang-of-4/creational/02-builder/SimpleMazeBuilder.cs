using Common.Maze;

namespace DesignPatterns.Creational.Builder;

public class SimpleMazeBuilder : MazeBuilder
{
    private Maze? _maze;
    private List<Door> _doors = new();

    public override Maze BuildMaze()
    {
        _maze = new Maze();
        return _maze;
    }

    public override void BuildRoom(int roomNumber)
    {
        if (_maze == null)
            throw new InvalidOperationException("Maze must be built first.");

        if (_maze.RoomNo(roomNumber) != null) return;
        var room = new Room(roomNumber);
            
        room.SetSide(Direction.North, new Wall());
        room.SetSide(Direction.South, new Wall());
        room.SetSide(Direction.East, new Wall());
        room.SetSide(Direction.West, new Wall());
    }

    public override void BuildDoor(int roomFrom, int roomTo)
    {
        if (_maze == null)
            throw new InvalidOperationException("Maze must be built first.");
        
        var roomOne = _maze.RoomNo(roomFrom);
        var roomTwo = _maze.RoomNo(roomTo);

        if (roomOne == null) return;
        if (roomTwo == null) return;
        
        var door = new Door(roomOne, roomTwo);
        
        // For the sake of it, we just say North for both sides.
        // In reality, you would have to call 'CommonWall' function to retrieve the common wall.
        roomOne.SetSide(Direction.North, door);
        roomOne.SetSide(Direction.North, door);
    }

    public override Maze GetMaze() => _maze ?? BuildMaze();
}