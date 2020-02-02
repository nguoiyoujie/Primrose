using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System.Collections.Generic;

namespace Primrose.Expressions
{
  public delegate Val FunctionDelegate(ContextBase context, Val[] param);

  public class ContextBase : IContext
  {
    public readonly Registry<FunctionDelegate> Functions = new Registry<FunctionDelegate>();

    public Script.Registry ScriptRegistry { get; } = new Script.Registry();
    public Registry<Pair<string, int>, IValFunc> ValFuncs { get; } = new Registry<Pair<string, int>, IValFunc>();
    public List<string> ValFuncRef { get; } = new List<string>();

    public ContextBase()
    {
      DefineFunc();
    }

    public Val RunFunction(ITracker caller, string _funcName, Val[] param)
    {
      FunctionDelegate fd = Functions.Get(_funcName);
      if (fd == null)
      {
        IValFunc vfs = ValFuncs.Get(new Pair<string, int>(_funcName, param.Length));
        if (vfs == null)
          if (ValFuncRef.Contains(_funcName))
            throw new EvalException(caller, "Incorrect number/type of parameters supplied to function '{0}'!".F(_funcName));
          else
            throw new EvalException(caller, "The function '{0}' does not exist!".F(_funcName));

        return vfs.Execute(caller, _funcName, this, param);
      }
      return fd.Invoke(this, param);
    }

    public virtual void Reset() { }

    protected void AddFunc(string name, ValFunc fn) { ValFuncs.Add(new Pair<string, int>(name, 0), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }
    protected void AddFunc<T1>(string name, ValFunc< T1> fn) { ValFuncs.Add(new Pair<string, int>(name, 1), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }
    protected void AddFunc<T1, T2>(string name, ValFunc< T1, T2> fn) { ValFuncs.Add(new Pair<string, int>(name, 2), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }
    protected void AddFunc<T1, T2, T3>(string name, ValFunc< T1, T2, T3> fn) { ValFuncs.Add(new Pair<string, int>(name, 3), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }
    protected void AddFunc<T1, T2, T3, T4>(string name, ValFunc< T1, T2, T3, T4> fn) { ValFuncs.Add(new Pair<string, int>(name, 4), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }
    protected void AddFunc<T1, T2, T3, T4, T5>(string name, ValFunc< T1, T2, T3, T4, T5> fn) { ValFuncs.Add(new Pair<string, int>(name, 5), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }
    protected void AddFunc<T1, T2, T3, T4, T5, T6>(string name, ValFunc< T1, T2, T3, T4, T5, T6> fn) { ValFuncs.Add(new Pair<string, int>(name, 6), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }
    protected void AddFunc<T1, T2, T3, T4, T5, T6, T7>(string name, ValFunc< T1, T2, T3, T4, T5, T6, T7> fn) { ValFuncs.Add(new Pair<string, int>(name, 7), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }
    protected void AddFunc<T1, T2, T3, T4, T5, T6, T7, T8>(string name, ValFunc< T1, T2, T3, T4, T5, T6, T7, T8> fn) { ValFuncs.Add(new Pair<string, int>(name, 8), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    public virtual void DefineFunc()
    {
      ValFuncs.Clear();
      Functions.Clear();
      ValFuncRef.Clear();
    }
  }
}
