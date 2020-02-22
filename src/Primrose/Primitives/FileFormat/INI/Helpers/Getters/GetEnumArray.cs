using Primrose.Primitives;
using Primrose.Primitives.Parsers;
using System;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Gets an enum[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public T GetEnumArray<T>(string section, string key, T defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }
  }
}
