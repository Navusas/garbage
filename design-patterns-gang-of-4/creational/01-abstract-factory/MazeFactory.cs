using Common.Maze;

namespace DesignPatterns.AbstractFactory;

/// <summary>
/// This class is an entry point.
/// It's not abstract, because, according to the book, it:
///    1. Provides default implementations for the factory methods + is a standard.
///    2. It's easy to make a new one, because the new one will only need to overwrite the methods required. This is called the "Hollywood Principle".
///                                                                                                              ^^^^^ Lol I don't know why GitHub Copilot suggested me this.
/// </summary>
public class MazeFactory
{
    public virtual Maze MakeMaze() => new();
    public virtual Wall MakeWall() => new();
    public virtual Room MakeRoom(int roomNumber) => new(roomNumber);
    public virtual Door MakeDoor(Room room1, Room room2) => new(room1, room2);
}