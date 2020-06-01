using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Byte3
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { (byte)0, (byte)0, (byte)0 },
      new object[] { (byte)45, (byte)6, (byte)15},
      new object[] { (byte)37, (byte)117, (byte)53},
      new object[] { (byte)2, (byte)58, (byte)233},
      new object[] { (byte)128, (byte)170, (byte)69},
      new object[] { (byte)50, (byte)53, (byte)10},
      new object[] { (byte)121, (byte)11, (byte)1},
      new object[] { byte.MaxValue, byte.MaxValue, byte.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{-2, 0, 0}" },
      new object[] { "{256, 0, 0}" },
      new object[] { "{0, 256, 0}" },
      new object[] { "{0, 0, 256}" },
      new object[] { "NotANumber" }
    };

    private static string NormalFormat = "{{{0},{1},{2}}}";
    private static string NoBracketFormat = "{0},{1},{2}";
    private static string[] ArbitaryWhiteSpaceFormat =
    {
      "{{{0}, {1}, {2}}}",
      "{{{0}   , {1}    , {2}}}",
      "{{{0} \t,\t{1},\t{2}}}",
      "{{   {0} , {1}   \t,{2}  \t}}",
      "    {0}, {1} ,{2}      ",
      "\t\t\t\t {0}, {1}, {2}"
    };

    [TestCaseSource(_NumberSource)]
    public void Byte3_GetFields(byte v0, byte v1, byte v2)
    {
      byte3 n = new byte3(v0, v1, v2);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte3_GetIndices(byte v0, byte v1, byte v2)
    {
      byte3 n = new byte3(v0, v1, v2);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte3_SetFields(byte v0, byte v1, byte v2)
    {
      byte3 n;
      n.x = v0;
      n.y = v1;
      n.z = v2;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte3_SetIndices(byte v0, byte v1, byte v2)
    {
      byte3 n = new byte3();
      n[0] = v0;
      n[1] = v1;
      n[2] = v2;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte3_ParseFromString(byte v0, byte v1, byte v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      byte3 n = new byte3(v0, v1, v2);
      byte3 ns = byte3.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte3_ParseFromString_ArbitarySpace(byte v0, byte v1, byte v2)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1, v2);
        byte3 n = new byte3(v0, v1, v2);
        byte3 ns = byte3.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Byte3_ParseFromString_NoBrackets(byte v0, byte v1, byte v2)
    {
      string s = NoBracketFormat.F(v0, v1, v2);
      byte3 n = new byte3(v0, v1, v2);
      byte3 ns = byte3.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte3_WriteToString(byte v0, byte v1, byte v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      byte3 n = new byte3(v0, v1, v2);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte3_TryParse(byte v0, byte v1, byte v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      byte3 n = new byte3(v0, v1, v2);
      byte3 ns;
      Assert.IsTrue(byte3.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Byte3_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      byte3 ns;
      Assert.IsFalse(byte3.TryParse(s, out ns));
    }
  }
}
