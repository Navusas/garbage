using DesignPatterns.Creational.AbstractFactory;
/*
 * Here, we can use a different factory to create a completely different maze.
 * The power of the abstract factory pattern is that we can change the maze completely by just changing this one line,
 * but not worrying about the actual maze creation happening in <seealso cref="MazeGame"/>.
 *
 *  Q: What if I need to combine the factories? Would I have to duplicate the code in both factories?
 */

// Create a default maze, using a default factory
var maze = new MazeGame().CreateMaze(new MazeFactory());

// This would use a different factory, and create a completely different maze.
//var maze = new MazeGame().CreateMaze(new BombedMazeFactory());
//var maze = new MazeGame().CreateMaze(new EnchantedMazeFactory());