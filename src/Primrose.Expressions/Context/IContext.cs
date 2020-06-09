using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System.Collections.Generic;

namespace Primrose.Expressions
{
  /// <summary>
  /// Provides a context for the script
  /// </summary>
  public interface IContext
  {
    /// <summary>
    /// Runs a user defined function. An EvalException should be thrown if errors arise from the function.
    /// </summary>
    /// <param name="caller">The script object that called this function</param>
    /// <param name="fnname">The function name</param>
    /// <param name="param">The list of parameters</param>
    /// <returns></returns>
    Val RunFunction(ITracker caller, string fnname, Val[] param);

    /// <summary>The list of function names contained in this context</summary>
    List<string> ValFuncRef { get; }

    /// <summary>Retrieves a function from the function list</summary>
    object GetFunction(string name, int argnum);

    /// <summary>The scripts contained in this context</summary>
    Script.Registry Scripts { get; }
  }
}
