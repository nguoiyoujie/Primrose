﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Primrose.Primitives.Extensions
{
  /// <summary>
  /// Provides extension methods for arrays
  /// </summary>
  public static class ArrayExts
  {
    /// <summary>Retrives a random object from an array of objects</summary>
    /// <typeparam name="T">The member type</typeparam>
    /// <param name="array">The array</param>
    /// <param name="rand">The random object</param>
    /// <returns>A random object from the array. If the array has no members, return the default value of the member type</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> and <paramref name="rand"/> cannot be null</exception>
    public static T Random<T>(this T[] array, Random rand)
    {
      if (array == null) { throw new ArgumentNullException(nameof(array)); }
      if (rand == null) { throw new ArgumentNullException(nameof(rand)); }
      return array.Length == 0 ? default : array[rand.Next(0, array.Length)];
    }

    /// <summary>Determines if an array contains a value</summary>
    /// <typeparam name="T">The member type</typeparam>
    /// <param name="array">The array</param>
    /// <param name="value">The value to check</param>
    /// <param name="start">The start index of the array to check</param>
    /// <param name="length">The number of elements to check</param>
    /// <returns>True if the array contains at least one member equal to value, false otherwise</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    public static bool Contains<T>(this T[] array, T value, int start = 0, int length = -1)
    {
      if (array == null) { throw new ArgumentNullException(nameof(array)); }
      if (length == -1) { length = array.Length; }
      for (int i = 0; i < length; i++)
      {
        T t = array[i];
        if (EqualityComparer<T>.Default.Equals(value, t))
          return true;
      }
      return false;
    }

    /// <summary>Determines the index position of a value in an array</summary>
    /// <typeparam name="T">The member type</typeparam>
    /// <param name="array">The array</param>
    /// <param name="value">The value to check</param>
    /// <param name="start">The start index of the array to check</param>
    /// <param name="length">The number of elements to check</param>
    /// <returns>The index position if the array contains at least one member equal to value, -1 otherwise</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    public static int IndexOf<T>(this T[] array, T value, int start = 0, int length = -1)
    {
      if (array == null) { throw new ArgumentNullException(nameof(array)); }
      if (length == -1) { length = array.Length; }
      for (int i = 0; i < length; i++)
      {
        T t = array[i];
        if (EqualityComparer<T>.Default.Equals(value, t))
          return i;
      }
      return -1;
    }

    /// <summary>Determines if two arrays contains the same elements, not necessarily in order</summary>
    /// <typeparam name="T">The member type</typeparam>
    /// <param name="array">The array</param>
    /// <param name="other">The second array to compare</param>
    /// <returns>True if the two arrays contains the same elements, false otherwise</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> cannot be null</exception>
    public static bool ContainsSameElements<T>(this T[] array, T[] other)
    {
      if (array == null) { throw new ArgumentNullException(nameof(array)); }
      if (other == null || other.Length != array.Length) { return false; }

      var cnt = new Dictionary<T, int>();
      foreach (T s in array)
      {
        if (cnt.ContainsKey(s))
        {
          cnt[s]++;
        }
        else
        {
          cnt.Add(s, 1);
        }
      }
      foreach (T s in other)
      {
        if (cnt.ContainsKey(s))
        {
          cnt[s]--;
        }
        else
        {
          return false;
        }
      }
      return cnt.Values.All(c => c == 0);
    }

    /// <summary>Performs an element-wise conversion of an array to an array of another type</summary>
    /// <typeparam name="T">The member type of the input array</typeparam>
    /// <typeparam name="U">The member type of the return array</typeparam>
    /// <param name="array">The input array</param>
    /// <param name="convertFn">The conversion function for each element</param>
    /// <returns>An array with each element converted from the corresponding element in the input array</returns>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> and <paramref name="convertFn"/> cannot be null</exception>
    public static U[] Convert<T, U>(this T[] array, Func<T, U> convertFn)
    {
      if (array == null) { throw new ArgumentNullException(nameof(array)); }
      if (convertFn == null) { throw new ArgumentNullException(nameof(convertFn)); }

      U[] ret = new U[array.Length];
      for (int i = 0; i < array.Length; i++)
        ret[i] = convertFn.Invoke(array[i]);

      return ret;
    }

  }
}
