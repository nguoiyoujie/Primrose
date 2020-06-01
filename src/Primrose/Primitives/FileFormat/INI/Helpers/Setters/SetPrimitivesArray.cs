using Primrose.Primitives.Parsers;

namespace Primitives.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>
    /// Sets a bool[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetBoolArray(string section, string key, bool[] list) { SetString(section, key, Parser.Write(list)); }

    /// <summary>
    /// Sets a byte[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetByteArray(string section, string key, byte[] list) { SetString(section, key, Parser.Write(list)); }

    /// <summary>
    /// Sets a short[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetShortArray(string section, string key, short[] list) { SetString(section, key, Parser.Write(list)); }

    /// <summary>
    /// Sets an int[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetIntArray(string section, string key, int[] list) { SetString(section, key, Parser.Write(list)); }

    /// <summary>
    /// Sets an uint[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetUIntArray(string section, string key, uint[] list) { SetString(section, key, Parser.Write(list)); }

    /// <summary>
    /// Sets an long[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetLongArray(string section, string key, long[] list) { SetString(section, key, Parser.Write(list)); }

    /// <summary>
    /// Sets an ulong[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetULongArray(string section, string key, ulong[] list) { SetString(section, key, Parser.Write(list)); }

    /// <summary>
    /// Sets a float[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetFloatArray(string section, string key, float[] list) { SetString(section, key, Parser.Write(list)); }

    /// <summary>
    /// Sets a double[] array in the INIFile 
    /// </summary>
    /// <param name="section">The section that will contain the key-value pair</param>
    /// <param name="key">The key that will contain the value</param>
    /// <param name="list">The array of values to be set</param>
    public void SetDoubleArray(string section, string key, double[] list) { SetString(section, key, Parser.Write(list)); }
  }
}
