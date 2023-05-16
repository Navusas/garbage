using DesignPatterns.Builder;

var builder = new SimpleMazeBuilder();
var mazeGame = MazeGame.CreateMaze(builder);
var maze = builder.GetMaze();