using Common.Maze;

namespace DesignPatterns.AbstractFactory;

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

        room1.SetSide(Direction.North, factory.MakeWall() );
        room1.SetSide(Direction.East, factory.MakeWall());
        room1.SetSide(Direction.South, factory.MakeWall());
        room1.SetSide(Direction.West, door);

        return maze;
    }
}