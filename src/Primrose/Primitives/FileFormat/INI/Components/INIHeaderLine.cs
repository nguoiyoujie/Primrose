using Primrose.Primitives;
using Primrose.Primitives.Extensions;
using System;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    public partial class INISection
    {
      /// <summary>Defines the header line within a section within an INI configuration file format</summary>
      public class INIHeaderLine
      {
        /// <summary>The header</summary>
        public string Header;

        /// <summary>Defines what other sections this section may inherit its values from</summary>
        public string[] Inherits = new string[0];

        /// <summary>Provides a string representation of the line</summary>
        public override string ToString()
        {
          return "[{0}]".F(Header);
        }

        internal static bool IsHeader(string line)
        {
          line = line.Trim();
          int startpos = line.IndexOf('[');
          int endpos = line.IndexOf(']');
          return (startpos == 0 && endpos > -1 && startpos < endpos);
        }

        internal static INIHeaderLine ReadLine(string line, INIFile src)
        {
          INIHeaderLine ret = new INIHeaderLine();
          ret.Parse(line, src);
          return ret;
        }

        private void Parse(string line, INIFile src)
        {
          if (line != null)
          {
            // Format: [HEADER] : inheritHeader, inheritHeader, inheritHeader, ...      ; or #COMMENT
            int compos = -1;
            foreach (string cdelim in src.Attributes.CommentDelimiters)
            {
              int c1 = line.IndexOf(cdelim);
              compos = (compos == -1 || compos < c1) ? c1 : compos;
            }

            if (compos > -1)
              line = line.Substring(0, compos);

            int inhpos = line.IndexOf(src.Attributes.SectionInheritanceDelimiter);
            if (inhpos > -1)
            {
              if (src.Attributes.SupportSectionInheritance)
              {
                if (line.Length > inhpos)
                {
                  string[] headers = line.Substring(inhpos + 1).Split(ListDelimiter, StringSplitOptions.RemoveEmptyEntries);
                  Inherits = new string[headers.Length];
                  for (int i = 0; i < headers.Length; i++)
                    Inherits[i] = headers[i].Trim();
                }
                line = line.Substring(0, inhpos);
              }
            }

            int startpos = line.IndexOf('[');
            int endpos = line.IndexOf(']');
            if (startpos > -1 && endpos > -1 && startpos < endpos)
            {
              Header = line.Substring(startpos + 1, endpos - startpos - 1);
            }
            else
              Header = "";
          }
        }

        internal string Write(INIFile src)
        {
          if (Inherits.Length == 0)
            return "[{0}]".F(Header);
          else
            return "[{0}] {1} {2}".F(Header, src.Attributes.SectionInheritanceDelimiter, string.Join(",", Inherits));
        }
      }
    }
  }
}
