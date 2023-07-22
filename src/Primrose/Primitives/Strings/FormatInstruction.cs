using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using System.Runtime.InteropServices;
using System.Text;

namespace Primrose.Primitives.Strings
{
  /// <summary>Represents a format instruction for generating a string</summary>
  [StructLayout(LayoutKind.Explicit)]
  public struct FormatInstruction
  {
    // Map of bits: 
    //  [_data_0_3.0-1]    Value Size: 0=8-bit, 1=16-bit, 2=32-bit, 3=64-bit. Does not matter if value is a reference
    //  [_data_0_3.2-4]    Value Type: 0=Unsigned Integer, 1=Signed Integer, 2=Floating-point, 3=DateTime, 4=?, 5=?, 6=?, 7=Reference Type
    //  [_data_0_3.5]      Pad Direction: 0=Left, 1=Right
    //  [_data_0_3.6-7]    Reserved
    //  [_data_0_3.8-15]   Pad Character (char)
    //  [_data_0_3.16-31]  Pad Length (ushort)
    //  [_data_4_5.0-15]   (Internal) Expected length (ushort)
    //  [_data_6_7.0-15]   Reserved
    // Numeric types:
    //  [_data_8_15.0-1]    Negative Sign: 0=SignBeforePadding, 1=SignAfterPadding, 2=Overflow
    //  [_data_8_15.2-4]    Numeric Base: 0=Decimal, 1=Hexadecimal, 2=Octal, 3=Binary, 4-7=? (Integers only)
    //  [_data_8_15.5-7]    Reserved
    //  [_data_8_15.8-11]   Decimal Places (byte)
    //  [_data_8_15.12-15]  Integer Places (byte)
    //  [_data_8_15.16-31]  Reserved
    //  [_data_8_15.32-63]  Reserved
    // DateTime types:
    //  [_data_8_15.ref]    Reference to the format string
    // Strings types:


    /// <summary>Describe the memory size of the value, only if the value is a valuetype</summary>
    public enum StoredSize : byte
    {
      /// <summary>Denotes an 8-bit value</summary>
      _8BIT = 0,

      /// <summary>Denotes a 16-bit value</summary>
      _16BIT = 1,

      /// <summary>Denotes a 32-bit value</summary>
      _32BIT = 2,

      /// <summary>Denotes a 64-bit value</summary>
      _64BIT = 3,
    }

    /// <summary>Describe the the type of the value</summary>
    public enum StoredType : byte
    {
      /// <summary>Denotes an unsigned integer</summary>
      UNSIGNED_INTEGER = 0,

      /// <summary>Denotes an unsigned integer</summary>
      SIGNED_INTEGER = 1,

      /// <summary>Denotes an unsigned integer</summary>
      FLOATING_POINT = 2,

      /// <summary>Denotes a datetime, represented by a 64-bit integer</summary>
      DATE_TIME = 3,

      /// <summary>Denotes an unsigned integer</summary>
      REFERENCE_TYPE = 7,
    }

    /// <summary>Describe the the base of the numeric value</summary>
    public enum NumericBase : byte
    {
      /// <summary>Denotes base-10, or decimal representation</summary>
      DECIMAL = 0,

      /// <summary>Denotes base-16, or hexadecimal representation</summary>
      HEXADECIMAL = 1,

      /// <summary>Denotes base-8, or octal representation</summary>
      OCTAL = 2,

      /// <summary>Denotes base-2, or binary representation</summary>
      BINARY = 3,
    }

    [FieldOffset(0)]
    private uint _data_0_3;

    [FieldOffset(4)]
    private ushort _data_4_5;

    [FieldOffset(6)]
    private ushort _data_6_7;

    [FieldOffset(8)]
    private ulong _data_8_15;

    [FieldOffset(8)]
    private string _dataFmt;

    [FieldOffset(16)] private long _valueL;

    [FieldOffset(16)] private ulong _valueUL;

    [FieldOffset(16)] private int _valueI;

    [FieldOffset(16)] private uint _valueUI;

    [FieldOffset(16)] private short _valueS;

    [FieldOffset(16)] private ushort _valueUS;

    [FieldOffset(16)] private sbyte _valueSB;

    [FieldOffset(16)] private byte _valueB;

    [FieldOffset(16)] private double _valueD;

    [FieldOffset(16)] private float _valueF;

    [FieldOffset(16)] private object _valueObj;

    /// <summary>Returns the memory size of the value. This is ignored if the type is a reference type</summary>
    public StoredSize Size { get { return (StoredSize)((_data_0_3 & 0xC0000000u) >> 30); } private set { _data_0_3 |= ((uint)value << 30) & 0xC0000000u; } }

