namespace Primrose.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Gets a string value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belonging to the section and key in the INIFile. If the key does not exist, returns defaultValue</returns>
    public string GetString(string section, string key, string defaultValue = null)
    {
      return GetString(section, key, defaultValue, section);
    }

    private string GetString(string section, string key, string defaultValue, string firstsection)
    {
      if (m_sections.ContainsKey(section))
      {
        if (m_sections[section].HasKey(key))
          return m_sections[section].GetLine(key).Value;

        string val = defaultValue;
        foreach (string inherit in m_sections[section].Inherits)
        {
          if (inherit != firstsection
            && val == defaultValue
            && m_sections.ContainsKey(inherit))
          {
            val = GetString(inherit, key, defaultValue, firstsection);
          }
        }
        return val;
      }
      return defaultValue;
    }
  }
}
