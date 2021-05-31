using System;
using System.Threading.Tasks;

namespace Primrose.Primitives.Tasks
{
  /// <summary>Defines a container for handled tasks</summary>
  public struct TaskSet<T>
  {
    /// <summary>The main action</summary>
    public Action<T> Task;

    /// <summary>The action to be performed on encountered exception during execution of the main action</summary>
    public Action<Task, T> ActionFail;

    /// <summary>The action to be performed on successful completion of the main action</summary>
    public Action<Task, T> ActionPass;

    /// <summary>Creates a container for handled tasks</summary>
    public TaskSet(Action<T> task = null
                          , Action<Task> actionfail = null
                          , Action<Task> actionpass = null)
    {
      Task = task;
      ActionFail = (t, o) => actionfail(t);
      ActionPass = (t, o) => actionpass(t);
    }

    /// <summary>Creates a container for handled tasks</summary>
    public TaskSet(Action<T> task = null
                          , Action<Task, T> actionfail = null
                          , Action<Task, T> actionpass = null)
    {
      Task = task;
      ActionFail = actionfail;
      ActionPass = actionpass;
    }
  }
}
