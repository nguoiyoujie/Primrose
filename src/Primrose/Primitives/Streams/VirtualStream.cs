using Primrose.Primitives.Factories;
using System;
using System.IO;
using System.Text;

namespace Primrose.Primitives.Streams
{
  /// <summary>Defines a virtualized stream residing in another stream</summary>
  public class VirtualStream : Stream
  {
    /// <summary>The underlying stream</summary>
    public Stream BaseStream { get; internal protected set; }

    // locator settings
    private long _baseOffset;
    private long _size;
    private long _pos;

    // buffer settings
    private byte[] _buffer;
    private readonly bool _isBuffered;
    private bool _isBufferInitialized;

    /// <summary>Defines a virtualized stream residing in another stream</summary>
    /// <param name="baseStream">The underlying stream content</param>
    /// <param name="baseOffset">The offset position in the underlying stream</param>
    /// <param name="streamLength">The length of the virtual snapshot</param>
    /// <param name="isBuffered">Denotes whether the virtual stream maintains its own content buffer instead of manipulating the base stream directly</param>
    public VirtualStream(Stream baseStream, long baseOffset, long streamLength, bool isBuffered = false)
    {
      BaseStream = baseStream;
      _size = streamLength;
      _baseOffset = baseOffset;
      _isBuffered = isBuffered;
    }

    /// <summary>Defines a virtualized stream residing in another stream</summary>
    /// <param name="baseStream">The underlying stream content</param>
    /// <param name="isBuffered">Denotes whether the virtual stream maintains its own content buffer instead of manipulating the base stream directly</param>
    public VirtualStream(Stream baseStream, bool isBuffered = false)
    {
      BaseStream = baseStream;
      _baseOffset = 0;
      _size = baseStream?.Length ?? 0;
      _isBuffered = isBuffered;
    }

    /// <inheritdoc />
    public override bool CanRead
    {
      get { return BaseStream.CanRead && _pos < _size; }
    }

    /// <inheritdoc />
    public override bool CanWrite
    {
      get { return BaseStream.CanWrite && !_isBuffered && _pos < _size; }
    }

    /// <inheritdoc />
    public override long Length
    {
      get { return _size; }
    }

    /// <inheritdoc />
    public override void Flush()
    {
    }

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
      count = Math.Min(count, (int)(Length - Position));
      if (_isBuffered)
      {
        if (!_isBufferInitialized)
          InitBuffer();

        Array.Copy(_buffer, _pos, buffer, offset, count);
      }
      else
      {
        BaseStream.Position = _baseOffset + _pos;
        BaseStream.Read(buffer, offset, count);
      }
      _pos += count;
      return count;
    }

    /// <summary>Reads a sequence of bytes as a null-terminated string</summary>
    /// <param name="count">The number of characters</param>
    public string ReadCString(int count)
    {
      var arr = Read(count);
      var sb = ObjectPool<StringBuilder>.GetStaticPool().GetNew();
      int i = 0;
      while (i < count && arr[i] != 0)
        sb.Append((char)arr[i++]);
      return sb.ToString();
    }

    private void InitBuffer()
    {
      BaseStream.Position = _baseOffset + _pos;
      _buffer = new byte[_size];
      BaseStream.Read(_buffer, 0, (int)_size);
      _isBufferInitialized = true;
    }

    /// <summary>Reads a sequence of bytes of a specific length</summary>
    public byte[] Read(int numBytes)
    {
      var ret = new byte[numBytes];
      Read(ret, 0, numBytes);
      return ret;
    }

    /// <summary>Reads a sequence of signed bytes of a specific length</summary>
    public sbyte[] ReadSigned(int numBytes)
    {
      var b = new byte[numBytes];
      Read(b, 0, numBytes);
      sbyte[] ret = new sbyte[numBytes];
      Buffer.BlockCopy(b, 0, ret, 0, b.Length);
      return ret;
    }

    /// <summary>Reads a single byte</summary>
    public new byte ReadByte()
    {
      return ReadUInt8();
    }

    /// <summary>Reads a single signed byte</summary>
    public sbyte ReadSByte()
    {
      return unchecked((sbyte)ReadUInt8());
    }

    /// <summary>Reads a single byte</summary>
    public byte ReadUInt8()
    {
      return Read(1)[0];
    }

    /// <summary>Reads a single 32-bit integer value</summary>
    public int ReadInt32()
    {
      return BitConverter.ToInt32(Read(sizeof(Int32)), 0);
    }

    /// <summary>Reads a single 32-bit signed integer value</summary>
    public uint ReadUInt32()
    {
      return BitConverter.ToUInt32(Read(sizeof(UInt32)), 0);
    }

    /// <summary>Reads a single 16-bit integer value</summary>
    public short ReadInt16()
    {
      return BitConverter.ToInt16(Read(sizeof(Int16)), 0);
    }

    /// <summary>Reads a single 16-bit signed integer value</summary>
    public ushort ReadUInt16()
    {
      return BitConverter.ToUInt16(Read(sizeof(UInt16)), 0);
    }

    /// <summary>Reads a single 16-bit floating-point value</summary>
    public float ReadFloat()
    {
      return BitConverter.ToSingle(Read(sizeof(Single)), 0);
    }

    /// <summary>Reads a single 32-bit floating-point value</summary>
    public double ReadDouble()
    {
      return BitConverter.ToDouble(Read(sizeof(Double)), 0);
    }

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count)
    {
      if (count + Position > Length)
      {
        throw new ArgumentException("The sum of offset and count is greater than the buffer length.");
      }
      if (!_isBuffered)
      {
        BaseStream.Position = _baseOffset + _pos;
        BaseStream.Write(buffer, offset, count);
      }
      _pos += count;
    }

    /// <inheritdoc />
    public override void Close()
    {
      base.Close();
      // virtual stream shall not close the base stream.
      //BaseStream.Close();
    }

    /// <inheritdoc />
    public override void SetLength(long value)
    {
      _size = value;
    }

    /// <inheritdoc />
    public override long Position
    {
      get
      {
        return _pos;
      }
      set
      {
        _pos = value;
        if (!_isBuffered && _pos + _baseOffset != BaseStream.Position)
          BaseStream.Seek(_pos + _baseOffset, SeekOrigin.Begin);
      }
    }

    /// <summary>Indicates the remaining length of the stream</summary>
    public long Remaining
    {
      get { return Length - _pos; }
    }

    /// <summary>Indicates whether the stream is at end-of-file</summary>
    public bool Eof
    {
      get { return Remaining <= 0; }
    }

    /// <inheritdoc />
    public override bool CanSeek
    {
      get { return BaseStream.CanSeek; }
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin)
    {
      switch (origin)
      {
        case SeekOrigin.Begin:
          Position = offset;
          break;
        case SeekOrigin.Current:
          Position += offset;
          break;
        case SeekOrigin.End:
          Position = Length - offset;
          break;
      }
      return Position;
    }
  }
}
