using System;

namespace Primrose.Expressions
{
  internal class ValFunc_Dynamic : IValFunc
  {
    private readonly FunctionDelegateParam F;

    public ValFunc_Dynamic(FunctionDelegateParam fn) { F = fn; }

    public object Func { get { return F; } }

    public Val Execute(ITracker caller, string _funcName, IContext c, Val[] p)
    {
      return F(c, p);
    }
  }
}
