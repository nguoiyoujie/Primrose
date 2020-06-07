using Primrose.Primitives.Extensions;
using System;

namespace Primrose.Primitives.StateMachines
{
  /// <summary>Represents an exception as a result of an invalid state command</summary>
  /// <typeparam name="T">The state type</typeparam>
  /// <typeparam name="U">The command type</typeparam>
  public class InvalidStateCommandException<T, U> : InvalidOperationException
  {
    /// <summary>Represents an exception as a result of an invalid state command</summary>
    /// <param name="command">The command</param>
    /// <param name="state">The state</param>
    public InvalidStateCommandException(U command, T state) : base("Command '{0}' is not valid on state '{1}'".F(command, state)) { }
  }
}
