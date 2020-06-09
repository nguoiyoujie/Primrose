namespace Primrose.Primitives.Triggers
{
  /// <summary>
  /// Provides a listing of comparison options
  /// </summary>
  public enum MatchType
  {
    /// <summary>Matches if two objects are equal</summary>
    EQUALS = 0,

    /// <summary>Matches if two objects are not equal</summary>
    NOT_EQUALS = 1,

    /// <summary>Matches if two objects have the same type</summary>
    SAME_TYPE = 2,

    // Number
    /// <summary>Matches if two objects are comparable and are equal</summary>
    NUMERIC_EQUALS = 10,

    /// <summary>Matches if two objects are comparable and are not equal</summary>
    NUMERIC_NOT_EQUALS = 11,

    /// <summary>Matches if two objects are comparable and the first element precedes the second</summary>
    LESS_THAN = 12,

    /// <summary>Matches if two objects are comparable and the first element succeeds the second</summary>
    MORE_THAN = 13,

    /// <summary>Matches if two objects are comparable and the first element precedes or equals the second</summary>
    LESS_THAN_OR_EQUAL_TO = 14,

    /// <summary>Matches if two objects are comparable and the first element succeeds or equals the second</summary>
    MORE_THAN_OR_EQUAL_TO = 15,

    // String
    /// <summary>Matches if two objects are strings and have the same length</summary>
    SAME_LENGTH = 20,

    /// <summary>Matches if two objects are strings and the first element has a character length shorter than the second</summary>
    LENGTH_SHORTER_THAN = 21,

    /// <summary>Matches if two objects are strings and the first element has a character length longer than the second</summary>
    LENGTH_LONGER_THAN = 22,

    /// <summary>Matches if two objects are strings and the first element has a character length shorter than or equal to the second</summary>
    LENGTH_SHORTER_THAN_OR_EQUAL_TO = 23,

    /// <summary>Matches if two objects are strings and the first element has a character length shorter than or equal to the second</summary>
    LENGTH_LONGER_THAN_OR_EQUAL_TO = 24,

    /// <summary>Matches if two objects are strings and the first element contains the second</summary>
    CONTAINS = 25,

    /// <summary>Matches if two objects are strings and the second element contains the first</summary>
    IS_CONTAINED_BY = 26,

    /// <summary>Matches if two objects are strings and the first element starts with the second</summary>
    STARTS_WITH = 27,

    /// <summary>Matches if two objects are strings and the first element ends with the second</summary>
    ENDS_WITH = 28,

    /// <summary>Matches if two objects are strings and the first element contains the second, disregarding character case</summary>
    CASE_INSENSITIVE_CONTAINS = 35,

    /// <summary>Matches if two objects are strings and the second element contains the first, disregarding character case</summary>
    CASE_INSENSITIVE_IS_CONTAINED_BY = 36,

    /// <summary>Matches if two objects are strings and the first element starts with the second, disregarding character case</summary>
    CASE_INSENSITIVE_STARTS_WITH = 37,

    /// <summary>Matches if two objects are strings and the first element ends with the second, disregarding character case</summary>
    CASE_INSENSITIVE_ENDS_WITH = 38
  }
}
