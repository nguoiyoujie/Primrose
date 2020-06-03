using Primrose.Primitives.Observables;
using System.Collections.Generic;

namespace Primrose.Primitives.Triggers
{
  /*
  /// <summary>Represents a condition to be fulfilled</summary>
  public abstract class ASubscriberCondition<T> : ACondition
  {
    private List<ObservableValue<T>> _subscribes = new List<ObservableValue<T>>();

    /// <summary>The value to be matched against</summary>
    public T Value { get; private set; }

    /// <summary>Checks the value on change to determine if the condition is fulfilled</summary>
    /// <param name="newValue">The new value</param>
    /// <param name="oldValue">The old value</param>
    protected void Check(T newValue, T oldValue)
    {
      Value = newValue;
      UpdateResult();
    }

    /// <summary>Creates a condition to be fulfilled</summary>
    public ASubscriberCondition() { }

    /// <summary>Creates a condition to be fulfilled with an initial ObservableValue target</summary>
    /// <param name="target">The ObservableValue to be monitored</param>
    public ASubscriberCondition(ref ObservableValue<T> target)
    {
      Subscribe(ref target);
    }

    /// <summary>Subscribes the condition to an ObservableValue target, so that changes to the target will trigger the condition</summary>
    /// <param name="target">The ObservableValue to be monitored</param>
    public void Subscribe(ref ObservableValue<T> target)
    {
      _subscribes.Add(target);
      target.ValueChanged += Check;
    }

    /// <summary>Removes an ObservableValue from the condition's trigger list</summary>
    /// <param name="target">The ObservableValue to be monitored</param>
    public void Unsubscribe(ref ObservableValue<T> target)
    {
      if (_subscribes.Contains(target))
      {
        _subscribes.Remove(target);
        target.ValueChanged -= Check;
      }
    }

    /// <summary>Clears all ObservableValue from the condition's trigger list</summary>
    public void UnsubscribeAll()
    {
      foreach (var s in _subscribes)
        s.ValueChanged -= Check;
      _subscribes.Clear();
    }
  }
  */
}
