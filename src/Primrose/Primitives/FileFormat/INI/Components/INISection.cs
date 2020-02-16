using System.Collections.Generic;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>Defines a section within an INI configuration file format</summary>
    public partial class INISection
    {
      internal INISection(string headerline, INIFile src)
      {
        if (headerline == null)
          m_headerline = new INIHeaderLine();
        else
          m_headerline = INIHeaderLine.ReadLine(headerline, src);
      }

      /// <summary>Provides a string representation of the INISection</summary>
      public override string ToString()
      {
        return (Header == null || Header.Length == 0 || Header == PreHeaderSectionName) ? "{Global}" : Header;
      }

      private INIHeaderLine m_headerline;
      private List<INILine> m_lines = new List<INILine>();
      internal INIHeaderLine HLine { get { return m_headerline; } }

      /// <summary>The name of the section</summary>
      public string Header { get { return m_headerline.Header; } set { m_headerline.Header = value; } }

      /// <summary>Defines what other sections this section may inherit its values from</summary>
      public string[] Inherits { get { return m_headerline.Inherits; } set { m_headerline.Inherits = value; } }


      /// <summary>The content of the section</summary>
      public INILine[] Lines { get { return m_lines.ToArray(); } }

      internal void ReadLine(string line, INIFile src)
      {
        INILine newiniline = INILine.ReadLine(line, src);
        m_lines.Add(newiniline);
      }

      /// <summary>Retrieves the list containing all non-empty keys in the section</summary>
      /// <returns></returns>
      public string[] GetKeys()
      {
        string[] ret = new string[m_lines.Count];
        for (int i = 0; i < m_lines.Count; i++)
        {
          if (m_lines[i].HasKey)
          {
            ret[i] = m_lines[i].Key;
          }
        }
        return ret;
      }

      /// <summary>Retrieves the list containing all values in the section</summary>
      public string[] GetValues()
      {
        List<string> ret = new List<string>();
        for (int i = 0; i < m_lines.Count; i++)
        {
          if (m_lines[i].HasValue)
          {
            ret.Add(m_lines[i].Value);
          }
        }
        return ret.ToArray();
      }

      /// <summary>Determines if a key is defined in the section</summary>
      /// <param name="key">The key name</param>
      /// <returns>True if the section and key is defined in the section</returns>
      public bool HasKey(string key)
      {
        for (int i = 0; i < m_lines.Count; i++)
        {
          if (m_lines[i].Key == key)
            return true;
        }
        return false;
      }

      /// <summary>Retrieves the line containing the key in the section</summary>
      /// <param name="key">The key name</param>
      public INILine GetLine(string key)
      {
        for (int i = 0; i < m_lines.Count; i++)
        {
          if (m_lines[i].Key == key)
            return m_lines[i];
        }
        throw new System.Exception("Key '" + key + "' not found in [" + Header + "]");
      }

      /// <summary>Retrieves the value associated with the key in the section</summary>
      /// <param name="key">The key name</param>
      /// <param name="defaultvalue">The default value, if the key is not found</param>
      public string GetValue(string key, string defaultvalue = "")
      {
        for (int i = 0; i < m_lines.Count; i++)
        {
          if (m_lines[i].Key == key)
            return m_lines[i].Value;
        }

        return defaultvalue;
      }

      /// <summary>Adds or sets the value associated with the key in the section</summary>
      /// <param name="key">The key name</param>
      /// <param name="value">The new value to be assigned to the key</param>
      public void SetValue(string key, string value)
      {
        for (int i = 0; i < m_lines.Count; i++)
        {
          if (m_lines[i].Key == key)
          {
            m_lines[i] = new INILine { Key = key, Value = value };
            return;
          }
        }

        m_lines.Add(new INILine { Key = key, Value = value });
      }

      /// <summary>Merges the content of this section with another section</summary>
      /// <param name="section">The name of the other section</param>
      public void Merge(INISection section)
      {
        foreach (INILine line in section.Lines)
        {
          if (line.HasKey)
          {
            m_lines.Remove(GetLine(line.Key));
            m_lines.Add(line);
          }
        }
      }
    }
  }
}
