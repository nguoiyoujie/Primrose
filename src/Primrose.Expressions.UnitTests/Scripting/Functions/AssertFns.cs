using NUnit.Framework;

namespace Primrose.Expressions.UnitTests.Scripting
{
  public static class AssertFns
  {
    public static Val AreEqual(IContext context, Val v1, Val v2)
    {
      Ops.ImplicitCast(ref v1, ref v2);

      if (v1.Type == typeof(float))
        Assert.That((float)v1, Is.EqualTo((float)v2).Within(0.00001f));
      else
        Assert.AreEqual(v1.Value, v2.Value);
      return Val.NULL;
    }

    public static Val AreNotEqual(IContext context, Val v1, Val v2)
    {
      Ops.ImplicitCast(ref v1, ref v2);

      if (v1.Type == typeof(float))
        Assert.That((float)v1, Is.Not.EqualTo((float)v2).Within(0.00001f));
      else
        Assert.AreNotEqual(v1.Value, v2.Value);
      return Val.NULL;
    }
  }
}
