using Common.Maze;

namespace DesignPatterns.Creational.AbstractFactory;

/// <summary>
/// A maze with 2 rooms and 1 door.
/// </summary>
internal class MazeGame
{
    public Maze CreateMaze(MazeFactory factory)
    {
        var maze = factory.MakeMaze();
        var room1 = factory.MakeRoom(1);
        var room2 = factory.MakeRoom(2);
        var door = factory.MakeDoor(room1, room2); 
        
        maze.AddRoom(room1);
        maze.AddRoom(room2);
        
        room1.SetSide(Direction.North, factory.MakeWall());
        room1.SetSide(Direction.East, door);
        room1.SetSide(Direction.South, factory.MakeWall());
        room1.SetSide(Direction.West, factory.MakeWall()); 

        room2.SetSide(Direction.North, factory.MakeWall());
        room2.SetSide(Direction.East, factory.MakeWall());
        room2.SetSide(Direction.South, factory.MakeWall());
        room2.SetSide(Direction.West, door);

        return maze;
    }
}