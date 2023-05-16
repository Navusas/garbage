using Common.Maze;

namespace DesignPatterns.Builder;

public class MazeGame
{
    public Maze CreateMaze(MazeBuilder builder)
    {
        builder.BuildMaze();
        
        builder.BuildDoor(1);
        builder.BuildRoom(2);
        builder.BuildDoor(1,2);
        
        return builder.GetMaze();
    }
}