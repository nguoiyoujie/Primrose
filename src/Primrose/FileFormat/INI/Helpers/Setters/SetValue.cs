using Primrose.Primitives.Parsers;

namespace Primrose.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>Sets a value from the INIFile </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetValue<T>(string section, string key, T value) { SetString(section, key, Parser.Write(value)); }
  }
}
