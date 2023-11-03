global using Csharp10;

Console.WriteLine("[C# 10]: Program starting");

// Constant Interpolated Strings
var constantInterpolatedStringExample = new ConstantInterpolatedStrings();
constantInterpolatedStringExample.DemonstrateBefore();
constantInterpolatedStringExample.DemonstrateAfter();


// Async Method Builders
var result1 = await AsyncMethodBuilder.DemonstrateBefore();
var result2 = await AsyncMethodBuilder.DemonstrateAfter();

Console.WriteLine($"[C# 10]: Result1: {result1}, Result2: {result2}");

Console.WriteLine("[C# 10]: Program ended");