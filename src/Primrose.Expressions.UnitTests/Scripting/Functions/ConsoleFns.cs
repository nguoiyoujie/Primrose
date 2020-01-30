using System;

namespace Primrose.Expressions.UnitTests.Scripting
{
  public static class ConsoleFns
  {
    public static Val Write(IContext context, Val val)
    {
      Console.Write((string)val);
        return Val.NULL;
    }

    public static Val WriteLine(IContext context, Val val)
    {
      Console.WriteLine((string)val);
      return Val.NULL;
    }
  }
}
