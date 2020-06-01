using NUnit.Framework;
using Primrose.Primitives;
using System;
using System.Collections.Generic;

namespace Primrose.UnitTests
{
  [TestFixture]
  public class EnumNames
  {
    private enum TestEnum { TEST, NOTEST, ABC, RANDOM = 8 };

    [TestCase]
    public void Enum_GetEnumName()
    {
      foreach (TestEnum t in Enum.GetValues(typeof(TestEnum)))
        Assert.AreEqual(t.ToString(), t.GetEnumName());
    }
  }
}
