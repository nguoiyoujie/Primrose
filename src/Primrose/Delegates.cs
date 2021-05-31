namespace Primrose
{
  /// <summary>A delegate representing a receipt of a string message</summary>
  /// <param name="message">Represents message received</param>
  public delegate void MessageDelegate(string message);

  /// <summary>A delegate representing a receipt of a typed message</summary>
  /// <typeparam name="T">The type of the message</typeparam>
  /// <param name="message">Represents message received</param>
  public delegate void MessageDelegate<T>(T message);
}
