﻿namespace Primrose.Expressions.Editor.Controls.Forms
{
  partial class ScriptEditForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.langINIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.langScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.langNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.setContextDllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ofd = new System.Windows.Forms.OpenFileDialog();
      this.sfd = new System.Windows.Forms.SaveFileDialog();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.tcEditor = new Primrose.Expressions.Editor.tcEditor();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tpFunctions = new System.Windows.Forms.TabPage();
      this.lboxSig = new System.Windows.Forms.ListBox();
      this.label2 = new System.Windows.Forms.Label();
      this.lboxFunctions = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
      this.rtfSelStatLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.ofd_dll = new System.Windows.Forms.OpenFileDialog();
      this.mainMenuStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tpFunctions.SuspendLayout();
      this.mainStatusStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // mainMenuStrip
      // 
      this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.languageToolStripMenuItem,
            this.scriptToolStripMenuItem});
      this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
      this.mainMenuStrip.Name = "mainMenuStrip";
      this.mainMenuStrip.Size = new System.Drawing.Size(1184, 24);
      this.mainMenuStrip.TabIndex = 1;
      this.mainMenuStrip.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // newFileToolStripMenuItem
      // 
      this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
      this.newFileToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + N";
      this.newFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.newFileToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
      this.newFileToolStripMenuItem.Text = "New";
      this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
      // 
      // openFileToolStripMenuItem
      // 
      this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
      this.openFileToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + O";
      this.openFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.openFileToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
      this.openFileToolStripMenuItem.Text = "Open";
      this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
      // 
      // saveFileToolStripMenuItem
      // 
      this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
      this.saveFileToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + S";
      this.saveFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
      this.saveFileToolStripMenuItem.Text = "Save";
      this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + Shift + S";
      this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
      this.saveAsToolStripMenuItem.Text = "Save As";
      this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // languageToolStripMenuItem
      // 
      this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.langINIToolStripMenuItem,
            this.langScriptToolStripMenuItem,
            this.langNoneToolStripMenuItem});
      this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
      this.languageToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
      this.languageToolStripMenuItem.Text = "Language";
      // 
      // langINIToolStripMenuItem
      // 
      this.langINIToolStripMenuItem.Name = "langINIToolStripMenuItem";
      this.langINIToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
      this.langINIToolStripMenuItem.Text = "INI";
      this.langINIToolStripMenuItem.Click += new System.EventHandler(this.langINIToolStripMenuItem_Click);
      // 
      // langScriptToolStripMenuItem
      // 
      this.langScriptToolStripMenuItem.Name = "langScriptToolStripMenuItem";
      this.langScriptToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
      this.langScriptToolStripMenuItem.Text = "Script File";
      this.langScriptToolStripMenuItem.Click += new System.EventHandler(this.langScriptToolStripMenuItem_Click);
      // 
      // langNoneToolStripMenuItem
      // 
      this.langNoneToolStripMenuItem.Name = "langNoneToolStripMenuItem";
      this.langNoneToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
      this.langNoneToolStripMenuItem.Text = "None";
      this.langNoneToolStripMenuItem.Click += new System.EventHandler(this.langNoneToolStripMenuItem_Click);
      // 
      // scriptToolStripMenuItem
      // 
      this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkToolStripMenuItem,
            this.setContextDllToolStripMenuItem});
      this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
      this.scriptToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
      this.scriptToolStripMenuItem.Text = "Script";
      // 
      // checkToolStripMenuItem
      // 
      this.checkToolStripMenuItem.Name = "checkToolStripMenuItem";
      this.checkToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + T";
      this.checkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
      this.checkToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
      this.checkToolStripMenuItem.Text = "Check";
      this.checkToolStripMenuItem.Click += new System.EventHandler(this.checkToolStripMenuItem_Click);
      // 
      // setContextDllToolStripMenuItem
      // 
      this.setContextDllToolStripMenuItem.Name = "setContextDllToolStripMenuItem";
      this.setContextDllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
      this.setContextDllToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
      this.setContextDllToolStripMenuItem.Text = "Set Context Dll";
      this.setContextDllToolStripMenuItem.Click += new System.EventHandler(this.setContextDllToolStripMenuItem_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.Location = new System.Drawing.Point(0, 24);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      this.splitContainer1.Panel1MinSize = 100;
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1184, 715);
      this.splitContainer1.SplitterDistance = 100;
      this.splitContainer1.TabIndex = 3;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.tcEditor);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
      this.splitContainer2.Panel2MinSize = 250;
      this.splitContainer2.Size = new System.Drawing.Size(1184, 611);
      this.splitContainer2.SplitterDistance = 900;
      this.splitContainer2.TabIndex = 3;
      // 
      // tcEditor
      // 
      this.tcEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tcEditor.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
      this.tcEditor.Location = new System.Drawing.Point(0, 0);
      this.tcEditor.Name = "tcEditor";
      this.tcEditor.Padding = new System.Drawing.Point(10, 3);
      this.tcEditor.SelectedIndex = 0;
      this.tcEditor.Size = new System.Drawing.Size(900, 611);
      this.tcEditor.TabIndex = 0;
      this.tcEditor.SelectedIndexChanged += new System.EventHandler(this.tcEditor_SelectedIndexChanged);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tpFunctions);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(280, 573);
      this.tabControl1.TabIndex = 0;
      // 
      // tpFunctions
      // 
      this.tpFunctions.Controls.Add(this.lboxSig);
      this.tpFunctions.Controls.Add(this.label2);
      this.tpFunctions.Controls.Add(this.lboxFunctions);
      this.tpFunctions.Controls.Add(this.label1);
      this.tpFunctions.Location = new System.Drawing.Point(4, 22);
      this.tpFunctions.Name = "tpFunctions";
      this.tpFunctions.Padding = new System.Windows.Forms.Padding(3);
      this.tpFunctions.Size = new System.Drawing.Size(272, 547);
      this.tpFunctions.TabIndex = 0;
      this.tpFunctions.Text = "Functions";
      this.tpFunctions.UseVisualStyleBackColor = true;
      // 
      // lboxSig
      // 
      this.lboxSig.Dock = System.Windows.Forms.DockStyle.Top;
      this.lboxSig.ForeColor = System.Drawing.Color.MidnightBlue;
      this.lboxSig.FormattingEnabled = true;
      this.lboxSig.Location = new System.Drawing.Point(3, 371);
      this.lboxSig.Name = "lboxSig";
      this.lboxSig.Size = new System.Drawing.Size(266, 134);
      this.lboxSig.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Top;
      this.label2.Location = new System.Drawing.Point(3, 358);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(83, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Valid Signatures";
      // 
      // lboxFunctions
      // 
      this.lboxFunctions.Dock = System.Windows.Forms.DockStyle.Top;
      this.lboxFunctions.ForeColor = System.Drawing.Color.MidnightBlue;
      this.lboxFunctions.FormattingEnabled = true;
      this.lboxFunctions.Location = new System.Drawing.Point(3, 16);
      this.lboxFunctions.Name = "lboxFunctions";
      this.lboxFunctions.Size = new System.Drawing.Size(266, 342);
      this.lboxFunctions.TabIndex = 0;
      this.lboxFunctions.SelectedIndexChanged += new System.EventHandler(this.lboxFunctions_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Top;
      this.label1.Location = new System.Drawing.Point(3, 3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(92, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Context Functions";
      // 
      // mainStatusStrip
      // 
      this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rtfSelStatLabel});
      this.mainStatusStrip.Location = new System.Drawing.Point(0, 739);
      this.mainStatusStrip.Name = "mainStatusStrip";
      this.mainStatusStrip.Size = new System.Drawing.Size(1184, 22);
      this.mainStatusStrip.TabIndex = 4;
      this.mainStatusStrip.Text = "statusStrip2";
      // 
      // rtfSelStatLabel
      // 
      this.rtfSelStatLabel.Name = "rtfSelStatLabel";
      this.rtfSelStatLabel.Size = new System.Drawing.Size(16, 17);
      this.rtfSelStatLabel.Text = "   ";
      // 
      // ScriptEditForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1184, 761);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.mainStatusStrip);
      this.Controls.Add(this.mainMenuStrip);
      this.DoubleBuffered = true;
      this.MainMenuStrip = this.mainMenuStrip;
      this.Name = "ScriptEditForm";
      this.Text = "Script Editor";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptEditForm_FormClosing);
      this.Load += new System.EventHandler(this.ScriptEditForm_Load);
      this.mainMenuStrip.ResumeLayout(false);
      this.mainMenuStrip.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tpFunctions.ResumeLayout(false);
      this.tpFunctions.PerformLayout();
      this.mainStatusStrip.ResumeLayout(false);
      this.mainStatusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.MenuStrip mainMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog ofd;
    private System.Windows.Forms.SaveFileDialog sfd;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.StatusStrip mainStatusStrip;
    private System.Windows.Forms.ToolStripStatusLabel rtfSelStatLabel;
    private System.Windows.Forms.ToolStripMenuItem langINIToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem langScriptToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem langNoneToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tpFunctions;
    private System.Windows.Forms.ListBox lboxSig;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListBox lboxFunctions;
    private System.Windows.Forms.Label label1;
    private tcEditor tcEditor;
    private System.Windows.Forms.OpenFileDialog ofd_dll;
    private System.Windows.Forms.ToolStripMenuItem setContextDllToolStripMenuItem;
  }
}

