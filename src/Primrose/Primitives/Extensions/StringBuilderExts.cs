using System;
using System.Diagnostics;
using System.Text;

namespace Primrose.Primitives.Extensions
{
  /// <summary>Extension methods for the 'StringBuilder' standard .NET class, to allow garbage-free concatenation of formatted strings with a variable set of arguments.</summary>
  public static class StringBuilderExts
  {
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // File:	StringBuilderExtFormat.cs
    // Date:	11th March 2010
    // Author:	Gavin Pugh
    // Details:	Extension methods for the 'StringBuilder' standard .NET class, to allow garbage-free concatenation of
    //			formatted strings with a variable set of arguments.
    //
    // Copyright (c) Gavin Pugh 2010 - Released under the zlib license: http://www.opensource.org/licenses/zlib-license.php
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private static readonly uint ms_default_decimal_places = 5; //< Matches standard .NET formatting dp's
    private static readonly char ms_default_pad_char = '0';

    /// <summary>Convert a given datetime value to a string and concatenate onto the stringbuilder. Only certain codes are supported</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, DateTime dt_val, string format, int formatStart, int formatLength)
    {
      Debug.Assert(format != null);
      Debug.Assert(formatStart >= 0 && formatStart < format.Length);
      Debug.Assert(formatLength >= 0 && formatStart + formatLength >= 0 && formatStart + formatLength < format.Length);

      char charType = '\0';
      int charCount = 0;
      int i = formatStart;

      do
      {
        if (format[i] != charType)
        {
          // evaluate current charType
          switch (charType)
          {
            case '\0':
              break;

            default:
              {
                string_builder.Append(charType, charCount);
              }
              break;

            case 'y': // year
              {
                int year = dt_val.Year;
                int div = 100;
                for (int d = 2; d < charCount; d++)
                {
                  div *= 10;
                }
                year %= div;
                uint padLen = (charCount <= 1) ? 0u : 2u;
                string_builder.Concat(year, padLen, '0', 10);
              }
              break;

            case 'M': // month
              {
                int month = dt_val.Month;
                if (charCount >= 4)
                {
                  string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(month));
                }
                else if (charCount == 3)
                {
                  string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month));
                }
                else
                {
                  uint padLen = (charCount <= 1) ? 0u : 2u;
                  string_builder.Concat(month, padLen, '0', 10);
                }
              }
              break;

            case 'd': // day, or short date format
              {
                int day = dt_val.Day;
                DayOfWeek dayWeek = dt_val.DayOfWeek;
                if (charCount >= 4)
                {
                  string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.GetDayName(dayWeek));
                }
                else if (charCount == 3)
                {
                  string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.GetAbbreviatedDayName(dayWeek));
                }
                else
                {
                  uint padLen = (charCount <= 1) ? 0u : 2u;
                  string_builder.Concat(day, padLen, '0', 10);
                }
              }
              break;

