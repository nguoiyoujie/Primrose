using NUnit.Framework;

namespace Primrose.Expressions.UnitTests.Scripting
{
  public static class AssertFns
  {
    public static Val AreEqual(IContext context, Val v1, Val v2)
    {
      if (v1.Type == ValType.FLOAT || v2.Type == ValType.FLOAT)
        Assert.That((float)v1, Is.EqualTo((float)v2).Within(0.00001f));
      else if (v1.Type == ValType.INT || v2.Type == ValType.INT)
        Assert.AreEqual((int)v1, (int)v2);
      else
        Assert.AreEqual(v1.Value, v2.Value);
      return Val.NULL;
    }

    public static Val AreNotEqual(IContext context, Val v1, Val v2)
    {
      if (v1.Type == ValType.FLOAT || v2.Type == ValType.FLOAT)
        Assert.That((float)v1, Is.Not.EqualTo((float)v2).Within(0.00001f));
      else if (v1.Type == ValType.INT || v2.Type == ValType.INT)
        Assert.AreNotEqual((int)v1, (int)v2);
      else
        Assert.AreNotEqual(v1.Value, v2.Value); return Val.NULL;
    }
  }
}
