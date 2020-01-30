﻿using System;

namespace Primrose.Expressions
{
  public class ValFunc< T1, T2> : IValFunc
     {
    Func<IContext, T1, T2, Val> F;
    public ValFunc(Func<IContext, T1, T2, Val> fn) { F = fn; }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c
        , ValParamContract.Convert<T1>(caller, _funcName, 1, p[0])
        , ValParamContract.Convert<T2>(caller, _funcName, 2, p[1])
        );
    }
  }
}
