using Primrose.Primitives.Observables;

namespace Primrose.Primitives.Triggers
{
  /// <summary>Represents a condition to be fulfilled</summary>
  public abstract class AObservableValueCondition<T> : ACondition
  {
    /// <summary>The ObservableValue to be matched</summary>
    public ObservableValue<T> ObservableValue { get; private set; }

    /// <summary>Creates a condition that compares an ObservableValue against a value</summary>
    /// <param name="observable">The ObservableValue to be matched</param>
    public AObservableValueCondition(ref ObservableValue<T> observable) : base()
    {
      ObservableValue = observable;
      ObservableValue.ValueChanged += (_, __) => UpdateResult();
    }
  }
}
