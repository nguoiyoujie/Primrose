using Primrose.Primitives.Parsers;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Sets a bool value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetBool(string section, string key, bool value) { SetString(section, key, Parser.Write(value)); }

    /// <summary>
    /// Sets a byte value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetByte(string section, string key, byte value) { SetString(section, key, value.ToString()); }

    /// <summary>
    /// Sets a short value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetShort(string section, string key, short value) { SetString(section, key, value.ToString()); }

    /// <summary>
    /// Sets an int value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetInt(string section, string key, int value) { SetString(section, key, value.ToString()); }

    /// <summary>
    /// Sets an uint value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetUInt(string section, string key, uint value) { SetString(section, key, value.ToString()); }

    /// <summary>
    /// Sets a long value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetLong(string section, string key, long value) { SetString(section, key, value.ToString()); }

    /// <summary>
    /// Sets an ulong value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetULong(string section, string key, ulong value) { SetString(section, key, value.ToString()); }

    /// <summary>
    /// Sets a float value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetFloat(string section, string key, float value) { SetString(section, key, value.ToString()); }

    /// <summary>
    /// Sets a double value in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="value">The value to be set</param>
    public void SetDouble(string section, string key, double value) { SetString(section, key, value.ToString()); }
  }
}
