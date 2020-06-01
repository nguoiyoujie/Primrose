using Primrose.Primitives;
using Primrose.Primitives.Parsers;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Gets a bool value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public bool GetBool(string section, string key, bool defaultValue = false)
    {
      return Parser.Parse(GetString(section, key, ""), defaultValue);
    }

    /// <summary>
    /// Gets a byte value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public byte GetByte(string section, string key, byte defaultValue = 0)
    {
      return Parser.Parse(GetString(section, key, defaultValue.ToString()), defaultValue);
    }

    /// <summary>
    /// Gets a short value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public short GetShort(string section, string key, short defaultValue = 0)
    {
      return Parser.Parse(GetString(section, key, defaultValue.ToString()), defaultValue);
    }

    /// <summary>
    /// Gets an int value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public int GetInt(string section, string key, int defaultValue = 0)
    {
      return Parser.Parse(GetString(section, key, defaultValue.ToString()), defaultValue);
    }

    /// <summary>
    /// Gets an uint value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public uint GetUInt(string section, string key, uint defaultValue = 0)
    {
      return Parser.Parse(GetString(section, key, defaultValue.ToString()), defaultValue);
    }

    /// <summary>
    /// Gets a long value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public long GetLong(string section, string key, long defaultValue = 0)
    {
      return Parser.Parse(GetString(section, key, defaultValue.ToString()), defaultValue);
    }

    /// <summary>
    /// Gets an ulong value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public ulong GetULong(string section, string key, ulong defaultValue = 0)
    {
      return Parser.Parse(GetString(section, key, defaultValue.ToString()), defaultValue);
    }

    /// <summary>
    /// Gets a float value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public float GetFloat(string section, string key, float defaultValue = 0)
    {
      return Parser.Parse(GetString(section, key, defaultValue.ToString()), defaultValue);
    }

    /// <summary>
    /// Gets a double value from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public double GetDouble(string section, string key, double defaultValue = 0)
    {
      return Parser.Parse(GetString(section, key, defaultValue.ToString()), defaultValue);
    }
  }
}
