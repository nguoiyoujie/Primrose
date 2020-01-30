namespace Primrose.Expressions.UnitTests.Scripting
{
  public delegate Val FunctionDelegate(Context context, Val[] param);

  public class Context : ContextBase
  {
    internal Context() { }
    
    public override void DefineFunc()
    {
      ValFuncs.Clear();
      Functions.Clear();
      ValFuncRef.Clear();

      // Assert
      AddFunc("Assert.AreEqual", new ValFunc<Val, Val>(AssertFns.AreEqual));
      AddFunc("Assert.AreNotEqual", new ValFunc<Val, Val>(AssertFns.AreNotEqual));

      // Console
      AddFunc("Console.Write", new ValFunc<Val>(ConsoleFns.Write));
      AddFunc("Console.WriteLine", new ValFunc<Val>(ConsoleFns.WriteLine));

      // Math
      AddFunc("Math.Int", new ValFunc<float>(MathFns.Int));
      AddFunc("Math.Max", new ValFunc<float, float>(MathFns.Max));
      AddFunc("Math.Min", new ValFunc<float, float>(MathFns.Min));

      // Misc
      AddFunc("IsNull", new ValFunc<Val>(MiscFns.IsNull));
      AddFunc("Random", new ValFunc(MiscFns.Random));
      AddFunc("Random", new ValFunc<int>(MiscFns.Random));
      AddFunc("Random", new ValFunc<int, int>(MiscFns.Random));
      AddFunc("GetArrayElement", new ValFunc<Val, int>(MiscFns.GetArrayElement));
    }
  }
}
