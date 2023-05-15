using Common.Maze;

namespace DesignPatterns.AbstractFactory;

public class BombedWall : Wall
{
    
}

public class RoomWithABomb : Room
{
    public RoomWithABomb(int roomNumber) : base(roomNumber)
    {
    }
}

public class BombedMazeFactory
{
    public virtual Wall MakeWall() => new BombedWall();
    public virtual Room MakeRoom(int roomNumber) => new RoomWithABomb(roomNumber);
}