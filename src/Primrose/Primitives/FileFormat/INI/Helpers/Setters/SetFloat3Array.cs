﻿using Primrose.Primitives.Parsers;
using Primrose.Primitives.ValueTypes;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Sets a float3[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetFloat3Array(string section, string key, float3[] list) { SetString(section, key, Parser.Write(list)); }
  }
}