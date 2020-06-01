using NUnit.Framework;
using Primrose.Primitives.Factories;
using Primrose.Primitives.Observables;

namespace Primrose.UnitTests.Collections
{
  [TestFixture]
  public class ObservableRegistry
  {
    [TestCase(0, 0, 0)]
    [TestCase(0, 0, -1, 0, 0, -1)]
    [TestCase(1, 2, 3, 4)]
    [TestCase(-1, -1, 2, 3, 0, 5)]
    [TestCase(1, 3, 0, -1, 0, 0, 1)]
    public void ObservableRegistry_Events(params int[] seq)
    {
      int obs_event = 0;
      int add_event = 0;
      int rmv_event = 0;
      int chg_event = 0;

      ObservableRegistry<int, int> list = new ObservableRegistry<int, int>(new Registry<int, int>());
      list.RegistryChanged += (v1, v0) => obs_event++;
      list.KeyAdded += (v1, v0) => add_event++;
      list.KeyRemoved += (v1, v0) => rmv_event++;
      list.ValueChanged += (v1, v0) => chg_event++;

      int expected_sum = 0;
      int expected_count = 0;
      for(int i = 0; i < seq.Length; i++)
      {
        int s = seq[i];

        // negative means add, then delete
        // 0 means add
        // positive x means add, then change x times

        if (s < 0)
        {
          list.Add(i, 0);
          list.Remove(i);
        }
        else if (s == 0)
        {
          list.Add(i, 0);
          expected_count++;
        }
        else
        {
          list.Add(i, 0);
          for (int n = 0; n < s; n++)
            list[i]++;
          expected_count++;
          expected_sum += s;
        }
      }

      Assert.Multiple(() =>
      {
        Assert.That(list.Count, Is.EqualTo(expected_count));

        int sum = 0;
        foreach (int i in list.GetValues())
          sum += i;
        Assert.That(sum, Is.EqualTo(expected_sum));

        Assert.That(obs_event, Is.EqualTo(0));
        list.Registry = new Registry<int, int>();
        Assert.That(obs_event, Is.EqualTo(1));
      });
    }
  }
}
