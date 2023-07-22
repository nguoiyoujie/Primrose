namespace Primrose
{
  /// <summary>Fixed arrays intended for reuse where an array would otherwise be allocated. Modify at your own risk!</summary>
  public static class ArrayConstants
  {
    /// <summary>An array with a single null character</summary>
    public readonly static char[] Null = new char[] { '\0' };

    /// <summary>An array with a single comma character</summary>
    public static readonly char[] Comma = new char[] { ',' };

    /// <summary>An array with a single period character</summary>
    public static readonly char[] Period = new char[] { '.' };

    /// <summary>An array with the brace characters</summary>
    public static readonly char[] Braces = new char[] { '{', '}' };

    /// <summary>An array with a single space character</summary>
    public static readonly char[] Space = new char[] { ' ' };

    /// <summary>An array with a list quote characters</summary>
    public static readonly char[] Quotes = new char[] { '\'', '"' };

    /// <summary>An array with a list of newline characters</summary>
    public static readonly char[] NewLineChar = new char[] { '\r', '\n' };

    /// <summary>An array with a list of newline character combinations</summary>
    public static readonly string[] NewLines = new string[] { "\r\n", "\r", "\n" };

    /// <summary>An array with a list of decimal characters in order of value</summary>
    public static readonly char[] DecimalDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    /// <summary>An array with a list of hexadecimal characters in order of value</summary>
    public static readonly char[] HexadecimalDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

  }
}
