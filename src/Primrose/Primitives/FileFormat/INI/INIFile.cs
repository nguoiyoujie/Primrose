using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Primitives.FileFormat.INI
{
  /// <summary>Defines a standard for INI configuration file format</summary>
  [INIFileConfiguration()]
  public partial class INIFile
  {
    /// <summary>The internal name of the global section</summary>
    public const string PreHeaderSectionName = "\n"; // not possible to duplicate in a real section

    /// <summary>Initializes an empty INI file</summary>
    public INIFile() { }

    /// <summary>Initializes an INI file with a given filepath</summary>
    /// <param name="filepath">The path of the file to be interpreted</param>
    public INIFile(string filepath)
    {
      FilePath = filepath;
      ReadFile();
    }

    /// <summary>The path of the source file</summary>
    public readonly string FilePath;

    /// <summary>The configuration attributes of the format</summary>
    public INIFileConfigurationAttribute Attributes
    {
      get
      {
        object[] list = GetType().GetCustomAttributes(typeof(INIFileConfigurationAttribute), true);
        if (list == null || list.Length < 1)
          throw new Exception("The attribute does not exist!");
        return ((INIFileConfigurationAttribute)list[0]);
      }
    }

    /// <summary>The sections contained within the format</summary>
    public Dictionary<string, INISection>.KeyCollection Sections { get { return m_sections.Keys; } }

    private readonly Dictionary<string, INISection> m_sections = new Dictionary<string, INISection>();

    /// <summary>Clears all information in the format</summary>
    public void Reset()
    {
      m_sections.Clear();
    }

    /// <summary>Determines if a section is defined in the file</summary>
    /// <param name="section">The section name</param>
    /// <returns>True if the section is defined in the file</returns>
    public bool HasSection(string section)
    {
      if (m_sections.ContainsKey(section))
        return true;
      else
        return false;
    }

    /// <summary>Determines if a key is defined in the file</summary>
    /// <param name="section">The section name</param>
    /// <param name="key">The key name</param>
    /// <returns>True if the section and key is defined in the file</returns>
    public bool HasKey(string section, string key)
    {
      if (!HasSection(section))
        return false;

      return (m_sections[section].HasKey(key));
    }

    /// <summary>Gets a section from the file</summary>
    /// <param name="section">The section name</param>
    /// <returns>The INISection object representing the section in the file</returns>
    public INISection GetSection(string section)
    {
      if (m_sections.ContainsKey(section))
        return m_sections[section];
      else
        throw new Exception("The section [{0}] does not exist in '{1}'!".F(section, FilePath));
    }

    /// <summary>Reads and parses the INI file from its source</summary>
    public virtual void ReadFile()
    {
      if (!File.Exists(FilePath))
      {
        throw new Exception("The file {0} is not found!".F(Path.GetFullPath(FilePath)));
      }
      else
      {
        Reset();

        using (StreamReader sr = new StreamReader(FilePath))
        {
          INISection currSection = new INISection("", this);
          if (Attributes.AllowGlobalSection)
            m_sections.Add(PreHeaderSectionName, currSection);

          while (!sr.EndOfStream)
          {
            string line = sr.ReadLine();

            if (INISection.INIHeaderLine.IsHeader(line))
            {
              currSection = new INISection(line, this);
              if (!m_sections.ContainsKey(currSection.Header))
              {
                m_sections.Add(currSection.Header, currSection);
              }
              else
              {
                switch (Attributes.DuplicateSectionPolicy)
                {
                  case DuplicateResolutionPolicy.BOTH:
                    currSection = m_sections[currSection.Header];
                    break;

                  case DuplicateResolutionPolicy.NEW:
                    m_sections.Put(currSection.Header, currSection);
                    break;

                  case DuplicateResolutionPolicy.OLD:
                    break;

                  default:
                  case DuplicateResolutionPolicy.THROW:
                    throw new InvalidOperationException("Invalid duplicate section [{0}] detected.".F(currSection.Header));
                }
              }
            }
            else
            {
              currSection.ReadLine(line, this);
            }
          }
        }
      }

      foreach (FieldInfo fi in GetType().GetFields())
      {
        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIValueAttribute), false))
          fi.SetValue(this, ((INIValueAttribute)a).Read(fi.FieldType, this, fi.GetValue(this)));

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIKeyListAttribute), false))
        {
          if (fi.FieldType != typeof(string[]))
            throw new InvalidOperationException("INIKeyList attribute can only be used with string[] data types! ({0} {1})".F(fi.FieldType.Name, fi.Name));
          fi.SetValue(this, ((INIKeyListAttribute)a).Read(this));
        }
      }
    }

    /// <summary>Writes into a file and updates its source path</summary>
    public virtual void SaveFile(string filepath)
    {
      foreach (FieldInfo fi in GetType().GetFields())
      {
        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIValueAttribute), false))
          ((INIValueAttribute)a).Write(fi.FieldType, this, fi.GetValue(this));

        foreach (Attribute a in fi.GetCustomAttributes(typeof(INIKeyListAttribute), false))
        {
          if (fi.FieldType != typeof(string[]))
            throw new InvalidOperationException("INIKeyList attribute can only be used with string[] data types! ({0} {1})".F(fi.FieldType.Name, fi.Name));
          ((INIKeyListAttribute)a).Write(this, (string[])fi.GetValue(this));
        }
      }

      Directory.CreateDirectory(Path.GetDirectoryName(filepath));

      using (StreamWriter sw = new StreamWriter(filepath, false))
      {
        foreach (INISection section in m_sections.Values)
        {
          if (section != null)
          {
            if (section.Header != null && section.Header.Length > 0)
              sw.WriteLine(section.HLine.Write(this));

            foreach (INISection.INILine line in section.Lines)
            {
              if (line.HasKey)
                sw.WriteLine(line.Write(this));
            }

            sw.WriteLine();
          }
        }
      }
    }
  }
}
