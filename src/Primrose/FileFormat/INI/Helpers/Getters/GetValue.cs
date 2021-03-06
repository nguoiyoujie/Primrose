﻿using Primrose.Primitives.Parsers;

namespace Primrose.FileFormat.INI
{
  public partial class INIFile
  {
    /// <summary>Gets a value from the INIFile</summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <param name="defaultValue">The default value</param>
    /// <returns>The value belonging to the section and key in the INIFile. If the key does not exist, returns defaultValue</returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetValue<T>(string section, string key, IResolver resolver = null, T defaultValue = default)
    {
      return Parser.Parse(GetString(section, key), resolver, defaultValue);
    }

    /// <summary>Gets a value from the INIFile</summary>
    /// <param name="section">The section containing the key-value pair</param>
    /// <param name="key">The key containing the value</param>
    /// <param name="resolver">A string resolver function</param>
    /// <returns>The value belonging to the section and key in the INIFile. If the key does not exist, returns defaultValue</returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetValueX<T>(string section, string key, IResolver resolver = null)
    {
      if (HasKey(section, key))
      {
        return Parser.Parse<T>(GetString(section, key), resolver);
      }
      else
      {
        throw new INIKeyNotFoundException(section, key);
      }
    }
  }
}
