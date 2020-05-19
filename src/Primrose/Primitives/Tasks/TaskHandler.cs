using System;
using System.Threading;
using System.Threading.Tasks;

namespace Primrose.Primitives.Tasks
{
  /// <summary>Methods for starting and handling tasks</summary>
  public static class TaskHandler
  {
    /// <summary>Creates and starts a task</summary>
    /// <param name="taskset">The taskset to run</param>
    /// <returns>The task generated and started by this method</returns>
    public static Task StartNew(TaskSet taskset)
    {
      return StartNew(taskset.Task, taskset.ActionFail, taskset.ActionPass);
    }

    /// <summary>Creates and starts a task</summary>
    /// <param name="taskset">The taskset to run</param>
    /// <param name="state">The parameterized state for the action</param>
    /// <returns>The task generated and started by this method</returns>
    public static Task StartNew<T>(TaskSet<T> taskset, T state)
    {
      return StartNew(taskset.Task, state, taskset.ActionFail, taskset.ActionPass);
    }

    /// <summary>Creates and starts a task</summary>
    /// <param name="action">The main action</param>
    /// <param name="exception_handler">The action to be performed on encountered exception during execution of the main action</param>
    /// <param name="completion_handler">The action to be performed on successful completion of the main action</param>
    /// <returns>The task generated and started by this method</returns>
    public static Task StartNew(Action action, Action<Task> exception_handler, Action<Task> completion_handler)
    {
      if (action == null) action = () => { };
      if (exception_handler == null) exception_handler = c => { };
      if (completion_handler == null) completion_handler = c => { };

      return Task.Factory.StartNew(action)
        .ContinueWith(exception_handler, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously)
        .ContinueWith(completion_handler, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
    }

    /// <summary>Creates and starts a task with a parameterized state</summary>
    /// <typeparam name="T">The type of the parameterized state</typeparam>
    /// <param name="action">The main action</param>
    /// <param name="state">The parameterized state for the action</param>
    /// <param name="exception_handler">The action to be performed on encountered exception during execution of the main action</param>
    /// <param name="completion_handler">The action to be performed on successful completion of the main action</param>
    /// <returns></returns>
    public static Task StartNew<T>(Action<T> action, T state, Action<Task> exception_handler, Action<Task> completion_handler)
    {
      if (action == null) action = c => { };
      if (exception_handler == null) exception_handler = c => { };
      if (completion_handler == null) completion_handler = c => { };

      return Task.Factory.StartNew(() => action(state))
        .ContinueWith(exception_handler, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously)
        .ContinueWith(completion_handler, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
    }

    /// <summary>Creates and starts a task</summary>
    /// <param name="action">The main action</param>
    /// <param name="cancellationToken">The cancellation token for the task</param>
    /// <param name="exception_handler">The action to be performed on encountered exception during execution of the main action</param>
    /// <param name="completion_handler">The action to be performed on successful completion of the main action</param>
    /// <returns>The task generated and started by this method</returns>
    public static Task StartNew(Action action, CancellationToken cancellationToken, Action<Task> exception_handler, Action<Task> completion_handler)
    {
      if (cancellationToken == null)
        return StartNew(action, exception_handler, completion_handler);

      if (action == null) action = () => { };
      if (exception_handler == null) exception_handler = c => { };
      if (completion_handler == null) completion_handler = c => { };

      return Task.Factory.StartNew(action, cancellationToken)
        .ContinueWith(exception_handler, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously)
        .ContinueWith(completion_handler, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
    }

    /// <summary>Creates and starts a task with a parameterized state</summary>
    /// <typeparam name="T">The type of the parameterized state</typeparam>
    /// <param name="action">The main action</param>
    /// <param name="state">The parameterized state for the action</param>
    /// <param name="cancellationToken">The cancellation token for the task</param>
    /// <param name="exception_handler">The action to be performed on encountered exception during execution of the main action</param>
    /// <param name="completion_handler">The action to be performed on successful completion of the main action</param>
    /// <returns></returns>
    public static Task StartNew<T>(Action<T> action, T state, CancellationToken cancellationToken, Action<Task> exception_handler, Action<Task> completion_handler)
    {
      if (cancellationToken == null)
        return StartNew(action, state, exception_handler, completion_handler);

      if (action == null) action = c => { };
      if (exception_handler == null) exception_handler = c => { };
      if (completion_handler == null) completion_handler = c => { };

      return Task.Factory.StartNew(() => action(state), cancellationToken)
        .ContinueWith(exception_handler, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously)
        .ContinueWith(completion_handler, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
    }
  }
}