    /// <summary>Returns the the type of the value</summary>
    public StoredType Type { get { return (StoredType)((_data_0_3 & 0x38000000u) >> 27); } private set { _data_0_3 |= ((uint)value << 27) & 0x38000000u; } }

    /// <summary>Returns whether the padding characters are to be inserted to the left or to the right</summary>
    public PadAlignmentType PadAlignment { get { return (PadAlignmentType)((_data_0_3 & 0x04000000u) >> 26); } private set { _data_0_3 |= ((uint)value << 26) & 0x04000000u; } }

    /// <summary>Returns the padding character</summary>
    public char PadCharacter { get { return (char)((_data_0_3 & 0x00FF0000u) >> 16); } private set { _data_0_3 |= ((uint)value << 16) & 0x00FF0000u; } }

    /// <summary>Returns the padding length</summary>
    public ushort PadLength { get { return (ushort)((_data_0_3 & 0x0000FFFFu)); } private set { _data_0_3 |= value & 0x0000FFFFu; } }

    /// <summary>Returns the format behavior for negative signed integers</summary>
    public NegativeFormatType NegativeFormat { get { return (NegativeFormatType)((_data_8_15 & 0xC000000000000000u) >> 62); } private set { _data_8_15 |= ((uint)value << 62) & 0xC000000000000000u; } }

    /// <summary>Returns the format behavior for integers</summary>
    public NumericBase IntegerBase { get { return (NumericBase)((_data_8_15 & 0x3800000000000000u) >> 59); } private set { _data_8_15 |= ((uint)value << 59) & 0x3800000000000000u; } }

    /// <summary>Returns the format string for datetime representation</summary>
    public string DataFormat { get { return _dataFmt; } private set { _dataFmt = value; } }

    /// <summary>Returns the expected length without padding</summary>
    public ushort ExpectedLength { get { return _data_4_5; } private set { _data_4_5 = value; } }

    /// <summary>Returns the number of decimal places for floating-point numerics</summary>
    public byte DecimalPlaces { get { return (byte)((_data_8_15 & 0x00F0000000000000uL) >> 52); } private set { _data_8_15 |= ((uint)value << 52) & 0x00F0000000000000uL; } }

    /// <summary>Returns the number of integer places for floating-point numerics</summary>
    public byte IntegerPlaces { get { return (byte)((_data_8_15 & 0x000F000000000000uL) >> 48); } private set { _data_8_15 |= ((uint)value << 48) & 0x000F000000000000uL; } }

    /// <summary>Creates a format instruction for generating a string from an unsigned 8-bit integer value</summary>
    public FormatInstruction(sbyte value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._8BIT;
      Type = StoredType.SIGNED_INTEGER;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      _valueSB = value;
      
      RecalcLength(value);
    }

    /// <summary>Creates a format instruction for generating a string from an signed 8-bit integer value</summary>
    public FormatInstruction(byte value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._8BIT;
      Type = StoredType.UNSIGNED_INTEGER;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      _valueB = value;

      RecalcLength(value);
    }

    /// <summary>Creates a format instruction for generating a string from an unsigned 16-bit integer value</summary>
    public FormatInstruction(ushort value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._16BIT;
      Type = StoredType.UNSIGNED_INTEGER;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      _valueUS = value;

      RecalcLength(value);
    }

    /// <summary>Creates a format instruction for generating a string from an signed 16-bit integer value</summary>
    public FormatInstruction(short value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._16BIT;
      Type = StoredType.SIGNED_INTEGER;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      _valueS = value;

      RecalcLength(value);
    }

    /// <summary>Creates a format instruction for generating a string from an unsigned 32-bit integer value</summary>
    public FormatInstruction(uint value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._32BIT;
      Type = StoredType.UNSIGNED_INTEGER;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      _valueUI = value;

      RecalcLength(value);
    }

    /// <summary>Creates a format instruction for generating a string from an signed 32-bit integer value</summary>
    public FormatInstruction(int value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._32BIT;
      Type = StoredType.SIGNED_INTEGER;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      _valueI = value;

      RecalcLength(value);
    }

    /// <summary>Creates a format instruction for generating a string from an unsigned 64-bit integer value</summary>
    public FormatInstruction(ulong value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._64BIT;
      Type = StoredType.UNSIGNED_INTEGER;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      _valueUL = value;

      RecalcLength(value);
    }

