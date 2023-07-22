using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives.Stocks
{
  /// <summary>Defines a container for holding a value.</summary>
  public class Stock<T> where T : struct, IComparable<T>
  {
    private T _value;
    private T _minimum;
    private T _maximum;
    private bool _hasMinimum;
    private bool _hasMaximum;

    /// <summary>Defines whether this stock container has a lower bound for the value</summary>
    public bool HasMinimum { get { return _hasMinimum; } set { _hasMinimum = value; if (value) { Value = _value; } } }

    /// <summary>Defines whether this stock container has a upper bound for the value</summary>
    public bool HasMaximum { get { return _hasMaximum; } set { _hasMaximum = value; if (value) { Value = _value; } } }

    /// <summary>The value expressed in the stock</summary>
    public T Value
    { 
      get { return _value; } 
      set 
      { 
        if (HasMinimum)
        {
          value = value.Max(_minimum);
        }

        if (HasMaximum)
        {
          value = value.Min(_maximum);
        }
        _value = value;
      }
    }

    /// <summary>The lower bound value of the stock, used only if HasMinimum is true</summary>
    public T Minimum { get { return _minimum; } set { _minimum = value; if (_hasMinimum) { Value = _value; } } }

    /// <summary>The upper bound value of the stock, used only if HasMaximum is true</summary>
    public T Maximum { get { return _maximum; } set { _maximum = value; if (_hasMaximum) { Value = _value; } } }

    /// <summary>Returns true if the HasMinimum is enabled and the value is at the lower bound value</summary>
    public bool IsAtMinimum()
    {
      return HasMinimum && Value.CompareTo(Minimum) <= 0;
    }

    /// <summary>Returns true if the HasMaximum is enabled and the value is at the upper bound value</summary>
    public bool IsAtMaximum()
    {
      return HasMaximum && Value.CompareTo(Maximum) >= 0;
    }
  }
}
