namespace Primrose
{
  /// <summary>A delegate with no parameters</summary>
  public delegate void VoidDelegate();

  /// <summary>A delegate with one parameter</summary>
  public delegate void ActionDelegate<T>(T param);

  /// <summary>A function delegate with no parameters</summary>
  public delegate V FuncDelegate<V>();

  /// <summary>A function delegate with one parameter</summary>
  public delegate V FuncDelegate<V, TParam>(TParam param);

  /// <summary>A delegate representing a receipt of a string message</summary>
  /// <param name="message">Represents message received</param>
  public delegate void MessageDelegate(string message);

  /// <summary>A delegate representing a receipt of a typed message</summary>
  /// <typeparam name="T">The type of the message</typeparam>
  /// <param name="message">Represents message received</param>
  public delegate void MessageDelegate<T>(T message);
}
