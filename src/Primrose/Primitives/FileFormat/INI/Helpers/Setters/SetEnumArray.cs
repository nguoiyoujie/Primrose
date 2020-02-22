using Primrose.Primitives.Parsers;
using System;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Sets an enum array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetEnumArray<T>(string section, string key, T list) { SetString(section, key, Parser.Write(list)); }
  }
}
