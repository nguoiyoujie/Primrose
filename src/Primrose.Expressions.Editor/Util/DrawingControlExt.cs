using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Primrose.Expressions.Editor.Util
{
  // Adapted from https://stackoverflow.com/questions/487661/how-do-i-suspend-painting-for-a-control-and-its-children
  internal static class DrawingControlExt
  {
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

    [DllImport("gdi32.dll")]
    public static extern int BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, int rop);

    private const int WM_SETREDRAW = 11;

    public static void SuspendDrawing(this Control parent)
    {
      SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
    }

    public static void ResumeDrawing(this Control parent)
    {
      SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
      parent.Refresh();
    }
  }
}
