using Primrose.Primitives.Observables;
using System.Collections.Generic;

namespace Primrose.Primitives.Triggers
{
  public enum MatchType
  {
    EQUALS = 0,
    NOT_EQUALS = 1,
    SAME_TYPE = 2,

    // Number
    NUMERIC_EQUALS = 10,
    NUMERIC_NOT_EQUALS = 11,
    LESS_THAN = 12,
    MORE_THAN = 13,
    LESS_THAN_OR_EQUAL_TO = 14,
    MORE_THAN_OR_EQUAL_TO = 15,

    // String
    SAME_LENGTH = 20,
    LENGTH_SHORTER_THAN = 21,
    LENGTH_LONGER_THAN = 22,
    LENGTH_SHORTER_THAN_OR_EQUAL_TO = 23,
    LENGTH_LONGER_THAN_OR_EQUAL_TO = 24,
    CONTAINS = 25,
    IS_CONTAINED_BY = 26,
    STARTS_WITH = 27,
    ENDS_WITH = 28,

    CASE_INSENSITIVE_CONTAINS = 35,
    CASE_INSENSITIVE_IS_CONTAINED_BY = 36,
    CASE_INSENSITIVE_STARTS_WITH = 37,
    CASE_INSENSITIVE_ENDS_WITH = 38
  }
}
