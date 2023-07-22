using Primrose.Primitives.Factories;
using System.Collections.Generic;
using System.Text;

namespace Primrose.Primitives.Strings
{
  /// <summary>
  /// A class for storing format instructions, for the generation of strings
  /// </summary>
  public class StringCompiler
  {
    /// <summary>
    /// The items to be formatted
    /// </summary>
    public readonly List<IFormatter> Items = new List<IFormatter>();

    /// <summary>
    /// Creates a string from the list of format definitions
    /// </summary>
    /// <returns></returns>
    public string CreateString()
    {
      StringBuilder sb = ObjectPool<StringBuilder>.GetStaticPool().GetNew();
      Append(sb);
      string result = sb.ToString();
      ObjectPool<StringBuilder>.GetStaticPool().Return(sb);
      return result;
    }

    private void Append(StringBuilder sb)
    {
      int expected_length = sb.Length;
      foreach (IFormatter f in Items)
      {
        expected_length += f.ExpectedLength;
      }

      sb.EnsureCapacity(expected_length);
      foreach (IFormatter f in Items)
      {
        f.Format(sb);
      }
    }
  }
}
