namespace Primrose.Primitives.Extensions
{
  /// <summary>
  /// Provides extension methods for float values
  /// </summary>
  public static class NumericExts
  {
    /// <summary>
    /// Returns a value at most max_delta value closer to a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="max_delta">The max_delta</param>
    /// <returns></returns>
    public static float Creep(this float value, float target, float max_delta)
    {
      return (value < target)
        ? target.Min(value + max_delta)
        : target.Max(value - max_delta);
    }

    /// <summary>
    /// Returns a value linearly interpolated towards a target
    /// </summary>
    /// <param name="value">The starting value</param>
    /// <param name="target">The target value</param>
    /// <param name="frac">The fraction to be interpolated towards the target point</param>
    /// <returns></returns>
    public static float Lerp(this float value, float target, float frac)
    {
      return value + (target - value) * frac;
    }


    /// <summary>
    /// Returns the result of (value % (max - min)), scaled so that lies between min and max
    /// </summary>
    /// <param name="value">The input value</param>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>(value % (max - min)), scaled so that lies between min and max</returns>
    public static float Modulus(this float value, float min, float max)
    {
      if (max == min)
      {
        value = min;
        return value;
      }
      else if (max < min)
      {
        float temp = max;
        max = min;
        min = temp;
      }

      value %= max - min;

      if (value > max)
        value -= max - min;
      else if (value < min)
        value += max - min;

      return value;
    }

    /// <summary>
    /// Returns the result of (value % (max - min)), scaled so that lies between min and max
    /// </summary>
    /// <param name="value">The input value</param>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <returns>(value % (max - min)), scaled so that lies between min and max</returns>
    public static int Modulus(this int value, int min, int max)
    {
      if (max == min)
      {
        value = min;
        return value;
      }
      else if (max < min)
      {
        int temp = max;
        max = min;
        min = temp;
      }

      value %= max - min;

      int min_r = min % (max - min);
      if (min_r < 0)
      {
        min_r += max - min;
      }
      value = min - min_r + value;
      if (value < min)
      {
        value += max - min;
      }

      return value;
    }
  }
}
