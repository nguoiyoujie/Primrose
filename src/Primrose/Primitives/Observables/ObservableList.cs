﻿using System.Collections;
using System.Collections.Generic;

namespace Primrose.Primitives.Observables
{
  /// <summary>Represents a list with events that notify on modification</summary>
  /// <typeparam name="TList">The type of the list</typeparam>
  /// <typeparam name="T">The type of elements in the list</typeparam>
  public class ObservableList<TList, T> : IList<T>
    where TList : IList<T>
  {
    private TList _list;

#pragma warning disable IDE0044 // Add readonly modifier
    private ChangeEvent<TList> _listChanged = new ChangeEvent<TList>();
    private ChangeEvent<T> _itemAdded = new ChangeEvent<T>();
    private ChangeEvent<T> _itemRemoved = new ChangeEvent<T>();
    private ChangeEvent<T> _itemChanged = new ChangeEvent<T>();
#pragma warning restore IDE0044 // Add readonly modifier

    /// <summary>Represents the set of functions to be called when the registry is replaced</summary>
    public event ChangeEventDelegate<TList> ListChanged { add { _listChanged.Ev += value; } remove { _listChanged.Ev -= value; } }

    /// <summary>Represents the set of functions to be called when a key is added</summary>
    public event ChangeEventDelegate<T> ItemAdded { add { _itemAdded.Ev += value; } remove { _itemAdded.Ev -= value; } }

    /// <summary>Represents the set of functions to be called when a key is removed</summary>
    public event ChangeEventDelegate<T> ItemRemoved { add { _itemRemoved.Ev += value; } remove { _itemRemoved.Ev -= value; } }

    /// <summary>Represents the set of functions to be called when a value is changed</summary>
    public event ChangeEventDelegate<T> ItemChanged { add { _itemChanged.Ev += value; } remove { _itemChanged.Ev -= value; } }

    /// <summary>Represents a list with events that notify on modification</summary>
    /// <param name="source">The initial list. No events are fired on the assignment of the initial list.</param>
    public ObservableList(TList source)
    {
      _list = source;
      _listChanged = default;
      _itemAdded = default;
      _itemRemoved = default;
      _itemChanged = default;
    }

    /// <summary>Gets or sets an element in the list, accessed by an index</summary>
    public T this[int index]
    {
      get { return _list[index]; }
      set
      {
        T old = _list[index];
        _list[index] = value;
        _itemChanged.Invoke(value, old);
      }
    }

    /// <summary>Gets the number of elements contained in the list</summary>
    public int Count { get { return _list.Count; } }

    /// <summary>Gets a value indicating whether the list is read-only</summary>
    public bool IsReadOnly { get { return _list.IsReadOnly; } }

    /// <summary>The encapsulated list. Direct method calls to the encapsulated object will not trigger any events.</summary>
    public TList List
    {
      get { return _list; }
      set
      {
        TList old = _list;
        _list = value;
        if ((_list == null && old != null) || !(_list != null && EqualityComparer<TList>.Default.Equals(_list, old)))
          _listChanged.Invoke(value, old);
      }
    }

    /// <summary>Adds an item to the list</summary>
    /// <param name="item">The object to add to the list</param>
    public void Add(T item)
    {
      // event
      _list.Add(item);
      _itemAdded.Invoke(item, default);
    }

    /// <summary>Removes all items from the list</summary>
    public void Clear() { _list.Clear(); }

    /// <summary>Determines if the list contains a specific value</summary>
    /// <param name="item">The object to locate in the list</param>
    public bool Contains(T item) { return _list.Contains(item); }

    /// <summary>Copies the elements of the list to an array, starting at a particular index.</summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied the list. The array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
    public void CopyTo(T[] array, int arrayIndex) { _list.CopyTo(array, arrayIndex); }

    /// <summary>Determines the index of a specific item in the list</summary>
    /// <param name="item">The object to locate in the list</param>
    /// <returns></returns>
    public int IndexOf(T item) {return _list.IndexOf(item);}

    /// <summary>Inserts an item to the list at the specified index.</summary>
    /// <param name="index">The zero-based index at which item should be inserted</param>
    /// <param name="item">The object to insert into the list</param>
    public void Insert(int index, T item)
    {
      _list.Insert(index, item);
      _itemAdded.Invoke(item, default);
    }

    /// <summary>Removes the first occurrence of a specific object from the list</summary>
    /// <param name="item">The object to remove from the item</param>
    /// <returns></returns>
    public bool Remove(T item)
    {
      bool ret = _list.Remove(item);
      if (ret)
        _itemRemoved.Invoke(default, item);

      return ret;
    }

    /// <summary>Removes the item at the specified index.</summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    public void RemoveAt(int index)
    {
      T item = _list[index];
      _list.RemoveAt(index);
      _itemRemoved.Invoke(default, item);
    }

    /// <summary>Returns an enumerator that iterates through the collection.</summary>
    public IEnumerator<T> GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _list.GetEnumerator();
    }

  }
}
