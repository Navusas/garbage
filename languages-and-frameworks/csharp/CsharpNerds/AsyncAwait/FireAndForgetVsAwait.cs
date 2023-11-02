namespace CsharpNerds.AsyncAwait;


public static class FireAndForgetVsAwaitExtensions
{
    public static async Task RunAsyncAwaitExample() 
    {
        Console.WriteLine("asyncAndAwait started");
        var asyncAndAwait = new FireAndForgetVsAwait();
        await asyncAndAwait.InvokeAsyncAwait();
        Console.WriteLine("asyncAndAwait ended");
    }
    
    public static async Task RunFireAndForgetExample()
    {
        Console.WriteLine("fireAndForget started");
        var fireAndForget = new FireAndForgetVsAwait();
        await fireAndForget.InvokeFireAndForgetAsync();
        Console.WriteLine("fireAndForget ended");
    }
}

/// <summary>
/// An example to showcase difference of FireAndForget vs Await/Async in action
/// </summary>
public class FireAndForgetVsAwait
{

    public async Task InvokeAsyncAwait()
    {
        Console.WriteLine("InvokeAsyncAwait: started");

        try {
            await LongRunningTask();
            Console.WriteLine($"InvokeAsyncAwait: LongRunningTask succeeded");
        }
        catch(Exception e)
        {
            Console.WriteLine($"InvokeAsyncAwait: LongRunningTask failed: {e.Message}");
        }

        Console.WriteLine("InvokeAsyncAwait: Ended");
    }

    public async Task InvokeFireAndForgetAsync()
    {
        Console.WriteLine("InvokeFireAndForgetAsync: started");

        var _ = LongRunningTask().ContinueWith(t =>
        {
            if (t.IsFaulted)
                Console.WriteLine($"InvokeFireAndForgetAsync: LongRunningTask failed: {t.Exception?.Message}");
            else
                Console.WriteLine($"InvokeFireAndForgetAsync: LongRunningTask succeeded");
        });

        Console.WriteLine("InvokeFireAndForgetAsync: Ended");
    }

    private async Task LongRunningTask()
    {
        Console.WriteLine("LongRunningTask: started");

        await Task.Delay(TimeSpan.FromSeconds(5));
        Console.WriteLine("LongRunningTask: ended");
        
        Console.WriteLine("LongRunningTask: throwing exception");
        throw new Exception("LongRunningTask: failed");
    }
}

/*
#############################################
Problem / Motivation:
#############################################
Before:
```
// This should be fire and forget rather than awaited.
var _ = _usageService.AddOpenAiUsageAsync(DateTime.UtcNow, request, response!).ContinueWith(t =>
{
    if (t.IsFaulted)
        _logger.LogError(t.Exception, "Failed to add usage.");
    else
        _logger.LogDebug("Usage recorded.");
});
```
After:
```
try
{
    await _usageService.AddOpenAiUsageAsync(DateTime.UtcNow, request, response);

    _logger.LogDebug("Usage recorded.");
}
catch (Exception exception)
{
    _logger.LogError(exception, "Failed to add usage.");
}
```

>>>> What's the difference? Are they conceptionally identical? 

#############################################
TLDR:
#############################################
Yes. And a very big difference.
Most of the times you want ot use await/async, but definitely not in all cases.
In the fire and forget case, the exception is not propagated to the caller and it's not being awaited. In this case, caller saves 10 seconds.
In the await case, it is. This can lead to a crash of the application if the exception is not handled properly + extra wait times


#############################################
Long explanation:
#############################################
From the caller/client/user perspective, especially if you're indifferent to the success or failure of the operation from a functional standpoint (e.g., the operation's failure doesn't affect the user experience or the next steps in your application flow), there are still a few considerations to keep in mind when choosing between these approaches:

Impact on Application Behavior
1. Error Propagation:
- Before (Fire-and-Forget): The caller will not know if the task failed or succeeded. Failures are logged but do not affect the calling context. If an exception occurs, it won't propagate to the caller.
- After (Await): If the task throws an exception and you don’t catch it within the method itself, it will propagate up to the caller. This can impact the calling method, potentially leading to application errors if not properly handled.

2. Execution Timing:
- Before: The task is run in the background. The calling method continues execution without waiting for the task to complete. This can be beneficial for responsiveness, particularly in UI applications or when the execution of subsequent code does not depend on the completion of this task.
- After: The calling method waits until the task completes before proceeding. This can affect responsiveness if the task takes a significant amount of time.

System Resource Considerations
- Before (Fire-and-Forget): Since it does not await the task's completion, this approach might lead to unobserved task exceptions (if you're not handling them inside the task itself), which, in some environments, can terminate your process. However, in your code, exceptions are logged, mitigating this risk.
- After (Await): Awaiting the task ensures that exceptions are observed and can be handled directly. This approach can be safer in terms of exception handling and can provide a clearer understanding of where and why a task failed.

Choosing Between the Two
- Use Fire-and-Forget (Before): If the task is truly ancillary to your main application flow, the operation is completely independent, and you want to minimize wait times for the caller, then the fire-and-forget method might be more appropriate. Just ensure that logging and error handling within the task itself are sufficient for your needs.
- Use Await (After): If you might need to extend this method in the future to handle the result of the task, or if you want to ensure that all resources used by the task are properly disposed of before continuing, then awaiting the task would be the safer choice.

Summary
- For operations where the result does not impact user experience or subsequent code execution and where execution speed is a priority, the fire-and-forget approach can be suitable.
- However, be mindful of potential drawbacks like unobserved exceptions (though your logging mitigates this) and potential difficulty in debugging, as the execution flow is less linear and predictable than with awaited asynchronous calls.

*/