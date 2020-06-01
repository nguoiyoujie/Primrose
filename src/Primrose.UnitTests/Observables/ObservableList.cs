using NUnit.Framework;
using Primrose.Primitives.Observables;
using System.Collections.Generic;

namespace Primrose.UnitTests.Collections
{
  [TestFixture]
  public class ObservableList
  {
    [TestCase(0, 0, 0)]
    [TestCase(0, 0, -1, 0, 0, -1)]
    [TestCase(1, 2, 3, 4)]
    [TestCase(-1, -1, 2, 3, 0, 5)]
    [TestCase(1, 3, 0, -1, 0, 0, 1)]
    public void ObservableList_Events(params int[] seq)
    {
      int obs_event = 0;
      int add_event = 0;
      int rmv_event = 0;
      int chg_event = 0;

      ObservableList<List<int>, int> list = new ObservableList<List<int>, int>(new List<int>());
      list.ListChanged += (v1, v0) => obs_event++;
      list.ItemAdded += (v1, v0) => add_event++;
      list.ItemRemoved += (v1, v0) => rmv_event++;
      list.ItemChanged += (v1, v0) => chg_event++;

      int expected_sum = 0;
      int expected_count = 0;
      foreach (int s in seq)
      {
        // negative means add, then delete
        // 0 means add
        // positive x means add, then change x times

        if (s < 0)
        {
          list.Add(0);
          list.RemoveAt(list.Count - 1);
        }
        else if (s == 0)
        {
          list.Add(0);
          expected_count++;
        }
        else
        {
          list.Add(0);
          for (int i = 0; i < s; i++)
            list[list.Count - 1]++;
          expected_count++;
          expected_sum += s;
        }
      }

      Assert.Multiple(() =>
      {
        Assert.That(list.Count, Is.EqualTo(expected_count));

        int sum = 0;
        foreach (int i in list)
          sum += i;
        Assert.That(sum, Is.EqualTo(expected_sum));

        Assert.That(obs_event, Is.EqualTo(0));
        list.List = new List<int>();
        Assert.That(obs_event, Is.EqualTo(1));
      });
    }
  }
}
