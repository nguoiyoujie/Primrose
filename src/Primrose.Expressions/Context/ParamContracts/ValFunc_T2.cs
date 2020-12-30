using System;

namespace Primrose.Expressions
{
  internal class ValFunc< T1, T2> : IValFunc
     {
    private readonly FunctionDelegate<T1, T2> F;
    public ValFunc(FunctionDelegate<T1, T2> fn) { F = fn; }

    public object Func { get { return F; } }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c
        , ValParamContract.Convert<T1>(caller, _funcName, 1, p[0])
        , ValParamContract.Convert<T2>(caller, _funcName, 2, p[1])
        );
    }
  }
}
