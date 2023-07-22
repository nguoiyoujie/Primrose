using System.Collections.Generic;

namespace Primrose.Expressions
{
  internal class ValFunc< T1, T2, T3, T4, T5, T6> : IValFunc
  {
    private readonly FunctionDelegate<T1, T2, T3, T4, T5, T6> F;
    public ValFunc(FunctionDelegate<T1, T2, T3, T4, T5, T6> fn) { F = fn; }

    public object Func { get { return F; } }

    public Val Execute(ITracker caller, string _funcName, IContext c, IList<Val> p)
    {
      return F(c
        , ValParamContract.Convert<T1>(caller, _funcName, 1, p[0])
        , ValParamContract.Convert<T2>(caller, _funcName, 2, p[1])
        , ValParamContract.Convert<T3>(caller, _funcName, 3, p[2])
        , ValParamContract.Convert<T4>(caller, _funcName, 4, p[3])
        , ValParamContract.Convert<T5>(caller, _funcName, 5, p[4])
        , ValParamContract.Convert<T6>(caller, _funcName, 6, p[5])
        );
    }
  }
}
