namespace Primrose.Expressions.Editor
{
  public interface IHighlighter
  {
    void Highlight(FlickerFreeRichEditTextBox box);
    void Highlight(FlickerFreeRichEditTextBox box, int line);
  }
}
