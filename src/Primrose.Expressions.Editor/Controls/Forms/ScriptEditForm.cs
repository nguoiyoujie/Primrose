using Primrose.Primitives.Extensions;
using Primrose.Primitives.Factories;
using Primrose.Primitives.ValueTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Primrose.Expressions.Editor.Controls.Forms
{
  public partial class ScriptEditForm : Form
  {
    private IContext Context;

    private Registry<IHighlighter, ToolStripMenuItem> HighlightAssoc = new Registry<IHighlighter, ToolStripMenuItem>();

    public ScriptEditForm()
    {
      InitializeComponent();
      Context = new ContextBase();

      ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
      sfd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
      UpdateTitle();
      UpdateStats();

      HighlightAssoc.Add(NoHighlighter.Instance, langNoneToolStripMenuItem);
      HighlightAssoc.Add(INIHighlighter.Instance, langINIToolStripMenuItem);
      HighlightAssoc.Add(ScriptHighlighter.Instance, langScriptToolStripMenuItem);

      foreach (string s in Context.ValFuncRef)
        lboxFunctions.Items.Add(s);
    }

    private void ScriptEditForm_Load(object sender, EventArgs e)
    {
      newFileToolStripMenuItem_Click(null, null);
    }

    private void UpdateTitle()
    {
      bool chg = false;
      tpEditor tp = GetCurrentEditor();
      if (tp != null)
      {
        chg = tp.IsChanged;
        if (tp.CurrPath != null)
          Text = "{0} [{1}]".F(Globals.Title, tp.CurrPath + (chg ? "*" : ""));
        else
          Text = "{0} [{1}]".F(Globals.Title, "Untitled Document" + (chg ? "*" : ""));
      }
      else
        Text = Globals.Title;

    }

    private void UpdateStats()
    {
      tpEditor tp = GetCurrentEditor();
      if (tp != null)
        rtfSelStatLabel.Text = "Ln: {0}    Col: {1}    Len: {2}".F(tp.Line, tp.Column, tp.Length);
    }

    private tpEditor GetCurrentEditor()
    {
      if (tcEditor.TabPages.Count == 0)
        return null;
      return tcEditor.SelectedTab as tpEditor;
    }

    private void newFileToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
      tpEditor tp = new tpEditor();
      tp.New();
      tp.TextChanged += editor_TextChanged;
      tp.Editor_SelectionChanged = UpdateStats;
      tcEditor.Add(tp);

      tcEditor.SelectedTab = tp;
      UpdateTitle();
    }

    private void openFileToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
      tpEditor tp = new tpEditor();
      if (tp.Open())
      {
        tp.TextChanged += editor_TextChanged;
        tp.Editor_SelectionChanged = UpdateStats;
        tcEditor.Controls.Add(tp);

        tp.Text = Path.GetFileName(tp.CurrPath);
        tcEditor.SelectedTab = tp;
        UpdateTitle();
      }
      else
        tp.Dispose();
    }

    private void saveFileToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
      tpEditor tp = GetCurrentEditor();
      if (tp != null)
      {
        tp.QuickSave();
        tp.Text = Path.GetFileName(tp.CurrPath);
      }
      UpdateTitle();
    }

    private void saveAsToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
      tpEditor tp = GetCurrentEditor();
      if (tp != null)
      {
        tp.SaveAs();
        tp.Text = Path.GetFileName(tp.CurrPath);
      }
      UpdateTitle();
    }

    private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
      Close();
    }

    private void ScriptEditForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      foreach (TabPage p in tcEditor.TabPages)
      {
        tpEditor tp = p as tpEditor;
        if (tp != null)
          if (!tp.FileClose())
            e.Cancel = true;
      }
    }

    private void editor_TextChanged(object sender, EventArgs e)
    {
      UpdateTitle();
    }

    private void langINIToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GetCurrentEditor()?.Higlight(INIHighlighter.Instance);
    }

    private void langScriptToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GetCurrentEditor()?.Higlight(ScriptHighlighter.Instance);
    }

    private void langNoneToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GetCurrentEditor()?.Higlight(NoHighlighter.Instance);
    }

    private void lboxFunctions_SelectedIndexChanged(object sender, EventArgs e)
    {
      lboxSig.Items.Clear();
      string s = lboxFunctions.SelectedItem?.ToString();
      if (lboxFunctions.SelectedItem != null)
      {
        for (int i = 0; i < 12; i++)
        {
          IValFunc iv = Context.ValFuncs.Get(new Pair<string, int>(s, i));
          if (iv != null)
          {
            Type t = iv.GetType();
            if (!t.IsGenericType)
              lboxSig.Items.Add(s + "()");
            else
            {
              Type[] ts = t.GetGenericArguments();
              string[] ss = new string[i];
              for (int j = 0; j < i; j++)
              {
                ss[j] = ts[j].Name;
              }
              lboxSig.Items.Add(s + "(" + string.Join(", ", ss) + ")");
            }
          }
        }
      }
    }

    private void checkToolStripMenuItem_Click(object sender, EventArgs e)
    {
      tpEditor tp = GetCurrentEditor();
      if (tp != null)
        tp.DoCheck(Context);
    }

    private void tcEditor_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateTitle();
    }

    private void setContextDllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (ofd_dll.ShowDialog() == DialogResult.OK)
      {
        string path_dll = ofd_dll.FileName;
        Assembly asm = null;

        try
        {
          asm = Assembly.LoadFile(path_dll);
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error loading assembly file '{0}'!\n\n{1}".F(path_dll, ex.Message), Globals.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        List<Type> eligible_types = new List<Type>();
        try
        {
          Type[] types = asm.GetTypes();
          foreach (Type t in types)
            foreach (Type i in t.GetInterfaces())
              if (i == typeof(IContext))
                eligible_types.Add(t);

          if (eligible_types.Count == 0)
          {
            MessageBox.Show("No suitable context found from assembly file '{0}'!".F(path_dll), Globals.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }
        catch (ReflectionTypeLoadException rex)
        {
          string load_ex = rex.LoaderExceptions.Length > 0 ? rex.LoaderExceptions[0].ToString() : "";
          MessageBox.Show("Error loading types from assembly file '{0}':\n\n{1}\n\n{2}".F(path_dll, rex.Message, load_ex), Globals.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }
        catch (Exception ex)
        {
          MessageBox.Show("Error loading types from assembly file '{0}':\n\n{1}".F(path_dll, ex.Message), Globals.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        string[] sitems = new string[eligible_types.Count];
        for (int i = 0; i < eligible_types.Count; i++)
          sitems[i] = eligible_types[i].FullName;

        SelectItemForm sif = new SelectItemForm(sitems);
        if (sif.ShowDialog() == DialogResult.OK)
        {
          foreach (Type t in eligible_types)
            if (sif.Item == t.FullName)
            {
              try
              {
                Context = (IContext)Activator.CreateInstance(t);
              }
              catch (Exception ex)
              {
                MessageBox.Show("Error creating context '{0}':\n\n{1}".F(t.Name, ex.Message), Globals.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
              }

              lboxFunctions.Items.Clear();
              foreach (string s in Context.ValFuncRef)
                lboxFunctions.Items.Add(s);

              lboxSig.Items.Clear();
              ScriptHighlighter.Instance.Context = Context;
            }
        }
        sif.Dispose();
      }
    }
  }
}
