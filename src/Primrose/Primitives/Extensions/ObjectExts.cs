namespace Primrose.Primitives.Extensions
{
  /// <summary>
  /// Provides extension methods for object values
  /// </summary>
  public static class ObjectExts
  {
    /// <summary>Checks if a value evaluates to True or False</summary>
    /// <param name="value">The object</param>
    /// <returns>A boolean value</returns>
    public static bool ToBool(this object value)
    {
      return !(value == null
              || value.ToString().Length == 0
              || "0".Equals(value.ToString().ToLower())
              || "false".Equals(value.ToString().ToLower())
              || "no".Equals(value.ToString().ToLower())
              );
    }

    /// <summary>Creates a new object using the default constructor if the value is null</summary>
    /// <param name="value">The object</param>
    /// <returns>A boolean value</returns>
    public static void CreateNewIfNull<T>(ref T value) where T : new()
    {
      if (value == null)
      {
        value = new T();
      }
    }
  }
}
