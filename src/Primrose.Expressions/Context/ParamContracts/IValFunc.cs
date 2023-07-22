using System.Collections.Generic;

namespace Primrose.Expressions
{
  internal interface IValFunc 
  {
    object Func { get; }
    Val Execute(ITracker caller, string _funcName, IContext c, IList<Val> p); 
  }
}
