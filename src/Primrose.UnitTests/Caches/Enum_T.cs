using NUnit.Framework;
using Primrose.Primitives.Cache;
using System;

namespace Primrose.UnitTests
{
  [TestFixture]
  public class Enum_T
  {
    private enum TestEnum { TEST, NOTEST, ABC, RANDOM = 8 };

    [TestCase]
    public void Enum_GetEnumName()
    {
      foreach (TestEnum t in Enum.GetValues(typeof(TestEnum)))
      {
        string s = t.ToString();
        Assert.That(s, Is.EqualTo(Enum<TestEnum>.GetName(t)));
        Assert.That(t, Is.EqualTo(Enum<TestEnum>.Parse(s)));
        Assert.That(Enum<TestEnum>.TryParse(s, out TestEnum v), Is.True);
        Assert.That(t, Is.EqualTo(v));
      }

      Assert.That(Enum<TestEnum>.TryParse("DOES_NOT_EXIST", out _), Is.False);
    }
  }
}
