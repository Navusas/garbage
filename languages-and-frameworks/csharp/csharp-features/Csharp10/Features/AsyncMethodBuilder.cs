using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Csharp10;

public static class AsyncMethodBuilderDemo
{
    public static async Task Demonstrate()
    {
        var example = new AsyncMethodBuilder();
        await example.DemonstrateBefore();
        await example.DemonstrateAfter();
    }
}

/*
Title:          Async Method Builders
Description:    Allow for greater control and optimization fo async operations
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-10.0/async-method-builders.md

Bonus:
AsyncValueTaskMethodBuilder Source Code: https://github.com/dotnet/runtime/blob/aea45f7aaa8f73cb2a585d8b14ce66634d58cb68/src/libraries/System.Private.CoreLib/src/System/Runtime/CompilerServices/AsyncValueTaskMethodBuilder.cs#L11
*/
public class AsyncMethodBuilder
{
    public async Task DemonstrateBefore()
    {
        Console.WriteLine("[AsyncMethodBuilder] Before: Starting...");
        await Task.Delay(2000);
        Console.WriteLine("[AsyncMethodBuilder] Before: Completed...");
    }

    public async PerformanceMeasuringTask DemonstrateAfter()
    {
        Console.WriteLine("[AsyncMethodBuilder] After: Starting...");
        await PerformanceMeasuringTask.Delay(2000);
        Console.WriteLine("[AsyncMethodBuilder] After: Completed...");
    }
}





public class PerformanceTrackingAsyncMethodBuilder
{
    private Stopwatch? _stopwatch;
    private string? _methodName;
    public PerformanceMeasuringTask Task => new();

    public static PerformanceTrackingAsyncMethodBuilder Create() => new();

    public void Start<TStateMachine>(ref TStateMachine stateMachine)
        where TStateMachine : IAsyncStateMachine
    {
        _methodName = stateMachine.GetType().Name;
        _stopwatch = Stopwatch.StartNew();
        Console.WriteLine($"[AsyncMethodBuilder] PerformanceTrackingAsyncMethodBuilder: Starting async method: {_methodName}");
        stateMachine.MoveNext();
    }

    public void SetStateMachine(IAsyncStateMachine stateMachine) { }

    public void SetException(Exception exception)
    {
        Console.WriteLine($"[AsyncMethodBuilder] PerformanceTrackingAsyncMethodBuilder: SetException");
        _stopwatch?.Stop();
        Console.WriteLine($"[AsyncMethodBuilder] PerformanceTrackingAsyncMethodBuilder: Exception in {_methodName}: {exception.Message}");
    }

    public void SetResult()
    {
        Console.WriteLine($"[AsyncMethodBuilder] PerformanceTrackingAsyncMethodBuilder: SetResult");
        _stopwatch?.Stop();
        Console.WriteLine($"[AsyncMethodBuilder] PerformanceTrackingAsyncMethodBuilder: Async method {_methodName} completed in {_stopwatch?.ElapsedMilliseconds} ms");
        Task.Complete();
    }

    public void GetResult()
    {
        Console.WriteLine($"[AsyncMethodBuilder] PerformanceTrackingAsyncMethodBuilder: GetResult");
    }

    public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : INotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        Console.WriteLine($"[AsyncMethodBuilder] PerformanceTrackingAsyncMethodBuilder: AwaitOnCompleted");
        awaiter.OnCompleted(stateMachine.MoveNext);
    }

    public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
        where TAwaiter : ICriticalNotifyCompletion
        where TStateMachine : IAsyncStateMachine
    {
        Console.WriteLine($"[AsyncMethodBuilder] PerformanceTrackingAsyncMethodBuilder: AwaitUnsafeOnCompleted");
        awaiter.OnCompleted(stateMachine.MoveNext);
    }
}


// Custom task-like type
[AsyncMethodBuilder(typeof(PerformanceTrackingAsyncMethodBuilder))]
public class PerformanceMeasuringTask
{
    private readonly TaskCompletionSource<object> _tcs = new();

    public CustomTaskAwaiter GetAwaiter() => new(_tcs.Task);

    public class CustomTaskAwaiter(Task task) : INotifyCompletion
    {
        private readonly Task _task = task;
        public bool IsCompleted => _task.IsCompleted;
        public void GetResult() => _task.GetAwaiter().GetResult();
        public void OnCompleted(Action continuation) => _task.ContinueWith(_ => continuation());
    }

    public void Complete() => _tcs.SetResult(null);


    public static PerformanceMeasuringTask Delay(int millisecondsDelay)
    {
        var task = new PerformanceMeasuringTask();
        Task.Delay(millisecondsDelay).ContinueWith(_ => task.Complete());
        return task;
    }
}