using Common.Maze;

namespace DesignPatterns.AbstractFactory;

public class MazeFactory
{
    public virtual Maze MakeMaze() => new();
    public virtual Wall MakeWall() => new();
    public virtual Room MakeRoom(int roomNumber) => new(roomNumber);
    public virtual Door MakeDoor(Room room1, Room room2) => new(room1, room2);
}