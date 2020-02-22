﻿using Primrose.Primitives.Parsers;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>Gets a value from the INIFile </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public T GetValue<T>(string section, string key, T defaultValue)
    {
      return Parser.Parse(GetString(section, key, ""), defaultValue);
    }
  }
}
