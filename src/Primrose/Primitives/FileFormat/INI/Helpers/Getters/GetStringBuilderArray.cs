using Primrose.Primitives;
using System.Text;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Gets a StringBuilder[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public StringBuilder[] GetStringBuilderArray(string section, string key, StringBuilder[] defaultList)
    {
      string[] tokens = GetStringArray(section, key, null);
      if (tokens == null)
        return defaultList;

      StringBuilder[] ret = new StringBuilder[tokens.Length];
      for (int i = 0; i < tokens.Length; i++)
        ret[i] = new StringBuilder(tokens[i]);
      return ret;
    }
  }
}
