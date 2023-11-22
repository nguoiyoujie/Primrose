namespace Primrose
{
  /// <summary>A delegate with no parameters</summary>
  public delegate void VoidDelegate();

  #region Action<...>
  /// <summary>A delegate with one parameter</summary>
  public delegate void ActionDelegate<T>(T obj);

  /// <summary>A delegate with one parameter</summary>
  public delegate void ActionDelegate<T, P>(T obj, P p);

  /// <summary>A delegate with two parameters</summary>
  public delegate void ActionDelegate<T, P1, P2>(T obj, P1 p1, P2 p2);

  /// <summary>A delegate with three parameters</summary>
  public delegate void ActionDelegate<T, P1, P2, P3>(T obj, P1 p1, P2 p2, P3 p3);
  #endregion

  #region Action<ref ...>
  /// <summary>A delegate with one parameter</summary>
  public delegate void ActionRefDelegate<T>(ref T obj);

  /// <summary>A delegate with one parameter</summary>
  public delegate void ActionRefDelegate<T, P>(ref T obj, P p);

  /// <summary>A delegate with two parameters</summary>
  public delegate void ActionRefDelegate<T, P1, P2>(ref T obj, P1 p1, P2 p2);

  /// <summary>A delegate with three parameters</summary>
  public delegate void ActionRefDelegate<T, P1, P2, P3>(ref T obj, P1 p1, P2 p2, P3 p3);
  #endregion

  #region Func<..., V>
  /// <summary>A function delegate with no parameters</summary>
  public delegate V FunctionDelegate<V>();

  /// <summary>A function delegate with one parameter</summary>
  public delegate V FunctionDelegate<P, V>(P p);

  /// <summary>A function delegate with two parameters</summary>
  public delegate V FunctionDelegate<P1, P2, V>(P1 p1, P2 p2);

  /// <summary>A function delegate with three parameters</summary>
  public delegate V FunctionDelegate<P1, P2, P3, V>(P1 p1, P2 p2, P3 p3);
  #endregion

  #region Func<..., ref V>
  /// <summary>A function delegate with one parameter</summary>
  public delegate void FunctionRefDelegate<P, V>(P p, ref V value);

  /// <summary>A function delegate with two parameters</summary>
  public delegate void FunctionRefDelegate<P1, P2, V>(P1 p1, P2 p2, ref V value);

  /// <summary>A function delegate with three parameters</summary>
  public delegate void FunctionRefDelegate<P1, P2, P3, V>(P1 p1, P2 p2, P3 p3, ref V value);
  #endregion

  #region Func<ref T, ..., ref V>
  /// <summary>A function delegate with a state object and no parameters</summary>
  public delegate void FunctionStateRefDelegate<T, V>(ref T obj, ref V value);

  /// <summary>A function delegate with a state object and one parameter</summary>
  public delegate void FunctionStateRefDelegate<T, P, V>(ref T obj, P p, ref V value);

  /// <summary>A function delegate with a state object and two parameters</summary>
  public delegate void FunctionStateRefDelegate<T, P1, P2, V>(ref T obj, P1 p1, P2 p2, ref V value);

  /// <summary>A function delegate with a state object and three parameters</summary>
  public delegate void FunctionStateRefDelegate<T, P1, P2, P3, V>(ref T obj, P1 p1, P2 p2, P3 p3, ref V value);
  #endregion

  #region Predicate<...>
  /// <summary>A predicate delegate with no parameters</summary>
  public delegate bool PredicateDelegate<T>(T obj);

  /// <summary>A predicate delegate with one parameter</summary>
  public delegate bool PredicateDelegate<T, P>(T obj, P p);

  /// <summary>A predicate delegate with two parameters</summary>
  public delegate bool PredicateDelegate<T, P1, P2>(T obj, P1 p1, P2 p2);

  /// <summary>A predicate delegate with three parameters</summary>
  public delegate bool PredicateDelegate<T, P1, P2, P3>(T obj, P1 p1, P2 p2, P3 p3);
  #endregion

  #region Predicate<ref ...>
  /// <summary>A predicate delegate with no parameters</summary>
  public delegate bool PredicateRefDelegate<T>(ref T obj);

  /// <summary>A predicate delegate with one parameter</summary>
  public delegate bool PredicateRefDelegate<T, P>(ref T obj, P p);

  /// <summary>A predicate delegate with two parameters</summary>
  public delegate bool PredicateRefDelegate<T, P1, P2>(ref T obj, P1 p1, P2 p2);

  /// <summary>A predicate delegate with three parameters</summary>
  public delegate bool PredicateRefDelegate<T, P1, P2, P3>(ref T obj, P1 p1, P2 p2, P3 p3);
  #endregion

  #region Messaging
  /// <summary>A delegate representing a receipt of a string message</summary>
  /// <param name="message">Represents message received</param>
  public delegate void MessageDelegate(string message);

  /// <summary>A delegate representing a receipt of a typed message</summary>
  /// <typeparam name="T">The type of the message</typeparam>
  /// <param name="message">Represents message received</param>
  public delegate void MessageDelegate<T>(T message);
  #endregion
}
