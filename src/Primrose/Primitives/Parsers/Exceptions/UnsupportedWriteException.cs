﻿using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives.Parsers
{
  /// <summary>Defines an exception for errors encountered by an attempt to write from an unsupported type</summary>
  /// <typeparam name="T">The unsupported type</typeparam>
  public class UnsupportedWriteException<T> : InvalidOperationException
  {
    /// <summary>Defines an exception for errors encountered by an attempt to write from an unsupported type</summary>
    public UnsupportedWriteException() : base("Attempted to write a string from an unsupported type '{0}'".F(typeof(T).Name)) { }
  }
}
