using System;

namespace Primrose.Expressions
{
  public class ValFunc<T1, T2, T3, T4, T5> : IValFunc
  {
    Func<IContext, T1, T2, T3, T4, T5, Val> F;
    public ValFunc(Func<IContext, T1, T2, T3, T4, T5, Val> fn) { F = fn; }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c
        , ValParamContract.Convert<T1>(caller, _funcName, 1, p[0])
        , ValParamContract.Convert<T2>(caller, _funcName, 2, p[1])
        , ValParamContract.Convert<T3>(caller, _funcName, 3, p[2])
        , ValParamContract.Convert<T4>(caller, _funcName, 4, p[3])
        , ValParamContract.Convert<T5>(caller, _funcName, 5, p[4])
        );
    }
  }
}
