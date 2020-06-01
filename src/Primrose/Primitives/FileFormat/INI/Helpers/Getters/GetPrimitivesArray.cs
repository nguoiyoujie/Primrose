using Primrose.Primitives;
using Primrose.Primitives.Parsers;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Gets a bool[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public bool[] GetBoolArray(string section, string key, bool[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }

    /// <summary>
    /// Gets a byte[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public byte[] GetByteArray(string section, string key, byte[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }

    /// <summary>
    /// Gets a short[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public short[] GetShortArray(string section, string key, short[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }

    /// <summary>
    /// Gets an int[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public int[] GetIntArray(string section, string key, int[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }

    /// <summary>
    /// Gets an uint[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public uint[] GetUIntArray(string section, string key, uint[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }

    /// <summary>
    /// Gets a long[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public long[] GetLongArray(string section, string key, long[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }

    /// <summary>
    /// Gets an ulong[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public ulong[] GetULongArray(string section, string key, ulong[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }

    /// <summary>
    /// Gets a float[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public float[] GetFloatArray(string section, string key, float[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }

    /// <summary>
    /// Gets a double[] array from the INIFile 
    /// </summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="defaultList">The default array</param>
    /// <returns>The value belong to the section and key in the INIFile. If any key does not exist, returns defaultValue</returns>
    public double[] GetDoubleArray(string section, string key, double[] defaultList)
    {
      return Parser.Parse(GetString(section, key, ""), defaultList);
    }
  }
}
