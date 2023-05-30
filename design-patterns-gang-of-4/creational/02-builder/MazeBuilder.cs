using Common.Maze;

namespace DesignPatterns.Creational.Builder;

public abstract class MazeBuilder
{
    public abstract Maze BuildMaze();
    public abstract void BuildRoom(int roomNumber);
    public abstract void BuildDoor(int roomFrom, int roomTo);
    
    public abstract Maze GetMaze();
}