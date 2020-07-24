using System;

namespace Primrose.Expressions
{
  internal class ValFunc_Dynamic : IValFunc
  {
    private readonly Func<IContext, Val[], Val> F;

    public ValFunc_Dynamic(Func<IContext, Val[], Val> fn) { F = fn; }

    public object Func { get { return F; } }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c, p);
    }
  }
}
