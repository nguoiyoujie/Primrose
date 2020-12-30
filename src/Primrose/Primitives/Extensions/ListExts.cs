﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Primrose.Primitives.Extensions
{
  /// <summary>
  /// Provides extension methods for Lists
  /// </summary>
  public static class ListExts
  {
    /// <summary>
    /// Performs a binary search 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="searchFor"></param>
    /// <returns></returns>
    public static int BinarySearchMany<T>(this List<T> list, int searchFor) where T : IIdentity
    {
      int start = 0;
      int end = list.Count;
      while (start != end)
      {
        int mid = (start + end) / 2;
        if (list[mid].ID < searchFor)
          start = mid + 1;
        else
          end = mid;
      }
      return start;
    }

    /// <summary>
    /// Performs a binary search 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="list"></param>
    /// <param name="target"></param>
    /// <param name="comparer"></param>
    /// <returns></returns>
    public static int BinarySearch<T, U>(this List<T> list, U target, Func<T, U, int> comparer)
    {
      int start = 0;
      int end = list.Count;
      while (start != end)
      {
        int mid = (start + end) / 2;
        if (comparer(list[mid], target) < 0)
          start = mid + 1;
        else
          end = mid;
      }
      return start;
    }

    /// <summary>Retrives a random object from a list of objects</summary>
    /// <typeparam name="T">The member type</typeparam>
    /// <param name="list">The array</param>
    /// <param name="rand">The random object</param>
    /// <returns>A random object from the array. If the array has no members, return the default value of the member type</returns>
    /// <exception cref="ArgumentNullException"><paramref name="list"/> and <paramref name="rand"/> cannot be null</exception>
    public static T Random<T>(this List<T> list, Random rand)
    {
      if (list == null) { throw new ArgumentNullException(nameof(list)); }
      if (rand == null) { throw new ArgumentNullException(nameof(rand)); }
      return list[rand.Next(0, list.Count)];
    }

    /// <summary>Retrives a random object from a list of objects</summary>
    /// <typeparam name="T">The member type</typeparam>
    /// <param name="list">The array</param>
    /// <param name="rand">The random object</param>
    /// <returns>A random object from the array. If the array has no members, return the default value of the member type</returns>
    /// <exception cref="ArgumentNullException"><paramref name="list"/> and <paramref name="rand"/> cannot be null</exception>
    public static T Random<T>(this ConcurrentBag<T> list, Random rand)
    {
      if (list == null) { throw new ArgumentNullException(nameof(list)); }
      if (rand == null) { throw new ArgumentNullException(nameof(rand)); }
      return list.ToArray()[rand.Next(0, list.Count)];
    }

    /// <summary>
    /// Removes items from a list based on a predicate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="match"></param>
    /// <returns></returns>
    public static int RemoveAll<T>(this LinkedList<T> list, Predicate<T> match)
    {
      if (list == null) { throw new ArgumentNullException(nameof(list)); }
      if (match == null) { throw new ArgumentNullException(nameof(match)); }

      int count = 0;
      LinkedListNode<T> node = list.First;
      LinkedListNode<T> next;
      while (node != null)
      {
        next = node.Next;
        if (match(node.Value))
        {
          list.Remove(node);
          count++;
        }
        node = next;
      }
      return count;
    }

    /// <summary>Retrives a random object from a list of objects</summary>
    /// <typeparam name="T">The member type</typeparam>
    /// <param name="list">The array</param>
    /// <param name="rand">The random object</param>
    /// <returns>A random object from the array. If the array has no members, return the default value of the member type</returns>
    /// <exception cref="ArgumentNullException"><paramref name="list"/> and <paramref name="rand"/> cannot be null</exception>
    public static T Random<T>(this LinkedList<T> list, Random rand)
    {
      if (list == null) { throw new ArgumentNullException(nameof(list)); }
      if (rand == null) { throw new ArgumentNullException(nameof(rand)); }

      int count = rand.Next(0, list.Count);
      LinkedListNode<T> node = list.First;
      while (count > 0)
      {
        node = node.Next;
        count--;
      }
      return node.Value;
    }
  }
}
