using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives.Collections
{
  /// <summary>
  /// Provides a FIFO queue based on circular linkage
  /// </summary>
  /// <typeparam name="T">The item type stored in the queue</typeparam>
  public class CircularQueue<T>
  {
    private readonly T[] nodes;
    private int count;
    private int front;
    private int rear;
    private readonly bool errifexceed;

    /// <summary>
    /// Creates a queue.
    /// </summary>
    /// <param name="size">The size of the queue</param>
    /// <param name="errifexceed">Whether an exception is thrown if the queue is full</param>
    public CircularQueue(int size, bool errifexceed)
    {
      this.nodes = new T[size];
      this.count = 0;
      this.front = 0;
      this.rear = 0;
      this.errifexceed = errifexceed;
    }

    /// <summary>Retrieves the number of elements in the queue</summary>
    public int Count { get { return count; } }

    /// <summary>Returns whether the queue throws an exception if its capacity is exceeded</summary>
    public bool ErrorIfExceedCapacity { get { return errifexceed; } }

    /// <summary>Enqueues an item from the queue</summary>
    /// <param name="value">The item to be enqueued</param>
    /// <exception cref="InvalidOperationException">Attempted to enqueue an item into the queue that has reached capacity limit.</exception>
    public void Enqueue(T value)
    {
      if (Count >= nodes.Length)
      {
        if (errifexceed)
          throw new CapacityExceededException<CircularQueue<T>>(nodes.Length);
        else
          Dequeue();
      }
      nodes[rear] = value;
      rear = (rear + 1).Modulus(0, nodes.Length);
      count++;
    }

    /// <summary>Dequeues an item from the queue</summary>
    /// <returns></returns>
    public T Dequeue()
    {
      int ret = front;
      front = (front + 1).Modulus(0, nodes.Length);
      count--;
      return nodes[ret];
    }
  }
}
