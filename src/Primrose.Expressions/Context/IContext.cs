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

    /// <summary>
    /// Holds a registry of functions based on the function name and the number of function parameters
    /// </summary>
    Registry<Pair<string, int>, IValFunc> ValFuncs { get; }

    /// <summary>
    /// Holds a registry of functions based on the function name only
    /// </summary>
    List<string> ValFuncRef { get; }

    /// <summary>
    /// Holds the registry of scripts
    /// </summary>
    Script.Registry Scripts { get; }
  }
}
