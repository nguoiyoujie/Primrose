﻿using Primrose.Primitives.Parsers;
using Primrose.Primitives.ValueTypes;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Gets a float4 value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public float4 GetFloat4(string section, string key, float4 defaultValue = default(float4))
    {
      return Parser.Parse(GetString(section, key, ""), defaultValue);
    }
  }
}