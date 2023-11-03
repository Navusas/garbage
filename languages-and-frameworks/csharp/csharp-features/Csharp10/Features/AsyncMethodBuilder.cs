// using System;
// using System.Runtime.CompilerServices;
// using System.Threading.Tasks;
// using System.Threading.Tasks.Sources;

// namespace Csharp10;

// /*
// Title:          Async Method Builders
// Description:    TBD
// Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-10.0/async-method-builders.md

// Extra:
// AsyncValueTaskMethodBuilder Source Code: https://github.com/dotnet/runtime/blob/aea45f7aaa8f73cb2a585d8b14ce66634d58cb68/src/libraries/System.Private.CoreLib/src/System/Runtime/CompilerServices/AsyncValueTaskMethodBuilder.cs#L11
// */
// public class AsyncMethodBuilder
// {
//     public static async ValueTask<int> DemonstrateBefore()
//     {
//         await Task.Delay(100);
//         return 42;
//     }

//     [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<>))]
//     public static async ValueTask<int> DemonstrateAfter()
//     {
//         await Task.Delay(10000);
//         return 42; // Return value using the custom builder.
//     }
// }





// public class PoolingAsyncValueTaskMethodBuilder
// {
//     private ManualResetValueTaskSourceCore<bool> _core;
//     private Action<object>? _continuation;

//     public static PoolingAsyncValueTaskMethodBuilder Create() => new();

//     public void SetResult()
//     {
//         System.Console.WriteLine("SetResult");
//         _core.SetResult(true);
//         if (_continuation != null)
//         {
//             Console.WriteLine("Invoking continuation");
//             _continuation(null);
//         }
//     }

//     public void SetException(Exception exception) => _core.SetException(exception);

//     public ValueTask Task
//     {
//         get { return new ValueTask((IValueTaskSource)this, _core.Version); }
//     }

//     public void Start<TStateMachine>(ref TStateMachine stateMachine)
//         where TStateMachine : IAsyncStateMachine
//     {
//         Console.WriteLine("Start");
//         stateMachine.MoveNext();
//         Console.WriteLine("MoveNext");
//     }

//     public void SetStateMachine(IAsyncStateMachine stateMachine)
//     {
//         Console.WriteLine("State Machine is pooled");
//     }

//     public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
//         where TAwaiter : INotifyCompletion
//         where TStateMachine : IAsyncStateMachine
//     {
//         awaiter.OnCompleted(GetContinuation(ref stateMachine));
//     }

//     public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
//         where TAwaiter : ICriticalNotifyCompletion
//         where TStateMachine : IAsyncStateMachine
//     {
//         awaiter.UnsafeOnCompleted(GetContinuation(ref stateMachine));
//     }

//     private Action GetContinuation<TStateMachine>(ref TStateMachine stateMachine)
//         where TStateMachine : IAsyncStateMachine
//     {
//         System.Console.WriteLine("GetContinuation");
//         if (_continuation == null)
//         {
//             Console.WriteLine("Creating continuation");
//             _continuation = stateMachine.MoveNext;
//         }
//         System.Console.WriteLine("Returning continuation");

//         return _continuation;
//     }

//     // Required by IValueTaskSource
//     public void GetResult(short token)
//     {
//         Console.WriteLine("GetResult");
//         _core.GetResult(token);
//     }

//     // Required by IValueTaskSource
//     public ValueTaskSourceStatus GetStatus(short token)
//     {
//         Console.WriteLine("GetStatus");
//         return _core.GetStatus(token);
//     }

//     // Required by IValueTaskSource
//     public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
//     {
//         _core.OnCompleted(continuation, state, token, flags);
//     }
// }