using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using System.Text;

namespace Primrose.Primitives.Strings
{
  /// <summary>Represents a format instruction for generating a string from an unsigned integer value</summary>
  public class UnsignedIntegerFormatter : IFormatter
  {
    // use for byte, ushort, uint, ulong

    private ulong _value;
    private int _base = 10; // min base 2, max base 16 
    private int _padding = 0;
    private PadAlignmentType _padDirection = PadAlignmentType.LEFT;
    private char _padCharacter = '0';

    private int _len;
    private int _lenWithPad;

    /// <summary>Creates a format instruction for generating a string from an 8-bit unsigned integer value</summary>
    public UnsignedIntegerFormatter(byte value, int @base = 10, int padding = 0, PadAlignmentType padDirection = PadAlignmentType.LEFT, char padCharacter = '0')
    {
      _value = value;
      _base = @base;
      _padding = padding;
      _padDirection = padDirection;
      _padCharacter = padCharacter;
      RecalcLength();
    }

    /// <summary>Creates a format instruction for generating a string from an 16-bit unsigned integer value</summary>
    public UnsignedIntegerFormatter(ushort value, int @base = 10, int padding = 0, PadAlignmentType padDirection = PadAlignmentType.LEFT, char padCharacter = '0')
    {
      _value = value;
      _base = @base;
      _padding = padding;
      _padDirection = padDirection;
      _padCharacter = padCharacter;
      RecalcLength();
    }

    /// <summary>Creates a format instruction for generating a string from an 32-bit unsigned integer value</summary>
    public UnsignedIntegerFormatter(uint value, int @base = 10, int padding = 0, PadAlignmentType padDirection = PadAlignmentType.LEFT, char padCharacter = '0')
    {
      _value = value;
      _base = @base;
      _padding = padding;
      _padDirection = padDirection;
      _padCharacter = padCharacter;
      RecalcLength();
    }

    /// <summary>Creates a format instruction for generating a string from an 64-bit unsigned integer value</summary>
    public UnsignedIntegerFormatter(ulong value, int @base = 10, int padding = 0, PadAlignmentType padDirection = PadAlignmentType.LEFT, char padCharacter = '0')
    {
      _value = value;
      _base = @base;
      _padding = padding;
      _padDirection = padDirection;
      _padCharacter = padCharacter;
      RecalcLength();
    }

    /// <summary>The numeric value</summary>
    public ulong Value
    {
      get { return _value; }
      set
      {
        if (_value != value)
        {
          _value = value;
          RecalcLength();
        }
      }
    }

    /// <summary>The base power. Only values from 2 to 16 are accepted</summary>
    public int Base
    {
      get { return _base; }
      set
      {
        value = value.Clamp(2, 16);
        if (_base != value)
        {
          _base = value;
          RecalcLength();
        }
      }
    }

    /// <summary>The length of the string to pad to</summary>
    public int Padding
    {
      get { return _padding; }
      set
      {
        if (_padding != value)
        {
          _padding = value;
          RecalcLength();
        }
      }
    }

    /// <summary>The padding character</summary>
    public PadAlignmentType PadDirection
    {
      get { return _padDirection; }
      set
      {
        // pad character does not impact length calculation
        _padDirection = value;
      }
    }

    /// <summary>The padding character</summary>
    public char PadCharacter
    {
      get { return _padCharacter; }
      set
      {
        // pad character does not impact length calculation
        _padCharacter = value;
      }
    }

    /// <inheritdoc/>
    public int ExpectedLength { get { return _lenWithPad; } }

    /// <inheritdoc/>
    public string Format()
    {
      StringBuilder sb = ObjectPool<StringBuilder>.GetStaticPool().GetNew();
      Concat(sb);
      string result = sb.ToString();
      ObjectPool<StringBuilder>.GetStaticPool().Return(sb);
      return result;
    }

    /// <inheritdoc/>
    public void Format(StringBuilder sb)
    {
      Concat(sb);
    }

    private void RecalcLength()
    {
      int len = 1;
      ulong val = (ulong)_base;
      ulong b = (ulong)_base;
      ulong maxval = ulong.MaxValue / b; // ensure that val does not exceed the max value

      while (val < _value)
      {
        len++;
        val *= b;
        if (val >= maxval)
        {
          len++;
          break;
        }
      }

      _len = len;
      _lenWithPad = len.Max(_padding);
    }

    private void Concat(StringBuilder sb)
    {
      // length is already precalulated
      // fill the space with pad characters
      sb.Append(_padCharacter, _lenWithPad);

      // Identify the position of the final digit
      int strpos = sb.Length;
      if (_padDirection == PadAlignmentType.RIGHT)
      {
        strpos -= _lenWithPad - _len;
      }

      ulong val = _value;
      ulong b = (ulong)_base;
      // We're writing backwards, one character at a time.
      for (int i = 0; i < _len; i++)
      {
        strpos--;
        // Lookup from static char array, to cover hex values too
        sb[strpos] = ArrayConstants.HexadecimalDigits[val % b];
        val /= b;
      }
    }
  }
}
