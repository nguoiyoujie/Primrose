namespace Primrose.Expressions
{
  /// <summary>Defines the types for linting purposes</summary>
  public enum LintType
  {
    /// <summary>Indicates no special linting</summary>
    NONE,

    /// <summary>Indicates linting for headers</summary>
    HEADER,

    /// <summary>Indicates linting for comments</summary>
    COMMENT,

    /// <summary>Indicates linting for object types</summary>
    TYPE,

    /// <summary>Indicates linting for reserved keywords</summary>
    KEYWORD,

    /// <summary>Indicates linting for special literals</summary>
    SPECIALLITERAL,

    /// <summary>Indicates linting for numeric literals</summary>
    NUMERICLITERAL,

    /// <summary>Indicates linting for string literals</summary>
    STRINGLITERAL,

    /// <summary>Indicates linting for function types</summary>
    FUNCTION,

    /// <summary>Indicates linting for variables</summary>
    VARIABLE,

  }
}


