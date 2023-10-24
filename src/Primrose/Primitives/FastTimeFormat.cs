using System;
using System.Text;

namespace Primrose.Primitives
{
  internal static class FastTimeFormat
  {
    public static string Format(this DateTime dt, string format)
    {
      switch (format)
      {
        case "s":
          return GetISO8601LocalTimeFormat(dt);
        case "u":
          return GetISO8601UniversalTimeFormat(dt);
        default:
          return dt.ToString(format);
      }
    }

    public static void AppendFormat(this DateTime dt, StringBuilder sb, string format)
    {
      switch (format)
      {
        case "s":
          AppendISO8601LocalTimeFormat(dt, sb);
          break;
        case "u":
          AppendISO8601UniversalTimeFormat(dt, sb);
          break;
        default:
          sb.Append(dt.ToString(format));
          break;
      }
    }

    // See https://stackoverflow.com/questions/1176276/how-do-i-improve-the-performance-of-code-using-datetime-tostring
    public static string GetISO8601LocalTimeFormat(this DateTime dt) 
    {
      // Format 's'
      // yyyy-MM-dd'T'HH:mm:ss
      char[] chars = new char[19];
      Write4Chars(chars, 0, dt.Year);
      chars[4] = '-';
      Write2Chars(chars, 5, dt.Month);
      chars[7] = '-';
      Write2Chars(chars, 8, dt.Day);
      chars[10] = 'T';
      Write2Chars(chars, 11, dt.Hour);
      chars[13] = ':';
      Write2Chars(chars, 14, dt.Minute);
      chars[16] = ':';
      Write2Chars(chars, 17, dt.Second);
      return new string(chars);
    }

    public static string GetISO8601UniversalTimeFormat(this DateTime dt)
    {
      // Format 'u'
      // yyyy-MM-dd HH:mm:ss'Z'
      dt = dt.ToUniversalTime();
      char[] chars = new char[20];
      Write4Chars(chars, 0, dt.Year);
      chars[4] = '-';
      Write2Chars(chars, 5, dt.Month);
      chars[7] = '-';
      Write2Chars(chars, 8, dt.Day);
      chars[10] = ' ';
      Write2Chars(chars, 11, dt.Hour);
      chars[13] = ':';
      Write2Chars(chars, 14, dt.Minute);
      chars[16] = ':';
      Write2Chars(chars, 17, dt.Second);
      chars[19] = 'Z';
      return new string(chars);
    }

    public static void AppendISO8601LocalTimeFormat(this DateTime dt, StringBuilder sb)
    {
      // Format 's'
      // yyyy-MM-dd'T'HH:mm:ss
      int len = sb.Length;
      sb.Length += 19;
      Write4Chars(sb, len, dt.Year);
      sb[4] = '-';
      Write2Chars(sb, len + 5, dt.Month);
      sb[7] = '-';
      Write2Chars(sb, len + 8, dt.Day);
      sb[10] = 'T';
      Write2Chars(sb, len + 11, dt.Hour);
      sb[13] = ':';
      Write2Chars(sb, len + 14, dt.Minute);
      sb[16] = ':';
      Write2Chars(sb, len + 17, dt.Second);
    }

    public static void AppendISO8601UniversalTimeFormat(this DateTime dt, StringBuilder sb)
    {
      // Format 'u'
      // yyyy-MM-dd HH:mm:ss'Z'
      dt = dt.ToUniversalTime();
      int len = sb.Length;
      sb.Length += 20;
      Write4Chars(sb, len, dt.Year);
      sb[4] = '-';
      Write2Chars(sb, len + 5, dt.Month);
      sb[7] = '-';
      Write2Chars(sb, len + 8, dt.Day);
      sb[10] = ' ';
      Write2Chars(sb, len + 11, dt.Hour);
      sb[13] = ':';
      Write2Chars(sb, len + 14, dt.Minute);
      sb[16] = ':';
      Write2Chars(sb, len + 17, dt.Second);
      sb[19] = 'Z';
    }

    private static void Write4Chars(char[] chars, int offset, int value)
    {
      chars[offset] = Digit(value / 1000);
      chars[offset + 1] = Digit(value / 100 % 10);
      chars[offset + 2] = Digit(value / 10 % 10);
      chars[offset + 3] = Digit(value % 10);
    }

    private static void Write2Chars(char[] chars, int offset, int value)
    {
      chars[offset] = Digit(value / 10);
      chars[offset + 1] = Digit(value % 10);
    }

    private static void Write4Chars(StringBuilder chars, int offset, int value)
    {
      chars[offset] = Digit(value / 1000);
      chars[offset + 1] = Digit(value / 100 % 10);
      chars[offset + 2] = Digit(value / 10 % 10);
      chars[offset + 3] = Digit(value % 10);
    }

    private static void Write2Chars(StringBuilder chars, int offset, int value)
    {
      chars[offset] = Digit(value / 10);
      chars[offset + 1] = Digit(value % 10);
    }

    private static char Digit(int value)
    {
      return (char)(value + '0');
    }
  }
}
