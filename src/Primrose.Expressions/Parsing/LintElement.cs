using Primrose.Primitives.Extensions;

namespace Primrose.Expressions
{
  /// <summary>Specifies a linter element at a specific line and position</summary>
  public struct LintElement
  {
    /// <summary>The line where the lint is applied</summary>
    public readonly int Line;

    /// <summary>The start position where the lint is applied</summary>
    public readonly int Position;

    /// <summary>The lint applied</summary>
    public readonly LintType Lint;

    /// <summary>Specifies a linter element at a specific line and position</summary>
    public LintElement(int line, int position, LintType lint)
    {
      Line = line;
      Position = position;
      Lint = lint;
    }

    /// <summary>Provides a string representation of the linter element</summary>
    public override string ToString()
    {
      return "{{{0}, line {1}:{2}}}".F(Lint, Line, Position);
    }
  }
}