            case 't': // AM/PM
              {
                int hour = dt_val.Hour;
                if (charCount >= 2)
                {
                  if (hour < 12)
                    string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.AMDesignator);
                  else
                    string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.PMDesignator);
                }
                else
                {
                  if (hour < 12)
                    string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.AMDesignator[0]);
                  else
                    string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.PMDesignator[0]);
                }
              }
              break;

            case 'H': // hour, 24
              {
                int hour = dt_val.Hour;
                uint padLen = (charCount <= 1) ? 0u : 2u;
                string_builder.Concat(hour, padLen, '0', 10);
              }
              break;

            case 'h': // hour, 12
              {
                int hour = dt_val.Hour % 12;
                if (hour == 0) { hour = 12; }
                uint padLen = (charCount <= 1) ? 0u : 2u;
                string_builder.Concat(hour, padLen, '0', 10);
              }
              break;

            case 'm': // minute
              {
                int minute = dt_val.Minute;
                uint padLen = (charCount <= 1) ? 0u : 2u;
                string_builder.Concat(minute, padLen, '0', 10);
              }
              break;

            case 's': // second
              {
                int second = dt_val.Second;
                uint padLen = (charCount <= 1) ? 0u : 2u;
                string_builder.Concat(second, padLen, '0', 10);
              }
              break;

            case 'f': // fractional second
              {
                float float_ms = dt_val.Ticks / TimeSpan.TicksPerSecond % 1;
                string_builder.Concat(float_ms, (uint)charCount, 0, '0');
              }
              break;

            case 'g': // era
              {
                int era = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar.GetEra(dt_val);
                if (charCount >= 2)
                {
                  string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.GetEraName(era));
                }
                else
                {
                  string_builder.Append(System.Globalization.DateTimeFormatInfo.CurrentInfo.GetAbbreviatedEraName(era));
                }
              }
              break;
          }

          charType = format[i];
          charCount = 1;
        }
        else
        {
          charCount++;
        }
        i++;
      } while (i <= formatStart + formatLength + 1); // hack: use the last character '}' to trigger the final pass
      return string_builder;
    }

    /// <summary>Convert a given unsigned integer value to a string and concatenate onto the stringbuilder. Any base value allowed.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount, char pad_char, uint base_val)
    {
      Debug.Assert(pad_amount >= 0);
      Debug.Assert(base_val > 0 && base_val <= 16);

      // Calculate length of integer when written out
      uint length = 0;
      uint length_calc = uint_val;

      do
      {
        length_calc /= base_val;
        length++;
      }
      while (length_calc > 0);

      // Pad out space for writing.
      string_builder.Append(pad_char, (int)Math.Max(pad_amount, length));

      int strpos = string_builder.Length;

      // We're writing backwards, one character at a time.
      while (length > 0)
      {
        strpos--;

        // Lookup from static char array, to cover hex values too
        string_builder[strpos] = ArrayConstants.HexadecimalDigits[uint_val % base_val];

        uint_val /= base_val;
        length--;
      }

      return string_builder;
    }

    /// <summary>Convert a given unsigned integer value to a string and concatenate onto the stringbuilder. Assume no padding and base ten.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val)
    {
      string_builder.Concat(uint_val, 0, ms_default_pad_char, 10);
      return string_builder;
    }

    /// <summary>Convert a given unsigned integer value to a string and concatenate onto the stringbuilder. Assume base ten.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount)
    {
      string_builder.Concat(uint_val, pad_amount, ms_default_pad_char, 10);
      return string_builder;
    }

    /// <summary>Convert a given unsigned integer value to a string and concatenate onto the stringbuilder. Assume base ten.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount, char pad_char)
    {
      string_builder.Concat(uint_val, pad_amount, pad_char, 10);
      return string_builder;
    }

    /// <summary>Convert a given signed integer value to a string and concatenate onto the stringbuilder. Any base value allowed.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount, char pad_char, uint base_val)
    {
      Debug.Assert(pad_amount >= 0);
      Debug.Assert(base_val > 0 && base_val <= 16);

      // Deal with negative numbers
      if (int_val < 0)
      {
        string_builder.Append('-');
        uint uint_val = uint.MaxValue - ((uint)int_val) + 1; //< This is to deal with Int32.MinValue
        string_builder.Concat(uint_val, pad_amount, pad_char, base_val);
      }
      else
      {
        string_builder.Concat((uint)int_val, pad_amount, pad_char, base_val);
      }

      return string_builder;
    }

    /// <summary>Convert a given signed integer value to a string and concatenate onto the stringbuilder. Assume no padding and base ten.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, int int_val)
    {
      string_builder.Concat(int_val, 0, ms_default_pad_char, 10);
      return string_builder;
    }

    /// <summary>Convert a given signed integer value to a string and concatenate onto the stringbuilder. Assume base ten.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount)
    {
      string_builder.Concat(int_val, pad_amount, ms_default_pad_char, 10);
      return string_builder;
    }

    /// <summary>Convert a given signed integer value to a string and concatenate onto the stringbuilder. Assume base ten.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount, char pad_char)
    {
      string_builder.Concat(int_val, pad_amount, pad_char, 10);
      return string_builder;
    }

    /// <summary>Convert a given float value to a string and concatenate onto the stringbuilder</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places, uint pad_amount, char pad_char)
    {
      Debug.Assert(pad_amount >= 0);

      if (decimal_places == 0)
      {
        // No decimal places, just round up and print it as an int

        // Agh, Math.Floor() just works on doubles/decimals. Don't want to cast! Let's do this the old-fashioned way.
        int int_val;
        if (float_val >= 0.0f)
        {
          // Round up
          int_val = (int)(float_val + 0.5f);
        }
        else
        {
          // Round down for negative numbers
          int_val = (int)(float_val - 0.5f);
        }

        string_builder.Concat(int_val, pad_amount, pad_char, 10);
      }
      else
      {
        // we want to round the numeric first before we separate the integer part.
        float f = 0.5f;
        for (int i = 0; i < decimal_places && i < 8; i++) // float precision
        {
          f /= 10;
        }
        float_val += f;
        int int_part = (int)float_val;

        if (int_part == 0 && float_val < 0)
        {
          string_builder.Append('-');
        }

        // Print the integer
        string_builder.Concat(int_part, pad_amount, pad_char, 10);

        // Decimal point
        string_builder.Append('.');

        // Work out remainder we need to print after the d.p.
        float remainder = Math.Abs(float_val - int_part);

        // Multiply up to become an int that we can print
        do
        {
          remainder *= 10;
          decimal_places--;
        }
        while (decimal_places > 0);

        // Rounding was already performed, we need only print the integer component of this number
        string_builder.Concat((uint)remainder, 0, '0', 10);
      }
      return string_builder;
    }

    /// <summary>Convert a given float value to a string and concatenate onto the stringbuilder. Assumes five decimal places, and no padding.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, float float_val)
    {
      string_builder.Concat(float_val, ms_default_decimal_places, 0, ms_default_pad_char);
      return string_builder;
    }

    /// <summary>Convert a given float value to a string and concatenate onto the stringbuilder. Assumes no padding.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places)
    {
      string_builder.Concat(float_val, decimal_places, 0, ms_default_pad_char);
      return string_builder;
    }

    /// <summary>Convert a given float value to a string and concatenate onto the stringbuilder.</summary>
    public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places, uint pad_amount)
    {
      string_builder.Concat(float_val, decimal_places, pad_amount, ms_default_pad_char);
      return string_builder;
    }

    /// <summary>Concatenate a formatted string with arguments</summary>
    public static StringBuilder ConcatFormat<A>(this StringBuilder string_builder, string format, A arg1)
               where A : IConvertible
    {
      return string_builder.ConcatFormat<A, int, int, int>(format, arg1, 0, 0, 0);
    }

    /// <summary>Concatenate a formatted string with arguments</summary>
    public static StringBuilder ConcatFormat<A, B>(this StringBuilder string_builder, string format, A arg1, B arg2)
              where A : IConvertible
              where B : IConvertible
    {
      return string_builder.ConcatFormat<A, B, int, int>(format, arg1, arg2, 0, 0);
    }

    /// <summary>Concatenate a formatted string with arguments</summary>
    public static StringBuilder ConcatFormat<A, B, C>(this StringBuilder string_builder, string format, A arg1, B arg2, C arg3)
              where A : IConvertible
              where B : IConvertible
              where C : IConvertible
    {
      return string_builder.ConcatFormat<A, B, C, int>(format, arg1, arg2, arg3, 0);
    }

    /// <summary>Concatenate a formatted string with arguments</summary>
    public static StringBuilder ConcatFormat<A, B, C, D>(this StringBuilder string_builder, string format, A arg1, B arg2, C arg3, D arg4)
              where A : IConvertible
              where B : IConvertible
              where C : IConvertible
              where D : IConvertible
    {
      int verbatim_range_start = 0;
      int format_start = 0;

      for (int index = 0; index < format.Length; index++)
      {
        if (format[index] == '{')
        {
          // Formatting bit now, so make sure the last block of the string is written out verbatim.
          if (verbatim_range_start < index)
          {
            // Write out unformatted string portion
            string_builder.Append(format, verbatim_range_start, index - verbatim_range_start);
          }

          uint base_value = 10;
          uint padding = 0;
          uint decimal_places = 5; // Default decimal places in .NET libs

          index++;
          char format_char = format[index];
          if (format_char == '{')
          {
            string_builder.Append('{');
            index++;
          }
          else
          {
            index++;

            if (format[index] == ':')
            {
              format_start = index + 1;
              // Extra formatting. This is a crude first pass proof-of-concept. It's not meant to cover
              // comprehensively what the .NET standard library Format() can do.
              index++;

              // Deal with padding
              while (format[index] == '0')
              {
                index++;
                padding++;
              }

              if (format[index] == 'X')
              {
                index++;

                // Print in hex
                base_value = 16;

                // Specify amount of padding ( "{0:X8}" for example pads hex to eight characters
                if ((format[index] >= '0') && (format[index] <= '9'))
                {
                  padding = (uint)(format[index] - '0');
                  index++;
                }
              }
              else if (format[index] == '.')
              {
                index++;

                // Specify number of decimal places
                decimal_places = 0;

                while (format[index] == '0')
                {
                  index++;
                  decimal_places++;
                }
              }
            }


            // Scan through to end bracket
            while (format[index] != '}')
            {
              index++;
            }

            // Have any extended settings now, so just print out the particular argument they wanted
            switch (format_char)
            {
              case '0': string_builder.ConcatFormatValue<A>(arg1, format, format_start, index - format_start - 1, padding, base_value, decimal_places); break;
              case '1': string_builder.ConcatFormatValue<B>(arg2, format, format_start, index - format_start - 1, padding, base_value, decimal_places); break;
              case '2': string_builder.ConcatFormatValue<C>(arg3, format, format_start, index - format_start - 1, padding, base_value, decimal_places); break;
              case '3': string_builder.ConcatFormatValue<D>(arg4, format, format_start, index - format_start - 1, padding, base_value, decimal_places); break;
              default: Debug.Assert(false, "Invalid parameter index"); break;
            }
          }

          // Update the verbatim range, start of a new section now
          verbatim_range_start = (index + 1);
        }
      }

      // Anything verbatim to write out?
      if (verbatim_range_start < format.Length)
      {
        // Write out unformatted string portion
        string_builder.Append(format, verbatim_range_start, format.Length - verbatim_range_start);
      }

      return string_builder;
    }

    /// <summary>The worker method. This does a garbage-free conversion of a generic type, and uses the garbage-free Concat() to add to the stringbuilder</summary>
    private static void ConcatFormatValue<T>(this StringBuilder string_builder, T arg, string format, int formatStart, int formatLength, uint padding, uint base_value, uint decimal_places) where T : IConvertible
    {
      if (arg == null) // nothing to add here
        return;

      switch (arg.GetTypeCode())
      {
        case TypeCode.UInt32:
          {
            string_builder.Concat(arg.ToUInt32(System.Globalization.NumberFormatInfo.CurrentInfo), padding, '0', base_value);
            break;
          }

        case TypeCode.Int32:
          {
            string_builder.Concat(arg.ToInt32(System.Globalization.NumberFormatInfo.CurrentInfo), padding, '0', base_value);
            break;
          }

        case TypeCode.Single:
          {
            string_builder.Concat(arg.ToSingle(System.Globalization.NumberFormatInfo.CurrentInfo), decimal_places, padding, '0');
            break;
          }

        case TypeCode.String:
          {
            string_builder.Append(Convert.ToString(arg));
            break;
          }

        case TypeCode.DateTime:
          {
            string_builder.Concat(arg.ToDateTime(System.Globalization.NumberFormatInfo.CurrentInfo), format, formatStart, formatLength);
            break;
          }

        default:
          {
            Debug.Assert(false, "Unsupported parameter type '{0}'".F(arg.GetTypeCode()));
            break;
          }
      }
    }
  }
}