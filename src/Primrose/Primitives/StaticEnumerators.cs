using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Primrose.Primitives
{
  /// <summary>Represents an enumeration of items in a linked list</summary>
  /// <typeparam name="T"></typeparam>
  public struct LinkedListEnumerable<T>
  {
    readonly LinkedList<T> L;
    /// <summary>Creates the enumerable</summary>
    public LinkedListEnumerable(LinkedList<T> l) { L = l; }

    /// <summary>Gets the enumerator</summary>
    public LinkedListEnumerator<T> GetEnumerator() { return new LinkedListEnumerator<T>(L); }

    /// <summary>The enumerator for an empty list</summary>
    public static LinkedListEnumerable<T> Empty = new LinkedListEnumerable<T>(null);
  }

  /// <summary>Represents an enumerator of items in a linked list</summary>
  /// <typeparam name="T"></typeparam>
  public struct LinkedListEnumerator<T>
  {
    readonly LinkedList<T> L;
    LinkedListNode<T> current;
    /// <summary>Creates an enumerator</summary>
    public LinkedListEnumerator(LinkedList<T> l) { L = l; current = null; }

    /// <summary>Retrieves the next item</summary>
    public bool MoveNext() { return (current = (current == null) ? L?.First : current?.Next) != null; }

    /// <summary>Retrieves the current item</summary>
    public T Current { get { return (current == null) ? default : current.Value; } }
  }

  /// <summary>Represents an enumerator of items in an array</summary>
  /// <typeparam name="T"></typeparam>
  public struct ArrayEnumerator<T> : IEnumerator<T>
  {
    private T[] _array;
    private int _startindex;
    private int _index;
    private int _maxindex;


    /// <summary>Creates an array enumerator</summary>
    /// <param name="array">The backing</param>
    /// <param name="index">The starting index</param>
    /// <param name="maxindex">The maximum index</param>
    public ArrayEnumerator(T[] array, int index = 0, int maxindex = -1)
    {
      _array = array;
      _startindex = index - 1;
      _index = _startindex;
      _maxindex = (maxindex == -1) ? _array.Length - 1 : maxindex;
      if (_array == null)
      {
        throw new ArgumentNullException(nameof(array));
      }

      if (_startindex < 0 || _maxindex < _startindex || _maxindex >= _array.Length)
      {
        throw new ArgumentException("Attempted to create an ArrayEnumerator<T> with indices outside the array capacity");
      }
    }

    /// <summary>Disposes the enumerator</summary>
    public void Dispose()
    {
      _index = _maxindex;
    }

    /// <summary>Advances the enumerator to the next element of the collection</summary>
    /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection</returns>
    public bool MoveNext()
    {
      _index++;
      return _index <= _maxindex;
    }

    /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection</summary>
    public void Reset()
    {
      _index = _startindex;
    }

    /// <summary>Gets the current element in the collection</summary>
    public T Current
    {
      get { return _array[_index]; }
    }

    object IEnumerator.Current => Current;
  }

}
