using Primrose.Primitives.Extensions;

namespace Primrose.FileFormat.INI
{
  public partial class INIFile
  {
    public partial class INISection
    {
      /// <summary>Defines a line within a section within an INI configuration file format</summary>
      public struct INILine
      {
        /// <summary>The key associated in this line</summary>
        public string Key;

        /// <summary>The value associated in this line</summary>
        public string Value;

        /// <summary>Denotes if this line has a key</summary>
        public bool HasKey { get { return Key != null && Key.Length > 0; } }

        /// <summary>Denotes if this line has a value</summary>
        public bool HasValue { get { return Value != null && Value.Length > 0; } }

        /// <summary>Provides a string representation of the line</summary>
        public override string ToString()
        {
          return "{0}={1}".F(Key, Value);
        }

        internal static INILine ReadLine(string line, INIFile src)
        {
          INILine ret = new INILine();
          ret.Parse(line, src);
          return ret;
        }

        private void Parse(string line, INIFile src)
        {
          if (line != null)
          {
            // Format: KEY(=VALUE)  (;COMMENT)

            int compos = -1;
            foreach (string cdelim in src.Attributes.CommentDelimiters)
            {
              int c1 = line.IndexOf(cdelim);
              compos = (compos == -1 || compos < c1) ? c1 : compos;
            }

            if (compos > -1)
              line = line.Substring(0, compos);

            int keypos = line.IndexOf(src.Attributes.KeyValueDelimiter);
            if (keypos > -1)
            {
              if (line.Length > keypos)
                Value = line.Substring(keypos + 1).Trim();

              Key = line.Substring(0, keypos).Trim();
            }
            else
              Key = line.Trim();
          }
        }

        internal string Write(INIFile src)
        {
          if (Value == null)
            return Key ?? "";
          else
            return "{0}{1}{2}".F(Key, src.Attributes.KeyValueDelimiter, Value);
        }
      }
    }
  }
}
