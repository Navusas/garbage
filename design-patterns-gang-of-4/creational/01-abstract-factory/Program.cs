
using DesignPatterns.AbstractFactory;

var maze = new MazeGame().CreateMaze(new MazeFactory());
// var maze = new MazeGame().CreateMaze(new BombedMazeFactory());
// var maze = new MazeGame().CreateMaze(new EnchantedMazeFactory());