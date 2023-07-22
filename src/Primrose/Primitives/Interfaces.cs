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

  /// <summary>
  /// Implements an ID object
  /// </summary>
  public interface ISetIdentity : IIdentity
  {
    /// <summary>The instance ID</summary>
    new int ID { get; set; }
  }

  /// <summary>
  /// Implements an ID object
  /// </summary>
  public interface ISetIdentity<T> : IIdentity<T>
  {
    /// <summary>The instance ID</summary>
    new T ID { get; set; }
  }
}
