namespace Primrose.Expressions
{
  public interface IValFunc { Val Execute(ITracker caller, string _funcName, IContext c, Val[] p); }
}
