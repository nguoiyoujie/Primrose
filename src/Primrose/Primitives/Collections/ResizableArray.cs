using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives.Collections
{
  /// <summary>Represents an resizable array</summary>
  /// <typeparam name="T">The element type</typeparam>
  public class ResizableArray<T>
  {
    private T[] _array;
    private int _count;

    /// <summary>Represents an resizable array</summary>
    /// <param name="initialCount">The initial size of the array</param>
    public ResizableArray(int initialCount = 0)
    {
      int initialCapacity = initialCount.Max(4);
      _count = initialCount;
      _array = new T[initialCapacity];
    }

    /// <summary>Retrieves the underlying array</summary>
    public T[] InternalArray { get { return _array; } }

    /// <summary>The number of valid elements in the array</summary>
    public int Count { get { return _count; } }

    /// <summary>Adds an element to the array</summary>
    /// <param name="element">The element to be added</param>
    public void Add(T element)
    {
      if (_count == _array.Length)
      {
        Array.Resize(ref _array, _array.Length * 2);
      }

      _array[_count++] = element;
    }

    /// <summary>Adds a range of elements to the array</summary>
    /// <param name="elements">The range of elements to be added</param>
    public void AddRange(T[] elements)
    {
      if (_count + elements.Length >= _array.Length)
      {
        int newSize = _array.Length;
        while (_count + elements.Length >= newSize)
          newSize *= 2;

        Array.Resize(ref _array, newSize);
      }

      elements.CopyTo(_array, _count);
      _count += elements.Length;
    }

    /// <summary>Adds a range of elements to the array</summary>
    /// <param name="elements">The range of elements to be added</param>
    public void AddRange(ResizableArray<T> elements)
    {
      if (_count + elements.Count >= _array.Length)
      {
        int newSize = _array.Length;
        while (_count + elements.Count >= newSize)
          newSize *= 2;

        Array.Resize(ref _array, newSize);
      } 
      Array.Copy(elements.InternalArray, 0, _array, _count, elements.Count);
      _count += elements.Count;
    }

    /// <summary>Adds a range of elements to the array</summary>
    /// <param name="elements">The range of elements to be added</param>
    public void AddRange(IEnumerable<T> elements)
    {
      // slow because iterative
      foreach (T t in elements) { Add(t); }
    }

    /// <summary>Resizes the array to contain the specified number of elements. This resizing will double the size of the current array to accommodate this count.</summary>
    /// <param name="count">The desired size of the array</param>
    public void Resize(int count)
    {
      // double size
      if (count > _array.Length)
      {
        int newSize = _array.Length;
        while (count > newSize)
          newSize *= 2;

        Array.Resize(ref _array, newSize);
      }
      _count = count;
    }

    /// <summary>Resizes the array to contain the specified number of elements. This resizing is exact.</summary>
    /// <param name="count">The desired size of the array</param>
    public void ResizeExact(int count)
    {
      Array.Resize(ref _array, count);
      _count = count;
    }


    /// <summary>Resets the count to 0, and optionally clears elements to default</summary>
    /// <param name="clearElements">Determines if the members of the array should be zeroed</param>
    public void Clear(bool clearElements = false)
    {
      _count = 0;
      if (clearElements)
      {
        Array.Clear(_array, 0, _array.Length);
      }
    }

    /// <summary>Clears elements within the array to default values</summary>
    public void ClearElements(int start, int length)
    {
      length = length.Min(_array.Length - start); // do not go out of range
      Array.Clear(_array, start, length);
    }

    /// <summary>Retrieves the element at an index of the array</summary>
    /// <param name="id">The index of the element</param>
    public T this[int id] { get { return _array[id]; } set { _array[id] = value; } }
  }
}
