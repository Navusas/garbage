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

/// <summary>
/// This factory has the same doors, but the rooms are bombed.
/// </summary>
public class BombedMazeFactory
{
    public virtual Wall MakeWall() => new BombedWall();
    public virtual Room MakeRoom(int roomNumber) => new RoomWithABomb(roomNumber);
}