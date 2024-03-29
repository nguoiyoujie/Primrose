﻿using Primrose.Primitives.Extensions;
using System;
using System.Collections.Generic;

namespace Primrose.Primitives.Factories
{
  /// <summary>
  /// Allows creation of objects and stores them automatically. Limited to objects with parameterless constructors; for others, use Registry
  /// </summary>
  /// <typeparam name="K">The key type to be stored</typeparam>
  /// <typeparam name="T">The object type to be stored</typeparam>
  public interface IFactory<K, T> : IRegistry<K, T>
  {
    /// <summary>
    /// Creates a new object and stores its reference in its internal registry
    /// </summary>
    /// <param name="id">The identifier for the object</param>
    /// <returns></returns>
    T Create(K id);
  }

  /// <summary>
  /// Maintains a typed registry of objects.
  /// </summary>
  /// <typeparam name="K">The type of the key</typeparam>
  /// <typeparam name="T">The type of the registered object</typeparam>
  public interface IRegistry<K, T>
  {
    /// <summary>Retrieves the value associated with a key</summary>
    /// <param name="key">The identifier key to check</param>
    /// <returns>The value associated with the key. If the registry does not contain this key, returns Default</returns>
    T Get(K key);

    /// <summary>Retrieves the underlying dictionary</summary>
    IDictionary<K, T> GetUnderlyingDictionary();

    /// <summary>Adds an object into the registry</summary>
    /// <param name="key">The identifier key to add</param>
    /// <param name="item">The object to be associated with this key</param>
    void Add(K key, T item);

    /// <summary>Updates or adds an object into the registry</summary>
    /// <param name="key">The identifier key to add</param>
    /// <param name="item">The object to be associated with this key</param>
    void Put(K key, T item);

    /// <summary>Removes an object from the registry</summary>
    /// <param name="key">The identifier key to remove</param>
    bool Remove(K key);

    /// <summary>Purges all data from the registry</summary>
    void Clear();
  }

  /// <summary>
  /// Defines a Factory object>
  /// </summary>
  /// <typeparam name="K">The type of the associated key</typeparam>
  public abstract class AFactoryObject<K> : IIdentity<K>
  {
    private K id;
    /// <summary>The unique identifier of the object</summary>
    public K ID
    {
      get { return id; }
      set
      {
        if (id == null)
          id = value;
        else
          throw new InvalidOperationException(Resource.Strings.Error_AFactoryObjectDuplicateID.F(id));
      }
    }

    /// <summary>Creates an instance of the object</summary>
    public AFactoryObject() { }
  }
}
