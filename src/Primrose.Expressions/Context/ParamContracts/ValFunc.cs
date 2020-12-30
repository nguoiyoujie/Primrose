using System;

namespace Primrose.Expressions
{
  internal class ValFunc : IValFunc
  {
    private readonly FunctionDelegate F;
    public ValFunc(FunctionDelegate fn) { F = fn; }

    public object Func { get { return F; } }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c);
    }
  }
}
