namespace Primrose.Primitives
{
  /// <summary>
  /// Implements an ID object
  /// </summary>
  public interface IIdentity
  {
    /// <summary>The instance ID</summary>
    int ID { get; }
  }

  /// <summary>
  /// Implements an ID object
  /// </summary>
  public interface IIdentity<T>
  {
    /// <summary>The instance ID</summary>
    T ID { get; }
  }
}
