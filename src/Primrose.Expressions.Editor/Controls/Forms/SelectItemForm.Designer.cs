namespace Primrose.Expressions.Editor.Controls.Forms
{
  partial class SelectItemForm
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
      this.cbItem = new System.Windows.Forms.ComboBox();
      this.bAdd = new System.Windows.Forms.Button();
      this.bCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // cbItem
      // 
      this.cbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbItem.Location = new System.Drawing.Point(12, 12);
      this.cbItem.Name = "cbItem";
      this.cbItem.Size = new System.Drawing.Size(410, 21);
      this.cbItem.TabIndex = 0;
      this.cbItem.SelectedIndexChanged += new System.EventHandler(this.cbItem_SelectedIndexChanged);
      // 
      // bAdd
      // 
      this.bAdd.Enabled = false;
      this.bAdd.Location = new System.Drawing.Point(280, 39);
      this.bAdd.Name = "bAdd";
      this.bAdd.Size = new System.Drawing.Size(68, 29);
      this.bAdd.TabIndex = 1;
      this.bAdd.Text = "Add";
      this.bAdd.UseVisualStyleBackColor = true;
      this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
      // 
      // bCancel
      // 
      this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.bCancel.Location = new System.Drawing.Point(354, 39);
      this.bCancel.Name = "bCancel";
      this.bCancel.Size = new System.Drawing.Size(68, 29);
      this.bCancel.TabIndex = 2;
      this.bCancel.Text = "Cancel";
      this.bCancel.UseVisualStyleBackColor = true;
      this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
      // 
      // SelectItemForm
      // 
      this.AcceptButton = this.bAdd;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.bCancel;
      this.ClientSize = new System.Drawing.Size(434, 73);
      this.Controls.Add(this.bCancel);
      this.Controls.Add(this.bAdd);
      this.Controls.Add(this.cbItem);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "SelectItemForm";
      this.Text = "Select Item";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cbItem;
    private System.Windows.Forms.Button bAdd;
    private System.Windows.Forms.Button bCancel;
  }
}