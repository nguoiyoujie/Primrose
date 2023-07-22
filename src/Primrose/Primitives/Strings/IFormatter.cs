using System.Text;

namespace Primrose.Primitives.Strings
{
  /// <summary>Represents a format instruction for generating a string from a value and parameters</summary>
  public interface IFormatter
  {
    /// <summary>Returns the expected length of the string produced by the value</summary>
    int ExpectedLength { get; }

    /// <summary>Returns the string representing the value</summary>
    string Format();

    /// <summary>Appends the string representing the value to a StringBuilder</summary>
    void Format(StringBuilder sb);
  }
}
