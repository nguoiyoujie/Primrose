﻿using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
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

    /// <summary>The registry of functions based on the function name and the number of function parameters</summary>
    private Registry<Pair<string, int>, IValFunc> ValFuncs { get; } = new Registry<Pair<string, int>, IValFunc>();

    /// <summary>The list of function names contained in this context</summary>
    public List<string> ValFuncRef { get; } = new List<string>();

    /// <summary>Creates a new context for evaluating scripts</summary>
    public ContextBase()
    {
      ClearFunctions();
      DefineFunctions();
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
            throw new EvalException(caller, Resource.Strings.Error_EvalException_IncorrectParameters.F(name));
          else
            throw new EvalException(caller, Resource.Strings.Error_EvalException_FunctionNotFound.F(name));

        return vfs.Execute(caller, name, this, param);
      }
      return fd.Invoke(this, param);
    }

    /// <summary>Retrieves a function from the function list</summary>
    /// <param name="name">The function name</param>
    /// <param name="argnum">The number of arguments for the function</param>
    /// <returns></returns>
    public object GetFunction(string name, int argnum)
    {
      return ValFuncs.Get(new Pair<string, int>(name, argnum));
    }

    

    /// <summary>Resets the context and clears the script</summary>
    public virtual void Reset()
    {
      Scripts.Clear();
    }

    /// <summary>Adds a function to the context</summary>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc(string name, Func<IContext, Val> fn) { ValFuncs.Add(new Pair<string, int>(name, 0), new ValFunc(fn)); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1>(string name, Func<IContext, T1, Val> fn) { ValFuncs.Add(new Pair<string, int>(name, 1), new ValFunc<T1>(fn)); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2>(string name, Func<IContext, T1, T2, Val> fn) { ValFuncs.Add(new Pair<string, int>(name, 2), new ValFunc<T1, T2>(fn)); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3>(string name, Func<IContext, T1, T2, T3, Val> fn) { ValFuncs.Add(new Pair<string, int>(name, 3), new ValFunc<T1, T2, T3>(fn)); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <typeparam name="T4">The type of the fourth argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3, T4>(string name, Func<IContext, T1, T2, T3, T4, Val> fn) { ValFuncs.Add(new Pair<string, int>(name, 4), new ValFunc<T1, T2, T3, T4>(fn)); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <typeparam name="T4">The type of the fourth argument</typeparam>
    /// <typeparam name="T5">The type of the fifth argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3, T4, T5>(string name, Func<IContext, T1, T2, T3, T4, T5, Val> fn) { ValFuncs.Add(new Pair<string, int>(name, 5), new ValFunc<T1, T2, T3, T4, T5>(fn)); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Adds a parameterized function to the context</summary>
    /// <typeparam name="T1">The type of the first argument</typeparam>
    /// <typeparam name="T2">The type of the second argument</typeparam>
    /// <typeparam name="T3">The type of the third argument</typeparam>
    /// <typeparam name="T4">The type of the fourth argument</typeparam>
    /// <typeparam name="T5">The type of the fifth argument</typeparam>
    /// <typeparam name="T6">The type of the sixth argument</typeparam>
    /// <param name="name">The function name</param>
    /// <param name="fn">The function delegate</param>
    protected void AddFunc<T1, T2, T3, T4, T5, T6>(string name, Func<IContext, T1, T2, T3, T4, T5, T6, Val> fn) { ValFuncs.Add(new Pair<string, int>(name, 6), new ValFunc<T1, T2, T3, T4, T5, T6>(fn)); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

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
    protected void AddFunc<T1, T2, T3, T4, T5, T6, T7>(string name, Func<IContext, T1, T2, T3, T4, T5, T6, T7, Val> fn) { ValFuncs.Add(new Pair<string, int>(name, 7), new ValFunc<T1, T2, T3, T4, T5, T6, T7>(fn)); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

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
    protected void AddFunc<T1, T2, T3, T4, T5, T6, T7, T8>(string name, Func<IContext, T1, T2, T3, T4, T5, T6, T7, T8, Val> fn) { ValFuncs.Add(new Pair<string, int>(name, 8), new ValFunc<T1, T2, T3, T4, T5, T6, T7, T8>(fn)); if (!ValFuncRef.Contains(name)) ValFuncRef.Add(name); }

    /// <summary>Clears all function definitions in the context</summary>
    protected virtual void ClearFunctions()
    {
      ValFuncs.Clear();
      Functions.Clear();
      ValFuncRef.Clear();

      // derived classes: add definitions here
    }

    /// <summary>Provides function definitions to the context</summary>
    protected virtual void DefineFunctions() { }
  }
}
