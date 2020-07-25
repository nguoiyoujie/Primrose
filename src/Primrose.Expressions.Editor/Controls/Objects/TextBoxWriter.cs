using Primrose.Primitives.Extensions;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Primrose.Expressions.Editor.Controls.Objects
{
  public class TextBoxWriter : TextWriter
  {
    private readonly Control _control;
    public TextBoxWriter(Control control)
    {
      _control = control;
    }

    public override void Write(object value) { _control.Text += value; }
    public override void Write(char value) { _control.Text += value; }
    public override void Write(string value) { _control.Text += value; }
    public override void Write(string format, object a1) { _control.Text += format.F(a1); }
    public override void Write(string format, object a1, object a2) { _control.Text += format.F(a1,a2); }
    public override void Write(string format, object a1, object a2, object a3) { _control.Text += format.F(a1,a2,a3); }
    public override void Write(string format, params object[] args) { _control.Text += format.F(args); }
    public override void Write(bool value) { _control.Text += value; }
    public override void Write(decimal value) { _control.Text += value; }
    public override void Write(int value) { _control.Text += value; }
    public override void Write(uint value) { _control.Text += value; }
    public override void Write(long value) { _control.Text += value; }
    public override void Write(ulong value) { _control.Text += value; }
    public override void Write(float value) { _control.Text += value; }
    public override void Write(double value) { _control.Text += value; }
    public override void Write(char[] buffer, int index, int count) { _control.Text += new string(buffer, index, count); }
    public override void Write(char[] buffer) { _control.Text += new string(buffer); }

    public override void WriteLine(object value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(char value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(string value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(string format, object a1) { _control.Text += format.F(a1) + Environment.NewLine; }
    public override void WriteLine(string format, object a1, object a2) { _control.Text += format.F(a1, a2) + Environment.NewLine; }
    public override void WriteLine(string format, object a1, object a2, object a3) { _control.Text += format.F(a1, a2, a3) + Environment.NewLine; }
    public override void WriteLine(string format, params object[] args) { _control.Text += format.F(args) + Environment.NewLine; }
    public override void WriteLine(bool value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(decimal value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(int value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(uint value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(long value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(ulong value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(float value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(double value) { _control.Text += value + Environment.NewLine; }
    public override void WriteLine(char[] buffer, int index, int count) { _control.Text += new string(buffer, index, count) + Environment.NewLine; }
    public override void WriteLine(char[] buffer) { _control.Text += new string(buffer) + Environment.NewLine; }

    public override void Flush() { }

    public override Encoding Encoding
    {
      get { return Encoding.Unicode; }
    }
  }
}
