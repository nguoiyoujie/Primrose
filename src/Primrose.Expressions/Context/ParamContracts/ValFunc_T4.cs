using System;

namespace Primrose.Expressions
{
  internal class ValFunc<T1, T2, T3, T4> : IValFunc
  {
    private readonly FunctionDelegate<T1, T2, T3, T4> F;
    public ValFunc(FunctionDelegate<T1, T2, T3, T4> fn) { F = fn; }

    public object Func { get { return F; } }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c
        , ValParamContract.Convert<T1>(caller, _funcName, 1, p[0])
        , ValParamContract.Convert<T2>(caller, _funcName, 2, p[1])
        , ValParamContract.Convert<T3>(caller, _funcName, 3, p[2])
        , ValParamContract.Convert<T4>(caller, _funcName, 4, p[3])
        );
    }
  }
}
