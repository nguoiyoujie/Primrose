using System;

namespace Primrose.Expressions
{
  internal class ValFunc : IValFunc
  {
    Func<IContext, Val> F;
    public ValFunc(Func<IContext, Val> fn) { F = fn; }

    public object Func { get { return F; } }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c);
    }
  }
}
