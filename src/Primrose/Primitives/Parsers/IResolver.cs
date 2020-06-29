namespace Primrose.Primitives.Parsers
{
  /// <summary>Allows customized resolution of parsing contentions, if any</summary>
  public interface IResolver
  {
    /// <summary>Resolves parsing contentions for a given input</summary>
    /// <param name="input">The input to resolve</param>
    string Resolve(string input);
  }
}
