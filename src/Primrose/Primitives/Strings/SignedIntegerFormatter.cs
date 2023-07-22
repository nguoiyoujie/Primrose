using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using System.Text;

namespace Primrose.Primitives.Strings
{
  /// <summary>Represents a format instruction for generating a string from an unsigned integer value</summary>
  public class SignedIntegerFormatter : IFormatter
  {
    // use for sbyte, short, int, long

    private long _value;
    private int _base = 10; // min base 2, max base 16 
    private int _padding = 0;
    private PadAlignmentType _padDirection = PadAlignmentType.LEFT;
    private NegativeFormatType _negativeSign = NegativeFormatType.AFTER_PADDING;
    private char _padCharacter = '0';
    private ulong _overflow;

    private int _len;
    private int _lenWithPad;

    /// <summary>Creates a format instruction for generating a string from an unsigned 8-bit integer value</summary>
    public SignedIntegerFormatter(sbyte value, int @base = 10, int padding = 0, PadAlignmentType padDirection = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeSign = NegativeFormatType.AFTER_PADDING)
    {
      _value = value;
      _base = @base;
      _padding = padding;
      _padDirection = padDirection;
      _padCharacter = padCharacter;
      _negativeSign = negativeSign;
      _overflow = byte.MaxValue;
      RecalcLength();
    }

    /// <summary>Creates a format instruction for generating a string from an unsigned 16-bit integer value</summary>
    public SignedIntegerFormatter(short value, int @base = 10, int padding = 0, PadAlignmentType padDirection = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeSign = NegativeFormatType.AFTER_PADDING)
    {
      _value = value;
      _base = @base;
      _padding = padding;
      _padDirection = padDirection;
      _padCharacter = padCharacter;
      _negativeSign = negativeSign;
      _overflow = ushort.MaxValue;
      RecalcLength();
    }

    /// <summary>Creates a format instruction for generating a string from an unsigned 32-bit integer value</summary>
    public SignedIntegerFormatter(int value, int @base = 10, int padding = 0, PadAlignmentType padDirection = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeSign = NegativeFormatType.AFTER_PADDING)
    {
      _value = value;
      _base = @base;
      _padding = padding;
      _padDirection = padDirection;
      _padCharacter = padCharacter;
      _negativeSign = negativeSign;
      _overflow = uint.MaxValue;
      RecalcLength();
    }

    /// <summary>Creates a format instruction for generating a string from an unsigned 64-bit integer value</summary>
    public SignedIntegerFormatter(long value, int @base = 10, int padding = 0, PadAlignmentType padDirection = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeSign = NegativeFormatType.AFTER_PADDING)
    {
      _value = value;
      _base = @base;
      _padding = padding;
      _padDirection = padDirection;
      _padCharacter = padCharacter;
      _negativeSign = negativeSign;
      _overflow = ulong.MaxValue;
      RecalcLength();
    }

    /// <summary>The numeric value</summary>
    public long Value
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
      int len;

      if (_value < 0 && _negativeSign == NegativeFormatType.OVERFLOW)
      {
        len = 1;
        ulong lvalue = _overflow & (ulong)_value;
        ulong val = (ulong)_base;
        ulong b = (ulong)_base;
        ulong maxval = ulong.MaxValue / b; // ensure that val does not exceed the max value

        while (val < lvalue)
        {
          len++;
          val *= b;
          if (val >= maxval)
          {
            len++;
            break;
          }
        }
      }
      else
      {
        len = _value < 0 ? 2 : 1;
        long uvalue = _value < 0 ? -_value : _value;
        // NOTE: -long.MinValue == long.MinValue . We make a special case to override this
        if (_value == long.MinValue)
        {
          uvalue = -(_value + 1);
        }

        long val = _base;
        long b = _base;
        long maxval = long.MaxValue / b; // ensure that val does not exceed the max value

        while (val < uvalue)
        {
          len++;
          val *= b;
          if (val >= maxval)
          {
            len++;
            break;
          }
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
      int strfirst = sb.Length - _lenWithPad;
      int strpos = sb.Length;
      if (_padDirection == PadAlignmentType.RIGHT)
      {
        strpos -= _lenWithPad - _len;
      }

      long mvalue = _value;
      long b = _base;
      if (_value < 0)
      {
        if (_negativeSign == NegativeFormatType.OVERFLOW)
        {
          // We're writing backwards, one character at a time.
          ulong lvalue = _overflow & (ulong)mvalue;
          ulong lb = (ulong)b;
          for (int i = _len - 1; i >= 0; i--)
          {
            strpos--;
            // Lookup from static char array, to cover hex values too
            sb[strpos] = ArrayConstants.HexadecimalDigits[lvalue % lb];
            lvalue /= lb;
          }
        }
        else
        {
          // We're writing backwards, one character at a time.
          for (int i = _len - 1; i >= 0; i--)
          {
            strpos--;
            // Lookup from static char array, to cover hex values too
            sb[strpos] = ArrayConstants.HexadecimalDigits[-(mvalue % b)];
            mvalue /= b;
            if (i == 0)
            {
              if (_negativeSign == NegativeFormatType.BEFORE_PADDING)
              {
                // the sign character is before the padding ('-00000123')
                sb[strfirst] = '-';
              }
              else
              {
                // the sign character is after the padding ('     -123')
                sb[strpos] = '-';
              }
            }
          }
        }
      }
      else
      {
        // We're writing backwards, one character at a time.
        for (int i = _len - 1; i >= 0; i--)
        {
          strpos--;
          // Lookup from static char array, to cover hex values too
          sb[strpos] = ArrayConstants.HexadecimalDigits[mvalue % b];
          mvalue /= b;
        }
      }
    }
  }
}
