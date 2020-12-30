

using System.Collections.Generic;

namespace Primrose.Primitives.Observables
{
  /// <summary>
  /// A wrapper for binding modification events to a variable
  /// </summary>
  /// <typeparam name="T">The encapsulated type</typeparam>
  public class ObservableValue<T>
  {
    private T _val;

    /// <summary>Creates an ObservableValue with an initialValue. The initial value does not trigger events</summary>
    /// <param name="initialValue"></param>
    public ObservableValue(T initialValue)
    {
      _val = initialValue;
      _valueChanged = default;
    }

    /// <summary>
    /// The encapsulated value
    /// </summary>
    public T Value
    {
      get { return _val; }
      set
      {
        T old = _val;
        _val = value;
        if ((_val == null && old != null) || !(_val != null && EqualityComparer<T>.Default.Equals(_val, old)))
          _valueChanged.Invoke(value, old);
      }
    }

#pragma warning disable IDE0044 // Add readonly modifier
    private ChangeEvent<T> _valueChanged;
#pragma warning restore IDE0044 // Add readonly modifier

    /// <summary>Represents the set of functions to be called when a value is changed</summary>
    public event ChangeEventDelegate<T> ValueChanged { add { _valueChanged.Ev += value; } remove { _valueChanged.Ev -= value; } }
  }
}
