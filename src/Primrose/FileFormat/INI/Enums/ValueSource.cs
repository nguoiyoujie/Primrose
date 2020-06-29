namespace Primrose.FileFormat.INI
{
  /// <summary>Defines where a desired value is stored in a [key=value] format</summary>
  public enum ValueSource
  {
    /// <summary>The desired value is taken from the key only, ignoring the value</summary>
    KEY_ONLY,

    /// <summary>The desired value is taken from the value only, ignoring all items with only the key</summary>
    VALUE_ONLY,

    /// <summary>The desired value is taken first from the value, then from the key if the value does not exist</summary>
    VALUE_OR_KEY
  }
}
