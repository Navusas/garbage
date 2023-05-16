using Common.Maze;
using DesignPatterns.Builder.MyInterpretation;

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

    public static Maze MyInterpretation(IMyMazeBuilder builder)
    {
        return builder.BuildMaze()
            .BuildRoom(1)
            .BuildRoom(2)
            .BuildDoor(1, 2)
            .GetMaze();
    }

    public static Maze MyInterpretation2(IMyMazeBuilder2 builder)
    {
        return builder.BuildMaze()
            .BuildRoom(1)
            .BuildRoom(2)
            .BuildDoor(1, 2)
            .Build();
    }
}