using Primrose.Primitives.Extensions;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Sets a string value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetString(string section, string key, string value)
    {
      if (!m_sections.ContainsKey(section))
        m_sections.Add(section, new INISection("[{0}]".F(section), this));

      m_sections[section].SetValue(key, value);
    }
  }
}
