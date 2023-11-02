// See https://aka.ms/new-console-template for more information

using CsharpNerds.AsyncAwait;

var consoleWriter = new ConsoleWriter("CsharpNerds");
consoleWriter.Write("Program started");


// var usingAliasType = new UsingAliasType();
// usingAliasType.DemonstrateBefore();
// usingAliasType.DemonstrateAfter();



// consoleWriter.Write("fireAndForget started");
// var fireAndForget = new FireAndForgetVsAwait();
// await fireAndForget.InvokeFireAndForgetAsync();
// consoleWriter.Write("fireAndForget ended");


consoleWriter.Write("asyncAndAwait started");
var asyncAndAwait = new FireAndForgetVsAwait();
await asyncAndAwait.InvokeAsyncAwait();
consoleWriter.Write("asyncAndAwait ended");


consoleWriter.Write("Program ended");