﻿using Primrose.Primitives.Extensions;
using Primrose.Primitives.Parsers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Primitives.FileFormat.INI
{
  /// <summary>Defines a standard for INI configuration file format</summary>
  [INIFileConfiguration()]
  public partial class INIFile
  {
    /// <summary>The internal name of the global section</summary>
    public const string PreHeaderSectionName = "\n"; // not possible to duplicate in a real section

    /// <summary>The default list delimiter used in an INIFile class</summary>
    public static char[] ListDelimiter { get { return Parser.ListDelimiter; } }

    /// <summary>Initializes an empty INI file</summary>
    public INIFile() { }

    /// <summary>Initializes an INI file with a given filepath</summary>
    /// <param name="filepath">The path of the file to be interpreted</param>
    public INIFile(string filepath)
    {
      ReadFromFile(filepath);
    }

    /// <summary>The configuration attributes of the format</summary>
    public INIFileConfigurationAttribute Attributes
    {
      get
      {
        object[] list = GetType().GetCustomAttributes(typeof(INIFileConfigurationAttribute), true);
        if (list == null || list.Length < 1)
          throw new Exception(Primrose.Properties.Resources.Error_INIFileAttributeNotFound);
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
        throw new Exception(Primrose.Properties.Resources.Error_INISectionNotFound.F(section));
    }

    /// <summary>Reads and parses the INI file from a data stream</summary>
    public void ReadFromStream(Stream stream, string baseSection = null, IResolver resolver = null)
    {
      using (StreamReader sr = new StreamReader(stream))
        Read(sr);

      INIFile t = this;
      LoadByAttribute(ref t, baseSection, resolver);
    }

    /// <summary>Reads and parses the INI file from a source file</summary>
    public void ReadFromFile(string filepath, string baseSection = null, IResolver resolver = null)
    {
      if (!File.Exists(filepath))
      {
        throw new FileNotFoundException(Primrose.Properties.Resources.Error_FileNotFound.F(Path.GetFullPath(filepath)));
      }
      else
      {
        Reset();
        using (StreamReader sr = new StreamReader(filepath))
          Read(sr);
      }

      INIFile t = this;
      LoadByAttribute(ref t, baseSection, resolver);
    }

    /// <summary>Reads and parses INI data from a source</summary>
    protected virtual void Read(StreamReader sr)
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
                throw new InvalidOperationException(Primrose.Properties.Resources.Error_DuplicateINISection.F(currSection.Header));
            }
          }
        }
        else
        {
          currSection.ReadLine(line, this);
        }
      }
    }

    /// <summary>Writes the INI data into a file</summary>
    public void WriteToFile(string filepath)
    {
      INIFile t = this;
      UpdateByAttribute(ref t);

      Directory.CreateDirectory(Path.GetDirectoryName(filepath));

      using (StreamWriter sw = new StreamWriter(filepath, false))
        Write(sw);
    }

    /// <summary>Writes the INI data into a stream</summary>
    public void WriteToStream(Stream stream)
    {
      INIFile t = this;
      UpdateByAttribute(ref t);

      using (StreamWriter sw = new StreamWriter(stream))
        Write(sw);
    }

    /// <summary>Writes the INI data into a destination</summary>
    protected virtual void Write(StreamWriter sw)
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
