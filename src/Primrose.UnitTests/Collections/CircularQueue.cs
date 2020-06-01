using NUnit.Framework;
using Primrose.Primitives.Collections;
using System;

namespace Primrose.UnitTests.Collections
{
  [TestFixture]
  public class CircularQueue
  {
    [TestCase(256)]
    [TestCase(1028)]
    public void CircularQueue_Enqueue_Count_Dequeue(int size)
    {
      CircularQueue<int> queue = new CircularQueue<int>(size, true);

      for (int i = 0; i < size; i++)
        queue.Enqueue(i);

      Assert.That(queue.Count, Is.EqualTo(size));
      int c = 0;
      while (queue.Count > 0)
      {
        Assert.That(queue.Dequeue(), Is.EqualTo(c));
        c++;
      }
    }

    [TestCase(256)]
    [TestCase(1028)]
    public void CircularQueue_WhenErrIfExceedTrue_IfExceedCapacity_Throw(int size)
    {
      CircularQueue<int> queue = new CircularQueue<int>(size, true);

      for (int i = 0; i < size; i++)
        queue.Enqueue(i);

      Assert.Throws<InvalidOperationException>(() => queue.Enqueue(0));
    }

    [TestCase(256)]
    [TestCase(1028)]
    public void CircularQueue_WhenErrIfExceedFalse_IfExceedCapacity_NoThrow(int size)
    {
      CircularQueue<int> queue = new CircularQueue<int>(size, false);

      for (int i = 0; i < size; i++)
        queue.Enqueue(i);

      for (int i = 0; i < 5; i++)
        Assert.DoesNotThrow(() => queue.Enqueue(0));
      Assert.That(queue.Count, Is.EqualTo(size)); // count stays at capacity
    }
  }
}
