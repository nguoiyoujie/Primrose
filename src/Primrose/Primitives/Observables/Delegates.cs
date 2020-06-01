using Primrose.Primitives.Factories;

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
    private event ChangeEventDelegate<T> _ev;

    internal event ChangeEventDelegate<T> Ev
    {
      add { _ev += value; }
      remove { _ev -= value; }
    }

    internal void Invoke(T newV, T oldV)
    {
      if ((newV == null && oldV != null) || !(newV != null && newV.Equals(oldV)))
        _ev?.Invoke(newV, oldV);
    }
  }
}
