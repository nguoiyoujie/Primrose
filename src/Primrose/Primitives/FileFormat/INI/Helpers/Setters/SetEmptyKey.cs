using Primrose.Primitives.Extensions;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Sets an empty value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key</param>
    /// <param name="key">The key that will be set</param>
    public void SetEmptyKey(string section, string key)
    {
      if (!m_sections.ContainsKey(section))
        m_sections.Add(section, new INISection("[{0}]".F(section), this));

      m_sections[section].SetValue(key, null);
    }
  }
}
