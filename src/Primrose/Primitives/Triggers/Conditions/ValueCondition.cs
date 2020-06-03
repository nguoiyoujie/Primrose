using Primrose.Primitives.Observables;
using System.Collections.Generic;

namespace Primrose.Primitives.Triggers
{
  /// <summary>Represents a condition that compares an ObservableValue against a value</summary>
  public class ValueCondition<T> : AObservableValueCondition<T>
  {
    /// <summary>The value to be matched against</summary>
    public T MatchValue { get; private set; }

    /// <summary>The comparison type</summary>
    public MatchType Type { get; private set; }

    /// <summary>Creates a condition that compares an ObservableValue against a value</summary>
    /// <param name="observable">The ObservableValue to be matched</param>
    /// <param name="type">The comparison type</param>
    /// <param name="value">The value to be matched</param>
    public ValueCondition(ref ObservableValue<T> observable, MatchType type, T value) : base(ref observable)
    {
      MatchValue = value;
      Type = type;
    }

    private bool Compare(T val)
    {
      try
      {
        // Generic equality
        switch (Type)
        {
          case MatchType.EQUALS:
            return (val == null && MatchValue == null) || val.Equals(MatchValue);
          case MatchType.NOT_EQUALS:
            return (val == null && MatchValue != null) || !val.Equals(MatchValue);
          case MatchType.SAME_TYPE:
            return val.GetType() == MatchValue.GetType();
        }

        // Comparable
        int cmp = Comparer<T>.Default.Compare(val, MatchValue);

        switch (Type)
        {
          case MatchType.NUMERIC_EQUALS:
            return cmp == 0;
          case MatchType.NUMERIC_NOT_EQUALS:
            return cmp != 0;
          case MatchType.LESS_THAN:
            return cmp < 0;
          case MatchType.MORE_THAN:
            return cmp > 0;
          case MatchType.LESS_THAN_OR_EQUAL_TO:
            return cmp <= 0;
          case MatchType.MORE_THAN_OR_EQUAL_TO:
            return cmp >= 0;
        }

        // Strings
        string s_val = val?.ToString() ?? "";
        string s_m_value = MatchValue?.ToString() ?? "";

        switch (Type)
        {
          case MatchType.SAME_LENGTH:
            return s_val.Length == s_m_value.Length;
          case MatchType.LENGTH_SHORTER_THAN:
            return s_val.Length < s_m_value.Length;
          case MatchType.LENGTH_LONGER_THAN:
            return s_val.Length > s_m_value.Length;
          case MatchType.LENGTH_SHORTER_THAN_OR_EQUAL_TO:
            return s_val.Length <= s_m_value.Length;
          case MatchType.LENGTH_LONGER_THAN_OR_EQUAL_TO:
            return s_val.Length >= s_m_value.Length;

          case MatchType.CONTAINS:
            return s_val.Contains(s_m_value);
          case MatchType.IS_CONTAINED_BY:
            return s_m_value.Contains(s_val);
          case MatchType.STARTS_WITH:
            return s_val.StartsWith(s_m_value);
          case MatchType.ENDS_WITH:
            return s_val.EndsWith(s_m_value);

          case MatchType.CASE_INSENSITIVE_CONTAINS:
            return s_val.ToLower().Contains(s_m_value.ToLower());
          case MatchType.CASE_INSENSITIVE_IS_CONTAINED_BY:
            return s_m_value.ToLower().Contains(s_val.ToLower());
          case MatchType.CASE_INSENSITIVE_STARTS_WITH:
            return s_val.ToLower().StartsWith(s_m_value.ToLower());
          case MatchType.CASE_INSENSITIVE_ENDS_WITH:
            return s_val.ToLower().EndsWith(s_m_value.ToLower());
        }

        // unsupported match type
        return false;
      }
      catch { return false; }
    }

    /// <summary>Evaluates the condition</summary>
    public override bool Evaluate()
    {
      return Compare(ObservableValue.Value);
    }
  }
}
