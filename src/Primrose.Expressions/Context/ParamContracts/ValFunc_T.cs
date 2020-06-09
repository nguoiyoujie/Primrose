using System;

namespace Primrose.Expressions
{
  /// <summary>Defines a script function</summary>
  /// <typeparam name="T">The type of the first argument</typeparam>
  public class ValFunc<T> : IValFunc
  {
    Func<IContext, T, Val> F;

    /// <summary>Creates a script function</summary>
    /// <param name="fn">The function</param>
    public ValFunc(Func<IContext, T, Val> fn) { F = fn; }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c, ValParamContract.Convert<T>(caller, _funcName, 1, p[0]));
    }
  }
}
