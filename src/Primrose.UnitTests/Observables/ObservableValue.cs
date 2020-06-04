using NUnit.Framework;
using Primrose.Primitives.Observables;
using System.Collections.Generic;

namespace Primrose.UnitTests.Collections
{
  [TestFixture]
  public class ObservableValue
  {
    [TestCase(0, 0, 0, 0)]
    [TestCase(3, 0, -1, 0, 0, -1)]
    [TestCase(4, 1, 2, 3, 4, 5)]
    [TestCase(1, -1, -1, -1, 1, 1)]
    [TestCase(3, 3, 2, 2, 3, 3, 4)]
    public void ObservableValue_Events(int expected_changes, int initial_value, params int[] seq)
    {
      int chg_event = 0;

      ObservableValue<int> val = new ObservableValue<int>(initial_value);
      val.ValueChanged += (v1, v0) => chg_event++;

      foreach (int s in seq)
      {
        val.Value = s;
      }

      Assert.That(chg_event, Is.EqualTo(expected_changes));
    }
  }
}
