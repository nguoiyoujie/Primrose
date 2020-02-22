using Primrose.Primitives;
using Primrose.Primitives.Parsers;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Gets a long[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public long[] GetLongArray(string section, string key, long[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }
  }
}
