namespace Primrose.Primitives.Strings
{
  /// <summary>Signals whether the padding characters are to be inserted to the left or to the right</summary>
  public enum PadAlignmentType

  {
    /// <summary>Signals that the padding characters are to be inserted to the left</summary>
    LEFT = 0,

    /// <summary>Signals that the padding characters are to be inserted to the right</summary>
    RIGHT = 1
  }

  /// <summary>Signals handling behavior of negative numbers (for numerics)</summary>
  public enum NegativeFormatType
  {
    /// <summary>Signals that the padding characters are to be inserted before the sign character</summary>
    AFTER_PADDING = 0,

    /// <summary>Signals that the padding characters are to be inserted after the sign character</summary>
    BEFORE_PADDING = 1,

    /// <summary>Signals that the negative sign be not rendered. Instead, the number will be wrapped around its maximum</summary>
    OVERFLOW = 2,
  }
}
