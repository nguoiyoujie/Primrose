using System;

namespace Primrose.Expressions
{
  internal class ValFunc<T> : IValFunc
  {
    private readonly Func<IContext, T, Val> F;

    public ValFunc(Func<IContext, T, Val> fn) { F = fn; }

    public object Func { get { return F; } }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c, ValParamContract.Convert<T>(caller, _funcName, 1, p[0]));
    }
  }
}
