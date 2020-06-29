using Primrose.Primitives.Factories;

namespace Primitives.Strings
{
  /// <summary>Defines a data resource that maps a token code to a string resource</summary>
  public class StringSource<T>
  {
    private readonly Registry<T, string> _reg = new Registry<T, string> { Default = string.Empty };

    /// <summary>Registers a new string token with a formatted string</summary>
    /// <param name="key">The identifier string token</param>
    /// <param name="formattedString">The formatted string</param>
    public void Add(T key, string formattedString)
    {
      _reg.Add(key, formattedString);
    }

    /// <summary>Registers or redefines a new string token with a formatted string</summary>
    /// <param name="key">The identifier string token</param>
    /// <param name="formattedString">The formatted string</param>
    public void Put(T key, string formattedString)
    {
      _reg.Put(key, formattedString);
    }

    /// <summary>Retrieves a formatted string from its code</summary>
    /// <param name="key">The identifier string code.</param>
    public string Get(T key)
    {
      return _reg.Get(key);
    }

    /// <summary>Removes a new string token</summary>
    /// <param name="key">The identifier string token to remove</param>
    public void Remove(T key)
    {
      _reg.Remove(key);
    }

    /// <summary>Clears the source of all tokens</summary>
    public void Clear()
    {
      _reg.Clear();
    }
  }
}
