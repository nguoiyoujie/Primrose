using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Byte2
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { (byte)0, (byte)0 },
      new object[] { (byte)45, (byte)6},
      new object[] { (byte)37, (byte)117},
      new object[] { (byte)2, (byte)58},
      new object[] { (byte)128, (byte)170},
      new object[] { (byte)50, (byte)53},
      new object[] { (byte)121, (byte)11},
      new object[] { byte.MaxValue, byte.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{-1, -1}" },
      new object[] { "{256, 0}" },
      new object[] { "{0, 256}" },
      new object[] { "NotANumber" }
    };

    private static string NormalFormat = "{{{0},{1}}}";
    private static string NoBracketFormat = "{0},{1}";
    private static string[] ArbitaryWhiteSpaceFormat =
    {
      "{{{0}, {1}}}",
      "{{{0}   , {1}}}",
      "{{{0} \t,\t{1}}}",
      "{{   {0} , {1}   \t}}",
      "    {0}, {1}     ",
      "\t\t\t\t {0}, {1}"
    };

    [TestCaseSource(_NumberSource)]
    public void Byte2_GetFields(byte v0, byte v1)
    {
      byte2 n = new byte2(v0, v1);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte2_GetIndices(byte v0, byte v1)
    {
      byte2 n = new byte2(v0, v1);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte2_SetFields(byte v0, byte v1)
    {
      byte2 n;
      n.x = v0;
      n.y = v1;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte2_SetIndices(byte v0, byte v1)
    {
      byte2 n = new byte2();
      n[0] = v0;
      n[1] = v1;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte2_ParseFromString(byte v0, byte v1)
    {
      string s = NormalFormat.F(v0, v1);
      byte2 n = new byte2(v0, v1);
      byte2 ns = byte2.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte2_ParseFromString_ArbitarySpace(byte v0, byte v1)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1);
        byte2 n = new byte2(v0, v1);
        byte2 ns = byte2.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Byte2_ParseFromString_NoBrackets(byte v0, byte v1)
    {
      string s = NoBracketFormat.F(v0, v1);
      byte2 n = new byte2(v0, v1);
      byte2 ns = byte2.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte2_WriteToString(byte v0, byte v1)
    {
      string s = NormalFormat.F(v0, v1);
      byte2 n = new byte2(v0, v1);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte2_TryParse(byte v0, byte v1)
    {
      string s = NormalFormat.F(v0, v1);
      byte2 n = new byte2(v0, v1);
      byte2 ns;
      Assert.IsTrue(byte2.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Byte2_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      byte2 ns;
      Assert.IsFalse(byte2.TryParse(s, out ns));
    }
  }
}
