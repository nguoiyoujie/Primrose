using System.Collections.Generic;

namespace Primrose.Primitives.Observables
{
  /// <summary>
  /// A delegate representing a change in value
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="newValue">Represents the new value</param>
  /// <param name="oldValue">Represents the old value</param>
  public delegate void ChangeEventDelegate<T>(T newValue, T oldValue);

  internal struct ChangeEvent<T>
  {
    internal event ChangeEventDelegate<T> Ev;

    internal void Invoke(T newV, T oldV)
    {
      if ((newV == null && oldV != null) || !(newV != null && EqualityComparer<T>.Default.Equals(newV, oldV)))
        Ev?.Invoke(newV, oldV);
    }
  }
}
