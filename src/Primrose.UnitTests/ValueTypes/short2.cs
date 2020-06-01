using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Short2
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { (short)0, (short)0 },
      new object[] { (short)-45, (short)6},
      new object[] { (short)37, (short)-117},
      new object[] { (short)2, (short)58},
      new object[] { (short)-128, (short)-170},
      new object[] { (short)121, (short)11},
      new object[] { short.MinValue, short.MinValue },
      new object[] { short.MaxValue, short.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{66666, 0}" },
      new object[] { "{0, -66666}" },
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
    public void Short2_GetFields(short v0, short v1)
    {
      short2 n = new short2(v0, v1);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Short2_GetIndices(short v0, short v1)
    {
      short2 n = new short2(v0, v1);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Short2_SetFields(short v0, short v1)
    {
      short2 n;
      n.x = v0;
      n.y = v1;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Short2_SetIndices(short v0, short v1)
    {
      short2 n = new short2();
      n[0] = v0;
      n[1] = v1;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
    }

    [TestCaseSource(_NumberSource)]
    public void Short2_ParseFromString(short v0, short v1)
    {
      string s = NormalFormat.F(v0, v1);
      short2 n = new short2(v0, v1);
      short2 ns = short2.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Short2_ParseFromString_ArbitarySpace(short v0, short v1)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1);
        short2 n = new short2(v0, v1);
        short2 ns = short2.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Short2_ParseFromString_NoBrackets(short v0, short v1)
    {
      string s = NoBracketFormat.F(v0, v1);
      short2 n = new short2(v0, v1);
      short2 ns = short2.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Short2_WriteToString(short v0, short v1)
    {
      string s = NormalFormat.F(v0, v1);
      short2 n = new short2(v0, v1);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Short2_TryParse(short v0, short v1)
    {
      string s = NormalFormat.F(v0, v1);
      short2 n = new short2(v0, v1);
      short2 ns;
      Assert.IsTrue(short2.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Short2_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      short2 ns;
      Assert.IsFalse(short2.TryParse(s, out ns));
    }
  }
}
