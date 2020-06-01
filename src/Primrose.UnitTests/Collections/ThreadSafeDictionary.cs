using NUnit.Framework;
using Primrose.Primitives;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Primrose.UnitTests.Collections
{
  [TestFixture]
  public class ThreadSafeDictionary
  {
    [TestCase(1, 1028)]
    [TestCase(11, 1111)]
    [TestCase(32, 4096)]
    public void ThreadSafeDictionary_Add_MultiThread(int n_tasks, int elements_pertask)
    {
      ThreadSafeDictionary<int, int> list = new ThreadSafeDictionary<int, int>();

      TaskSet<int> ts = new TaskSet<int>();
      ts.Task = new Action<int>((t) =>
      {
        for (int i = 0; i < elements_pertask; i++)
          list.Add(elements_pertask * t + i, i);
      });

      List<Task> tasks = new List<Task>();
      for (int t = 0; t < n_tasks; t++)
        tasks.Add(TaskHandler.StartNew(ts, t));

      bool running = true;
      int cycle = 1;
      while (running)
      {
        int keys = 0;
        int values = 0;
        foreach (int i in list.Keys)  // if any error encountered in the enumerator access, this is not threadsafe
          keys++;

        foreach (int i in list.Values)  // if any error encountered in the enumerator access, this is not threadsafe
          values++;

        Console.WriteLine("Cycle {0}: {1} keys, {2} values".F(cycle, keys, values));
        cycle++;

        running = false;
        foreach (Task tk in tasks)
        {
          if (!tk.IsCompleted)
          {
            running = true;
            break;
          }
        }
      }

      Assert.Multiple(() =>
      {
        Assert.That(list.Count, Is.EqualTo(n_tasks * elements_pertask));

        int[] elements = new int[elements_pertask];
        foreach (int i in list.Values)
          elements[i]++;

        foreach (int element_count in elements)
          Assert.That(element_count, Is.EqualTo(n_tasks));
      });
    }

    [TestCase(1)]
    [TestCase(11)]
    [TestCase(1028)]
    public void ThreadSafeDictionary_Modify_MultiThread(int n_tasks)
    {
      ThreadSafeDictionary<int, int> list = new ThreadSafeDictionary<int, int>();
      list.Add(0, 0);
      list.Add(1, 1);

      Func<int, int> fn_inc = (v) => ++v;
      Func<int, int> fn_inv = (v) => -v;
      TaskSet<int> ts = new TaskSet<int>();
      ts.Task = new Action<int>((t) =>
      {
        list.Modify(0, fn_inc);
        list.Modify(1, fn_inv);
      });

      List<Task> tasks = new List<Task>(n_tasks);
      for (int t = 0; t < n_tasks; t++)
        tasks.Add(TaskHandler.StartNew(ts, t));

      Task.WaitAll(tasks.ToArray());

      Assert.Multiple(() =>
      {
        Assert.That(list[0], Is.EqualTo(n_tasks));
        Assert.That(list[1], Is.EqualTo(n_tasks % 2 == 0 ? 1 : -1));
      });
    }
  }
}
