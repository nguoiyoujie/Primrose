using System.Collections.Concurrent;

namespace Primrose.Primitives.Pipelines
{
  /// <summary>
  /// Maintains and executes objects in an queue. Useful for FIFO procedures like UI modifications.
  /// </summary>
  /// <typeparam name="T">A piped object</typeparam>
  public class Pipeline<T> where T : IPipedObject
  {
    private readonly ConcurrentQueue<T> pipe = new ConcurrentQueue<T>();

    /// <summary>The maximum number of piped objects to be executed per call to Run()</summary>
    public int MaxExecutionsPerRun = 1;

    /// <summary>
    /// Creates a pipeline
    /// </summary>
    /// <param name="maxExecutionsPerRun">The maximum number of piped objects to be executed per call to Run()</param>
    public Pipeline(int maxExecutionsPerRun = 1) { MaxExecutionsPerRun = maxExecutionsPerRun; }

    /// <summary>Queues an piped object into the pipeline</summary>
    /// <param name="item"></param>
    public void Queue(T item) { pipe.Enqueue(item); }

    /// <summary>Returns the number of piped objects in the pipeline</summary>
    public int Count { get { return pipe.Count; } }

    /// <summary>Runs the execution of queued objects, up to a limit defined by MaxExecutionsPerRun. Returns the number of queued objects run</summary>
    public int Run()
    {
      int x = 0;
      if (pipe.Count == 0) { return x; }
      OnStartCycle();
      while (++x < MaxExecutionsPerRun && pipe.TryDequeue(out T pobj))
      {
        pobj.Execute();
      }
      OnEndCycle();
      return x;
    }

    /// <summary>The method executed just before the start of each execution cycle</summary>
    protected virtual void OnStartCycle() { }

    /// <summary>The method executed just after the end of each execution cycle</summary>
    protected virtual void OnEndCycle() { }

    /// <summary>Clears the queue without execution</summary>
    public void Clear()
    {
      while (pipe.TryDequeue(out _)) { }
    }
  }

  /// <summary>
  /// Defines a piped object to be processed in a pipeline
  /// </summary>
  public interface IPipedObject
  {
    /// <summary>Provides execution entry point from the pipeline</summary>
    void Execute();
  }
}