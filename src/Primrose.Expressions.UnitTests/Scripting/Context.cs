namespace Primrose.Expressions.UnitTests.Scripting
{
  public class Context : ContextBase
  {
    public Context() { }
    
    protected override void DefineFunctions()
    {
      // Assert
      AddFunc<Val, Val>("Assert.AreEqual", AssertFns.AreEqual);
      AddFunc<Val, Val>("Assert.AreNotEqual", AssertFns.AreNotEqual);

      // Console
      AddFunc<Val>("Console.Write", ConsoleFns.Write);
      AddFunc<Val>("Console.WriteLine", ConsoleFns.WriteLine);

      // Math
      AddFunc<float>("Math.Int", MathFns.Int);
      AddFunc<float, float>("Math.Max", MathFns.Max);
      AddFunc<float, float>("Math.Min", MathFns.Min);

      // Misc
      AddFunc<Val>("IsNull", MiscFns.IsNull);
      AddFunc("Random", MiscFns.Random);
      AddFunc<int>("Random", MiscFns.Random);
      AddFunc<int, int>("Random", MiscFns.Random);
      AddFunc<Val, int>("GetArrayElement", MiscFns.GetArrayElement);
    }
  }
}
