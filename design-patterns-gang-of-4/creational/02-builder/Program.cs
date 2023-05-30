using DesignPatterns.Creational.Builder;
using DesignPatterns.Creational.Builder.MyInterpretation;

// simple 
var builder = new SimpleMazeBuilder();
var mazeGame = MazeGame.CreateMaze(builder);
var maze = builder.GetMaze();

// 1
var myBuilder = new MyMazeBuilder();
var myMazeGame = MazeGame.MyInterpretation(myBuilder);

// 2
var myMaeGame2 = MazeGame.MyInterpretation2(new MyMazeBuilder2());