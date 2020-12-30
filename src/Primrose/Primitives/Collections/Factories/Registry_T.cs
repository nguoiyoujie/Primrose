namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Maintains a typed registry of objects.
  /// </summary>
  /// <typeparam name="T">The type of the registered object</typeparam>
  public class Registry<T> : Registry<string, T>
  { 
    // legacy solution, without only one generic type, the key type is assumed to be a string
  }
}