    /// <summary>Creates a format instruction for generating a string from an signed 64-bit integer value</summary>
    public FormatInstruction(long value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._64BIT;
      Type = StoredType.SIGNED_INTEGER;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      _valueL = value;

      RecalcLength(value);
    }

    /// <summary>Creates a format instruction for generating a string from a 32-bit floating-point value</summary>
    public FormatInstruction(float value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', byte decimalPlaces = 0, NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._32BIT;
      Type = StoredType.FLOATING_POINT;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      DecimalPlaces = decimalPlaces;
      _valueF = value;

      RecalcLength(value);
    }

    /// <summary>Creates a format instruction for generating a string from a 64-bit floating-point value</summary>
    public FormatInstruction(double value, NumericBase @base = NumericBase.DECIMAL, ushort padding = 0, PadAlignmentType padAlignment = PadAlignmentType.LEFT, char padCharacter = '0', byte decimalPlaces = 0, NegativeFormatType negativeFormat = NegativeFormatType.AFTER_PADDING) : this()
    {
      Size = StoredSize._64BIT;
      Type = StoredType.FLOATING_POINT;
      PadLength = padding;
      PadAlignment = padAlignment;
      PadCharacter = padCharacter;
      NegativeFormat = negativeFormat;
      IntegerBase = @base;
      DecimalPlaces = decimalPlaces;
      _valueD = value;

      // to-do
      RecalcLength((float)value);
    }


    /// <summary>Returns the string representing the value</summary>
    public string Format()
    {
      StringBuilder sb = ObjectPool<StringBuilder>.GetStaticPool().GetNew();
      Concat_Long(sb);
      //Concat(sb);
      string result = sb.ToString();
      ObjectPool<StringBuilder>.GetStaticPool().Return(sb);
      return result;
    }

    /// <summary>Appends the string representing the value to a StringBuilder</summary>
    public void Format(StringBuilder sb)
    {
      Concat_Long(sb);
    }

    private int GetBaseNumber()
    {
      switch (IntegerBase)
      {
        default:
        case NumericBase.DECIMAL:
          return 10;
        case NumericBase.HEXADECIMAL:
          return 16;
        case NumericBase.OCTAL:
          return 8;
        case NumericBase.BINARY:
          return 2;
      }
    }

    private void RecalcLength(byte value)
    {
      ushort len = 1;
      int bnum = GetBaseNumber();
      byte val = (byte)bnum;
      byte b = (byte)bnum;
      byte maxval = (byte)(byte.MaxValue / b); // ensure that val does not exceed the max value

      while (val < value)
      {
        len++;
        val *= b;
        if (val >= maxval)
        {
          len++;
          break;
        }
      }

      ExpectedLength = len.Max(IntegerPlaces);
    }

