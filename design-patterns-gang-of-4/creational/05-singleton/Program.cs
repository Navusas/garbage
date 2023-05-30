using DesignPatterns.Creational.Singleton;
using Autofac;

// Initialise the singleton instance
var maze = MazeFactory.GetInstance();


// I think these days, in .NET world, you would instead be using AutoFac?

// Like so:
// 1. Register any singleton you want as interface
var builder = new ContainerBuilder();
builder.RegisterType<BombedMazeSingleton>().As<ISingleton>().SingleInstance();
var container = builder.Build();

// 2. Access it like so in console apps, or in ASP.NET Core, you would inject it into your controller
var singleton = container.Resolve<ISingleton>();
// 3. Call a methd
singleton.ToString();