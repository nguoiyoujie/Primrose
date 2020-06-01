using NUnit.Framework;
using Primrose.Primitives.Extensions;
using Primrose.Primitives.ValueTypes;

namespace Primrose.UnitTests.ValueTypes
{
  [TestFixture]
  public class Short3
  {
    private const string _NumberSource = "NumberSource";
    private static object[] NumberSource =
    {
      new object[] { (short)0, (short)0, (short)0 },
      new object[] { (short)-45, (short)6, (short)15},
      new object[] { (short)37, (short)-117, (short)53},
      new object[] { (short)2, (short)58, (short)-233},
      new object[] { (short)-128, (short)-170, (short)-69},
      new object[] { (short)121, (short)11, (short)1},
      new object[] { short.MinValue, short.MinValue, short.MinValue  },
      new object[] { short.MaxValue, short.MaxValue, short.MaxValue }
    };

    private const string _InvalidStringSource = "InvalidStringSource";
    private static object[] InvalidStringSource =
    {
      new object[] { "{66666, 0, 0}" },
      new object[] { "{0, -66666, 0}" },
      new object[] { "{0, 0, 66666}" },
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
    public void Short3_GetFields(short v0, short v1, short v2)
    {
      short3 n = new short3(v0, v1, v2);
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Short3_GetIndices(short v0, short v1, short v2)
    {
      short3 n = new short3(v0, v1, v2);
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Short3_SetFields(short v0, short v1, short v2)
    {
      short3 n;
      n.x = v0;
      n.y = v1;
      n.z = v2;
      Assert.That(n.x, Is.EqualTo(v0));
      Assert.That(n.y, Is.EqualTo(v1));
      Assert.That(n.z, Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Short3_SetIndices(short v0, short v1, short v2)
    {
      short3 n = new short3();
      n[0] = v0;
      n[1] = v1;
      n[2] = v2;
      Assert.That(n[0], Is.EqualTo(v0));
      Assert.That(n[1], Is.EqualTo(v1));
      Assert.That(n[2], Is.EqualTo(v2));
    }

    [TestCaseSource(_NumberSource)]
    public void Short3_ParseFromString(short v0, short v1, short v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      short3 n = new short3(v0, v1, v2);
      short3 ns = short3.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Short3_ParseFromString_ArbitarySpace(short v0, short v1, short v2)
    {
      foreach (string fmt in ArbitaryWhiteSpaceFormat)
      {
        string s = fmt.F(v0, v1, v2);
        short3 n = new short3(v0, v1, v2);
        short3 ns = short3.Parse(s);
        Assert.That(ns, Is.EqualTo(n));
      }
    }

    [TestCaseSource(_NumberSource)]
    public void Short3_ParseFromString_NoBrackets(short v0, short v1, short v2)
    {
      string s = NoBracketFormat.F(v0, v1, v2);
      short3 n = new short3(v0, v1, v2);
      short3 ns = short3.Parse(s);
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_NumberSource)]
    public void Short3_WriteToString(short v0, short v1, short v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      short3 n = new short3(v0, v1, v2);
      string sn = n.ToString();
      Assert.That(sn, Is.EqualTo(s));
    }

    [TestCaseSource(_NumberSource)]
    public void Short3_TryParse(short v0, short v1, short v2)
    {
      string s = NormalFormat.F(v0, v1, v2);
      short3 n = new short3(v0, v1, v2);
      short3 ns;
      Assert.IsTrue(short3.TryParse(s, out ns));
      Assert.That(ns, Is.EqualTo(n));
    }

    [TestCaseSource(_InvalidStringSource)]
    public void Short3_TryParse_InvalidReturnsFalse(string invalid)
    {
      string s = invalid;
      short3 ns;
      Assert.IsFalse(short3.TryParse(s, out ns));
    }
  }
}
