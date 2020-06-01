using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Byte4
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { (byte)0, (byte)0, (byte)0, (byte)0 },
      new object[] { (byte)45, (byte)6, (byte)15, (byte)25},
      new object[] { (byte)37, (byte)117, (byte)53, (byte)75},
      new object[] { (byte)2, (byte)58, (byte)233, (byte)100},
      new object[] { (byte)128, (byte)170, (byte)69, (byte)86},
      new object[] { (byte)50, (byte)53, (byte)10, (byte)62},
      new object[] { (byte)121, (byte)11, (byte)1, (byte)111},
      new object[] { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{-2, 0, 0, 0}" },
      new object[] { "{256, 0, 0, 0}" },
      new object[] { "{0, 256, 0, 0}" },
      new object[] { "{0, 0, 256, 0}" },
      new object[] { "{0, 0, 0, 256}" },
      new object[] { "NotANumber" }
    };

    private static string NormalFormat = "{{{0},{1},{2},{3}}}";
    private static string NoBracketFormat = "{0},{1},{2},{3}";
    private static string[] ArbitaryWhiteSpaceFormat =
    {
      "{{{0}, {1}, {2}, {3}}}",
      "{{{0}   , {1}    , {2}      , {3}}}",
      "{{{0} \t,\t{1},\t{2}\t,\t{3}}}",
      "{{   {0} , {1}   \t,{2}  \t,   {3}   \t}}",
      "    {0}, {1} ,{2} ,  {3}     ",
      "\t\t\t\t {0}, {1}, {2}, {3}"
    };

    [TestCaseSource(_NumberSource)]
    public void Byte4_GetFields(byte v0, byte v1, byte v2, byte v3)
    {
      byte4 n = new byte4(v0, v1, v2, v3);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
      Assert.That(n.w, Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte4_GetIndices(byte v0, byte v1, byte v2, byte v3)
    {
      byte4 n = new byte4(v0, v1, v2, v3);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
      Assert.That(n[3], Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte4_SetFields(byte v0, byte v1, byte v2, byte v3)
    {
      byte4 n;
      n.x = v0;
      n.y = v1;
      n.z = v2;
      n.w = v3;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
      Assert.That(n.w, Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte4_SetIndices(byte v0, byte v1, byte v2, byte v3)
    {
      byte4 n = new byte4();
      n[0] = v0;
      n[1] = v1;
      n[2] = v2;
      n[3] = v3;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
      Assert.That(n[3], Is.EqualTo(v3));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte4_ParseFromString(byte v0, byte v1, byte v2, byte v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      byte4 n = new byte4(v0, v1, v2, v3);
      byte4 ns = byte4.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte4_ParseFromString_ArbitarySpace(byte v0, byte v1, byte v2, byte v3)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1, v2, v3);
        byte4 n = new byte4(v0, v1, v2, v3);
        byte4 ns = byte4.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Byte4_ParseFromString_NoBrackets(byte v0, byte v1, byte v2, byte v3)
    {
      string s = NoBracketFormat.F(v0, v1, v2, v3);
      byte4 n = new byte4(v0, v1, v2, v3);
      byte4 ns = byte4.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte4_WriteToString(byte v0, byte v1, byte v2, byte v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      byte4 n = new byte4(v0, v1, v2, v3);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Byte4_TryParse(byte v0, byte v1, byte v2, byte v3)
    {
      string s = NormalFormat.F(v0, v1, v2, v3);
      byte4 n = new byte4(v0, v1, v2, v3);
      byte4 ns;
      Assert.IsTrue(byte4.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Byte4_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      byte4 ns;
      Assert.IsFalse(byte4.TryParse(s, out ns));
    }
  }
}
