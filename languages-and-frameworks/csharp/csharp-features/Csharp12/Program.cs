Console.WriteLine("[C# 12]: Program starting");

var usingAliasType = new UsingAliasType();
usingAliasType.DemonstrateBefore();
usingAliasType.DemonstrateAfter();


int value = 42;

var refReadonlyParams = new RefReadonlyParams();
refReadonlyParams.DemonstrateBefore(in value);
refReadonlyParams.DemonstrateAfter(ref value);

Console.WriteLine("[C# 12]: Program ended");
