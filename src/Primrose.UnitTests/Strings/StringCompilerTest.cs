using NUnit.Framework;
using Primrose.Primitives.Strings;

namespace Primrose.UnitTests.Strings
{
  [TestFixture]
  public class StringCompilerTest
  {
    [TestCase(0ul)]
    [TestCase(2350ul)]
    [TestCase(35660ul)]
    [TestCase(3470ul)]
    [TestCase(48850ul)]
    [TestCase(3350ul)]
    [TestCase(468584788875ul)]
    [TestCase(357345546ul)]
    [TestCase(ulong.MaxValue)]
    public void StringCompiler_Numerics_Int64(ulong value)
    {
      StringCompiler scmp = new StringCompiler();
      scmp.Items.Add(new UnsignedIntegerFormatter(value));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString()));
      scmp.Items.Clear();
      scmp.Items.Add(new UnsignedIntegerFormatter(value, padding: 8));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString("D8")));
      scmp.Items.Clear();
      scmp.Items.Add(new UnsignedIntegerFormatter(value, padding: 12, padDirection: PadAlignmentType.RIGHT, padCharacter: ' '));
      Assert.That(scmp.CreateString(), Is.EqualTo(string.Format("{0,-12}", value)));
      scmp.Items.Clear();
      scmp.Items.Add(new UnsignedIntegerFormatter(value, padding: 8, @base: 16));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString("X8")));
      scmp.Items.Clear();
    }

    [TestCase(0u)]
    [TestCase(2350u)]
    [TestCase(35660u)]
    [TestCase(3470u)]
    [TestCase(48850u)]
    [TestCase(3350u)]
    [TestCase(uint.MaxValue)]
    public void StringCompiler_Numerics_Int32(uint value)
    {
      StringCompiler scmp = new StringCompiler();
      scmp.Items.Add(new UnsignedIntegerFormatter(value));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString()));
      scmp.Items.Clear();
      scmp.Items.Add(new UnsignedIntegerFormatter(value, padding: 8));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString("D8")));
      scmp.Items.Clear();
      scmp.Items.Add(new UnsignedIntegerFormatter(value, padding: 12, padDirection: PadAlignmentType.RIGHT, padCharacter: ' '));
      Assert.That(scmp.CreateString(), Is.EqualTo(string.Format("{0,-12}", value)));
      scmp.Items.Clear();
      scmp.Items.Add(new UnsignedIntegerFormatter(value, padding: 8, @base: 16));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString("X8")));
      scmp.Items.Clear();
    }

    [TestCase(0L)]
    [TestCase(-2350L)]
    [TestCase(35660L)]
    [TestCase(3470L)]
    [TestCase(-48850L)]
    [TestCase(-35779686350L)]
    [TestCase(468584788875L)]
    [TestCase(357345546L)]
    [TestCase(long.MaxValue)]
    [TestCase(long.MinValue)]
    public void StringCompiler_SignedNumerics_Int64(long value)
    {
      StringCompiler scmp = new StringCompiler();
      scmp.Items.Add(new SignedIntegerFormatter(value));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString()));
      scmp.Items.Clear();
      // 'D8' format will produce an additional sign character for negative numbers!
      scmp.Items.Add(new SignedIntegerFormatter(value, padding: value < 0 ? 9 : 8, negativeSign: NegativeFormatType.BEFORE_PADDING));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString("D8")));
      scmp.Items.Clear();
      scmp.Items.Add(new SignedIntegerFormatter(value, padding: 12, padDirection: PadAlignmentType.RIGHT, padCharacter: ' '));
      Assert.That(scmp.CreateString(), Is.EqualTo(string.Format("{0,-12}", value)));
      scmp.Items.Clear();
      // 'X8' format will convert negative hexadecimal numbers to their conjugates
      scmp.Items.Add(new SignedIntegerFormatter(value, @base: 16, padding: 8, negativeSign: NegativeFormatType.OVERFLOW));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString("X8")));
      scmp.Items.Clear();
    }

    [TestCase(0)]
    [TestCase(-2350)]
    [TestCase(35660)]
    [TestCase(3470)]
    [TestCase(-48850)]
    [TestCase(int.MaxValue)]
    [TestCase(int.MinValue)]
    public void StringCompiler_SignedNumerics_Int32(int value)
    {
      StringCompiler scmp = new StringCompiler();
      scmp.Items.Add(new SignedIntegerFormatter(value));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString()));
      scmp.Items.Clear();
      // 'D8' format will produce an additional sign character for negative numbers!
      scmp.Items.Add(new SignedIntegerFormatter(value, padding: value < 0 ? 9 : 8, negativeSign: NegativeFormatType.BEFORE_PADDING));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString("D8")));
      scmp.Items.Clear();
      scmp.Items.Add(new SignedIntegerFormatter(value, padding: 12, padDirection: PadAlignmentType.RIGHT, padCharacter: ' '));
      Assert.That(scmp.CreateString(), Is.EqualTo(string.Format("{0,-12}", value)));
      scmp.Items.Clear();
      // 'X8' format will convert negative hexadecimal numbers to their conjugates
      scmp.Items.Add(new SignedIntegerFormatter(value, @base: 16, padding: 8, negativeSign: NegativeFormatType.OVERFLOW));
      Assert.That(scmp.CreateString(), Is.EqualTo(value.ToString("X8")));
      scmp.Items.Clear();
    }
  }
}
