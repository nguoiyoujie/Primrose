
/*
namespace Primrose.FileFormat.JSON
{
  /// <summary>Defines a standard for JSON file format</summary>
  public class JSONFile : IFile
  {
    /// <summary>The list of root-level objects in this file</summary>
    public readonly List<JSONValue> Root = new List<JSONValue>();

    /// <summary>Initializes an empty INI file</summary>
    public JSONFile() { }

    /// <summary>Initializes an JSON file with a given filepath</summary>
    /// <param name="filepath">The path of the file to be interpreted</param>
    public JSONFile(string filepath)
    {
      ReadFromFile(filepath);
    }

    /// <summary>Clears all information in the format</summary>
    public void Reset()
    {
      Root.Clear();
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

    /// <summary>Gets a section from the file</summary>
    /// <param name="section">The section name</param>
    /// <returns>The INISection object representing the section in the file</returns>
    public INISection GetSection(string section)
    {
      if (m_sections.ContainsKey(section))
        return m_sections[section];
      else
        throw new Exception(Resource.Strings.Error_INISectionNotFound.F(section));
    }

    /// <summary>Reads and parses the INI file from a data stream</summary>
    public void ReadFromStream(StreamReader reader, string baseSection = null, IResolver resolver = null)
    {
      Read(reader);
      JSONFile t = this;
      LoadByAttribute(ref t, baseSection, resolver);
    }

    /// <summary>Reads and parses the INI file from a data stream</summary>
    public void ReadFromStream(Stream stream, string baseSection = null, IResolver resolver = null)
    {
      using (StreamReader sr = new StreamReader(stream))
        Read(sr);

      JSONFile t = this;
      LoadByAttribute(ref t, baseSection, resolver);
    }

    /// <summary>Reads and parses the INI file from a source file</summary>
    public void ReadFromFile(string filepath) { ReadFromFile(filepath, null, null); }

    /// <summary>Reads and parses the INI file from a source file</summary>
    public void ReadFromFile(string filepath, string baseSection = null, IResolver resolver = null)
    {
      if (!File.Exists(filepath))
      {
        throw new FileNotFoundException(Resource.Strings.Error_FileNotFound.F(Path.GetFullPath(filepath)));
      }
      else
      {
        Reset();
        using (StreamReader sr = new StreamReader(filepath))
          Read(sr);
      }

      JSONFile t = this;
      LoadByAttribute(ref t, baseSection, resolver);
    }

    /// <summary>Reads and parses INI data from a source</summary>
    protected virtual void Read(StreamReader sr)
    {
      // Root

      JSONValue current = new JSONValue();

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
                throw new InvalidOperationException(Resource.Strings.Error_DuplicateINISection.F(currSection.Header));
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
      JSONFile t = this;
      UpdateByAttribute(ref t);

      Directory.CreateDirectory(Path.GetDirectoryName(filepath));

      using (StreamWriter sw = new StreamWriter(filepath, false))
        Write(sw);
    }

    /// <summary>Writes the INI data into a stream</summary>
    public void WriteToStream(StreamWriter writer)
    {
      JSONFile t = this;
      UpdateByAttribute(ref t);
      Write(writer);
    }

    /// <summary>Writes the INI data into a stream</summary>
    public void WriteToStream(Stream stream)
    {
      JSONFile t = this;
      UpdateByAttribute(ref t);

      using (StreamWriter sw = new StreamWriter(stream))
        Write(sw);
    }

    /// <summary>Writes the INI data into a destination</summary>
    protected virtual void Write(StreamWriter sw)
    {
      // write the preheader first
      INISection preheader = null;
      if (m_sections.ContainsKey(PreHeaderSectionName))
      {
        preheader = m_sections[PreHeaderSectionName];
        if (preheader != null && preheader.Lines.Length > 0)
        {
          foreach (INISection.INILine line in preheader.Lines)
          {
            if (line.HasKey)
              sw.WriteLine(line.Write(this));
          }
          sw.WriteLine();
        }
      }

      foreach (INISection section in m_sections.Values)
      {
        if (section != null && section != preheader)
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
*/