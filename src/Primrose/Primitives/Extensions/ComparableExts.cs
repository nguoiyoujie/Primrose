using System;
using System.Collections.Generic;

namespace Primrose.Primitives.Extensions
{
  /// <summary>
  /// Provides extension methods for generic comparable values
  /// </summary>
  public static class ComparableExts
  {
    /// <summary>
    /// Checks if a value equals the default, and provides a replacement value if so
    /// </summary>
    /// <param name="value">The input value</param>
    /// <param name="replacevalue">The replacement value</param>
    /// <returns>replacevalue if the input equals the default value of the type, the original input value otherwise</returns>
    public static T ReplaceIfDefault<T>(this T value, T replacevalue)
    {
      return Comparer<T>.Default.Compare(default(T), value) == 0 ? replacevalue : value;
    }

    /// <summary>
    /// Checks if a value satisfies a condition, and provides a replacement value if so
    /// </summary>
    /// <param name="value">The input value</param>
    /// <param name="replacevalue">The replacement value</param>
    /// <param name="condition">The condition function</param>
    /// <returns>replacevalue if the input satisfies the condition, the original input value otherwise</returns>
    public static T ReplaceIf<T>(this T value, T replacevalue, Predicate<T> condition)
    {
      if (condition == null) { throw new ArgumentNullException(nameof(condition)); }

      return condition(value) ? replacevalue : value;
    }

    /// <summary>
    /// Returns a value clamped between a minimum and a maximum
    /// </summary>
    /// <param name="value">The input value</param>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>min if the value is less than min, max is the value is more than max, value otherwise</returns>
    public static T Clamp<T>(this T value, T min, T max) where T : struct, IComparable<T>
    {
      return Max(Min(value, max), min);
    }

    /// <summary>
    /// Returns the maximum of two values
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value</param>
    /// <returns>The greater of the two values</returns>
    public static T Max<T>(this T value1, T value2) where T : struct, IComparable<T>
    {
      return (value1.CompareTo(value2) > 0)
        ? value1 
        : value2;
    }

    /// <summary>
    /// Returns the minimum of two values
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value</param>
    /// <returns>The smaller of the two values</returns>
    public static T Min<T>(this T value1, T value2) where T : struct, IComparable<T>
    {
      return (value1.CompareTo(value2) > 0)
        ? value2
        : value1;
    }

    /// <summary>
    /// Returns the maximum of two values, as determined by a comparer
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value</param>
    /// <param name="comparer">The comparer object used to compare the two values</param>
    /// <returns>The greater of the two values</returns>
    public static T Max<T>(this T value1, T value2, IComparer<T> comparer) where T : struct, IComparable<T>
    {
      if (comparer == null) { throw new ArgumentNullException(nameof(comparer)); }

      return (comparer.Compare(value1, value2) > 0)
        ? value1
        : value2;
    }

    /// <summary>
    /// Returns the minimum of two values, as determined by a comparer
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value</param>
    /// <param name="comparer">The comparer object used to compare the two values</param>
    /// <returns>The smaller of the two values</returns>
    public static T Min<T>(this T value1, T value2, IComparer<T> comparer) where T : struct, IComparable<T>
    {
      if (comparer == null) { throw new ArgumentNullException(nameof(comparer)); }

      return (comparer.Compare(value1, value2) > 0)
        ? value2
        : value1;
    }
  }
}
