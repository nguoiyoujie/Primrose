using System;
using System.Windows.Forms;

namespace Primrose.Expressions.Editor.Controls.Forms
{
  public partial class SelectItemForm : Form
  {
    public SelectItemForm(string[] items)
    {
      InitializeComponent();
      cbItem.Items.AddRange(items);
    }

    public string Item;

    private void bAdd_Click(object sender, EventArgs e)
    {
      Item = cbItem.Text;
      DialogResult = DialogResult.OK;
    }

    private void bCancel_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }

    private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
    {
      bAdd.Enabled = cbItem.Text.Length > 0;
    }
  }
}
