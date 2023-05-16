using Common.Maze;

namespace DesignPatterns.Builder;

public static class MazeGame
{
    public static Maze CreateMaze(MazeBuilder builder)
    {
        builder.BuildMaze();
        
        builder.BuildRoom(1);
        builder.BuildRoom(2);
        builder.BuildDoor(1,2);
        
        return builder.GetMaze();
    }
}