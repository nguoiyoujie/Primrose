using Primrose.Primitives.Factories;

namespace Primrose.Primitives.Cache
{
  /// <summary>Helper class to cache ToString invocations of a generic type</summary>
  public static class ToStringCache<T>
  {
    private static Registry<T, string> _toStr = new Registry<T, string>();

    /// <summary>The maximum number of entries stored in the cache before automatically generating Clear()</summary>
    public static int MaxEntries = short.MaxValue;

    /// <summary>
    /// Retrieves the string associated with the value.
    /// </summary>
    public static string Get(T value)
    {
      if (!_toStr.Contains(value))
      {
        if (_toStr.Count > MaxEntries)
        {
          Clear();
        }
        string s = value.ToString();
        _toStr[value] = s;
        return s;
      }
      return _toStr[value];
    }

    /// <summary>Clears the cache for the class</summary>
    public static void Clear()
    {
      _toStr.Clear();
    }
  }
}
