using Primrose.Primitives;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Sets an enum value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetEnum<T>(string section, string key, T value) { SetString(section, key, value.ToString().Replace(", ", "|")); }
  }
}
