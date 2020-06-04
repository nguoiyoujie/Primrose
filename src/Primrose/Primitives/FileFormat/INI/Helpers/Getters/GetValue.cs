using Primrose.Primitives.Parsers;
using System.Runtime.CompilerServices;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>Gets a value from the INIFile</summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belonging to the section and key in the INIFile. If the key does not exist, returns defaultValue</returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetValue<T>(string section, string key, T defaultValue = default(T))
    {
      return Parser.Parse(GetString(section, key), defaultValue);
    }
  }
}
