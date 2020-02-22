using Primrose.Primitives.Parsers;
using Primrose.Primitives.ValueTypes;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Sets a float3 value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetFloat3(string section, string key, float3 value) { SetString(section, key, Parser.Write(value)); }
  }
}
