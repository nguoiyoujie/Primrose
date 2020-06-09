using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System.Collections.Generic;

namespace Primrose.Expressions
{
  /// <summary>
  /// Defines a delegate representing a function expression
  /// </summary>
  /// <param name="context">The script context</param>
  /// <param name="param">The input parameters to the function</param>
  /// <returns>The result of the function</returns>
  public delegate Val FunctionDelegate(ContextBase context, Val[] param);

  /// <summary>Defines a context for evaluating scripts</summary>
  public class ContextBase : IContext
  {
    /// <summary>The collection of functions contained in this context</summary>
    public readonly Registry<FunctionDelegate> Functions = new Registry<FunctionDelegate>();

    /// <summary>The scripts contained in this context</summary>
    public Script.Registry Scripts { get; } = new Script.Registry();

    /// <summary>The list of functions</summary>
    public Registry<Pair<string, int>, IValFunc> ValFuncs { get; } = new Registry<Pair<string, int>, IValFunc>();

    /// <summary>The list of function names</summary>
    public List<string> ValFuncRef { get; } = new List<string>();

    /// <summary>Creates a new context for evaluating scripts</summary>
    public ContextBase()
    {
      DefineFunc();
    }

    /// <summary>Performs evaluation of the global script</summary>
    public void RunGlobalScript()
    {
      Scripts.Global?.Run(this);
    }

    /// <summary>Performs evaluation of a script</summary>

    /// <param name="name">The name of the script to run</param>
    public void RunScript(string name)
    {
      Scripts.Get(name)?.Run(this);
    }

    /// <summary>Performs evaluation of a function</summary>

    /// <param name="caller">The tracking object calling the function</param>
    /// <param name="name">The function name</param>
    /// <param name="param">The function parameters</param>
    /// <returns></returns>
    public Val RunFunction(ITracker caller, string name, Val[] param)
    {
      FunctionDelegate fd = Functions.Get(name);
      if (fd == null)
      {
        IValFunc vfs = ValFuncs.Get(new Pair<string, int>(name, param.Length));
        if (vfs == null)
          if (ValFuncRef.Contains(name))
            throw new EvalException(caller, "Incorrect number/type of parameters supplied to function '{0}'!".F(name));
          else
            throw new EvalException(caller, "The function '{0}' does not exist!".F(name));

        return vfs.Execute(caller, name, this, param);
      }
      return fd.Invoke(this, param);
    }

    /// <summary>Resets the context and clears the script</summary>
    public virtual void Reset()
    {
      Scripts.Clear();
    }

    /// <summary>Adds a function to the context</summary>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc(string name, ValFunc fn) { ValFuncs.Add(new Pair<string, int>(name, 0), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1>(string name, ValFunc<T1> fn) { ValFuncs.Add(new Pair<string, int>(name, 1), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2>(string name, ValFunc<T1, T2> fn) { ValFuncs.Add(new Pair<string, int>(name, 2), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3>(string name, ValFunc<T1, T2, T3> fn) { ValFuncs.Add(new Pair<string, int>(name, 3), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <typeparam name="T4">The type of the fourth argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3, T4>(string name, ValFunc<T1, T2, T3, T4> fn) { ValFuncs.Add(new Pair<string, int>(name, 4), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <typeparam name="T4">The type of the fourth argument</typeparam>
    /// <typeparam name="T5">The type of the fifth argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3, T4, T5>(string name, ValFunc<T1, T2, T3, T4, T5> fn) { ValFuncs.Add(new Pair<string, int>(name, 5), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <typeparam name="T4">The type of the fourth argument</typeparam>
    /// <typeparam name="T5">The type of the fifth argument</typeparam>
    /// <typeparam name="T6">The type of the sixth argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3, T4, T5, T6>(string name, ValFunc<T1, T2, T3, T4, T5, T6> fn) { ValFuncs.Add(new Pair<string, int>(name, 6), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <typeparam name="T4">The type of the fourth argument</typeparam>
    /// <typeparam name="T5">The type of the fifth argument</typeparam>
    /// <typeparam name="T6">The type of the sixth argument</typeparam>
    /// <typeparam name="T7">The type of the seventh argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3, T4, T5, T6, T7>(string name, ValFunc<T1, T2, T3, T4, T5, T6, T7> fn) { ValFuncs.Add(new Pair<string, int>(name, 7), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <typeparam name="T4">The type of the fourth argument</typeparam>
    /// <typeparam name="T5">The type of the fifth argument</typeparam>
    /// <typeparam name="T6">The type of the sixth argument</typeparam>
    /// <typeparam name="T7">The type of the seventh argument</typeparam>
    /// <typeparam name="T8">The type of the eighth argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3, T4, T5, T6, T7, T8>(string name, ValFunc<T1, T2, T3, T4, T5, T6, T7, T8> fn) { ValFuncs.Add(new Pair<string, int>(name, 8), fn); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Provides function definitions to the context</summary>
    public virtual void DefineFunc()
    {
      ValFuncs.Clear();
      Functions.Clear();
      ValFuncRef.Clear();

      // derived classes: add definitions here
    }
  }
}
