using Primrose.Primitives.Observables;
using System;

namespace Primrose.Primitives.Triggers
{
  /// <summary>Represents a condition that compares an ObservableValue against a function</summary>
  public class FuncCondition<T> : AObservableValueCondition<T>
  {
    /// <summary>The function to determine if a value satisfies the condition</summary>
    public Func<T, bool> Func { get; private set; }

    /// <summary>Creates a condition that compares an ObservableValue against a value</summary>
    /// <param name="observable">The ObservableValue to be matched</param>
    /// <param name="func">The function to determine if a value satisfies the condition</param>
    public FuncCondition(ref ObservableValue<T> observable, Func<T, bool> func) : base(ref observable)
    {
      Func = func;
    }

    /// <summary>Creates a condition that compares an ObservableValue against a value</summary>
    /// <param name="func">The function to determine if a value satisfies the condition</param>
    /// <param name="target">The ObservableValue to be monitored</param>
    public FuncCondition(Func<T, bool> func, ref ObservableValue<T> target) : base(ref target)
    {
      Func = func;
    }

    /// <summary>Evaluates the condition</summary>
    public override bool Evaluate()
    {
      return Func.Invoke(ObservableValue.Value);
    }
  }
}
