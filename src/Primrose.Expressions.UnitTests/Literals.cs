using System;
using System.Globalization;
using NUnit.Framework;

namespace Primrose.Expressions.UnitTests
{
  [TestFixture]
  public class Literals
  {
    [TestCase("true")]
    [TestCase("false")]
    public void Literal_Bool(string value)
    {
      bool i = Boolean.Parse(value);
      ScriptExpression expr = new ScriptExpression(value);
      Assert.That((bool)expr.Evaluate(null), Is.EqualTo(i));
    }

    private const string _intStrings = "intStrings";
    private static string[] intStrings = {
        "0", // zero
        "1", "432", "57965", // natural
        "-2", "-93", "-24611", // negative
        "0xc", "0xff", "0xd045" // hex
      };
    [TestCaseSource(_intStrings)]
    public void Literal_Int(string value)
    {
      int i = value.StartsWith("0x")
        ? Int32.Parse(value.Substring(2), NumberStyles.HexNumber)
        : Int32.Parse(value, NumberStyles.Number);
      ScriptExpression expr = new ScriptExpression(value);
      Assert.That((int)expr.Evaluate(null), Is.EqualTo(i));
    }

    private const string _floatStrings = "floatStrings";
    private static string[] floatStrings = {
        "0.0", // zero
        "0.54", "0.231", "0.00001", // 0 < x < 1
        "20.4", "539022.2",
        "10", "24", // int as float
        "-6509.2", "-35.13896" // negative
      };
    [TestCaseSource(_floatStrings)]
    public void Literal_Float(string value)
    {
      float i = Single.Parse(value);
      ScriptExpression expr = new ScriptExpression(value);
      Assert.That((float)expr.Evaluate(null), Is.EqualTo(i));
    }

    private const string _strings = "Strings";
    private static string[] Strings = {
        "\"yes\"",
        "\"affirmative\"",
        "\"joy to the world\"",
        "\"RAGE_AGE_GE_E\"",
        "\"Weird.pun|ct&u@a$ti()o/n!\"",
        "\"[(brackets)]{}\"",
      };
    [TestCaseSource(_strings)]
    public void Literal_String(string value)
    {
      string i = value.Trim('\"');
      ScriptExpression expr = new ScriptExpression(value);
      Assert.That((string)expr.Evaluate(null), Is.EqualTo(i));
    }
  }
}
