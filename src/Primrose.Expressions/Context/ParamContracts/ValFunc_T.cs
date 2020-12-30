using System;

namespace Primrose.Expressions
{
  internal class ValFunc<T> : IValFunc
  {
    private readonly FunctionDelegate<T> F;

    public ValFunc(FunctionDelegate<T> fn) { F = fn; }

    public object Func { get { return F; } }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c, ValParamContract.Convert<T>(caller, _funcName, 1, p[0]));
    }
  }
}
