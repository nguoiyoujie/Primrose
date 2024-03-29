﻿using System.Collections.Generic;

namespace Primrose.Expressions
{
  internal class ValFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IValFunc
  {
    private readonly FunctionDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> F;
    public ValFunc(FunctionDelegate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> fn) { F = fn; }

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
        , ValParamContract.Convert<T7>(caller, _funcName, 7, p[6])
        , ValParamContract.Convert<T8>(caller, _funcName, 8, p[7])
        , ValParamContract.Convert<T9>(caller, _funcName, 9, p[8])
        , ValParamContract.Convert<T10>(caller, _funcName, 10, p[9])
        , ValParamContract.Convert<T11>(caller, _funcName, 11, p[10])
        , ValParamContract.Convert<T12>(caller, _funcName, 12, p[11])
        , ValParamContract.Convert<T13>(caller, _funcName, 13, p[12])
        , ValParamContract.Convert<T14>(caller, _funcName, 14, p[13])
        );
    }
  }
}