    private void RecalcLength(sbyte value)
    {
      ushort len;
      int bnum = GetBaseNumber();

      if (value < 0 && NegativeFormat == NegativeFormatType.OVERFLOW)
      {
        RecalcLength(byte.MaxValue & (byte)value);
        return;
      }
      else
      {
        len = 1;
        int uvalue = value;
        // NOTE: -long.MinValue == long.MinValue . We make a special case to override this
        if (value == sbyte.MinValue)
        {
          len = 2;
          uvalue = -(value + 1);
        }
        else if (value < 0)
        {
          len = 2;
          uvalue = -uvalue;
        }

        int val = bnum;
        int b = bnum;
        int maxval = sbyte.MaxValue / b; // ensure that val does not exceed the max value

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
      ExpectedLength = len.Max(IntegerPlaces);
    }

    private void RecalcLength(ushort value)
    {
      ushort len = 1;
      int bnum = GetBaseNumber();
      ushort val = (ushort)bnum;
      ushort b = (ushort)bnum;
      ushort maxval = (ushort)(ushort.MaxValue / b); // ensure that val does not exceed the max value

      while (val < value)
      {
        len++;
        val *= b;
        if (val >= maxval)
        {
          len++;
          break;
        }
      }

      ExpectedLength = len.Max(IntegerPlaces);
    }

    private void RecalcLength(short value)
    {
      ushort len;
      int bnum = GetBaseNumber();

      if (value < 0 && NegativeFormat == NegativeFormatType.OVERFLOW)
      {
        RecalcLength(ushort.MaxValue & (ushort)value);
        return;
      }
      else
      {
        len = 1;
        int uvalue = value;
        // NOTE: -long.MinValue == long.MinValue . We make a special case to override this
        if (value == short.MinValue)
        {
          len = 2;
          uvalue = -(value + 1);
        }
        else if (value < 0)
        {
          len = 2;
          uvalue = -uvalue;
        }

        int val = bnum;
        int b = bnum;
        int maxval = short.MaxValue / b; // ensure that val does not exceed the max value

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
      ExpectedLength = len.Max(IntegerPlaces);
    }

    private void RecalcLength(uint value)
    {
      ushort len = 1;
      int bnum = GetBaseNumber();
      uint val = (uint)bnum;
      uint b = (uint)bnum;
      uint maxval = uint.MaxValue / b; // ensure that val does not exceed the max value

      while (val < value)
      {
        len++;
        val *= b;
        if (val >= maxval)
        {
          len++;
          break;
        }
      }

      ExpectedLength = len.Max(IntegerPlaces);
    }

    private void RecalcLength(int value)
    {
      ushort len;
      int bnum = GetBaseNumber();

      if (value < 0 && NegativeFormat == NegativeFormatType.OVERFLOW)
      {
        RecalcLength(uint.MaxValue & (uint)value);
        return;
      }
      else
      {
        len = 1;
        int uvalue = value;
        // NOTE: -long.MinValue == long.MinValue . We make a special case to override this
        if (value == int.MinValue)
        {
          len = 2;
          uvalue = -(value + 1);
        }
        else if (value < 0)
        {
          len = 2;
          uvalue = -uvalue;
        }

        int val = bnum;
        int b = bnum;
        int maxval = int.MaxValue / b; // ensure that val does not exceed the max value

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
      ExpectedLength = len.Max(IntegerPlaces);
    }

    private void RecalcLength(ulong value)
    {
      ushort len = 1;
      int bnum = GetBaseNumber();
      ulong val = (ulong)bnum;
      ulong b = (ulong)bnum;
      ulong maxval = ulong.MaxValue / b; // ensure that val does not exceed the max value

      while (val < value)
      {
        len++;
        val *= b;
        if (val >= maxval)
        {
          len++;
          break;
        }
      }

      ExpectedLength = len.Max(IntegerPlaces);
    }

    private void RecalcLength(long value)
    {
      ushort len;
      int bnum = GetBaseNumber();

      if (value < 0 && NegativeFormat == NegativeFormatType.OVERFLOW)
      {
        RecalcLength(ulong.MaxValue & (ulong)value);
        return;
      }
      else
      {
        len = 1;
        long uvalue = value;
        // NOTE: -long.MinValue == long.MinValue . We make a special case to override this
        if (value == long.MinValue)
        {
          len = 2;
          uvalue = -(value + 1);
        }
        else if (value < 0)
        {
          len = 2;
          uvalue = -uvalue;
        }

        long val = bnum;
        long b = bnum;
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
      ExpectedLength = len.Max(IntegerPlaces);
    }

    private void RecalcLength(float value)
    {
      // integer
      ushort len = 1;
      int bnum = GetBaseNumber();
      ulong val = (ulong)bnum;
      ulong b = (ulong)bnum;
      ulong maxval = ulong.MaxValue / b; // ensure that val does not exceed the max value

      while (val < value)
      {
        len++;
        val *= b;
        if (val >= maxval)
        {
          len++;
          break;
        }
      }

      ExpectedLength = len.Max(IntegerPlaces);
    }

    private void Concat_Long(StringBuilder sb)
    {
      // length is already precalulated
      // fill the space with pad characters
      ushort len = ExpectedLength;
      ushort lenWithPad = ExpectedLength.Max(PadLength);
      sb.Append(PadCharacter, lenWithPad);

      // Identify the position of the final digit
      int strfirst = sb.Length - lenWithPad;
      int strpos = sb.Length;
      if (PadAlignment == PadAlignmentType.RIGHT)
      {
        strpos -= lenWithPad - ExpectedLength;
      }

      long mvalue = _valueL;
      long b = GetBaseNumber();
      if (_valueL < 0)
      {
        if (NegativeFormat == NegativeFormatType.OVERFLOW)
        {
          // We're writing backwards, one character at a time.
          ulong lvalue = ulong.MaxValue & (ulong)mvalue;
          ulong lb = (ulong)b;
          for (int i = len - 1; i >= 0; i--)
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
          for (int i = len - 1; i >= 0; i--)
          {
            strpos--;
            // Lookup from static char array, to cover hex values too
            sb[strpos] = ArrayConstants.HexadecimalDigits[-(mvalue % b)];
            mvalue /= b;
            if (i == 0)
            {
              if (NegativeFormat == NegativeFormatType.BEFORE_PADDING)
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
        for (int i = len - 1; i >= 0; i--)
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
