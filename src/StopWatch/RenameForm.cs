// Decompiled with JetBrains decompiler
// Type: StopWatch.RenameForm
// Assembly: StopWatch, Version=2.0.4520.41037, Culture=neutral, PublicKeyToken=null
// MVID: 50397C4C-3761-4290-83B1-F140B4BC3638
// Assembly location: C:\Users\Jason Dong\Documents\timer\StopWatch.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StopWatch
{
  public class RenameForm : Form
  {
    private bool m_bOkay;
    private string m_strName = "";
    private IContainer components;
    private TextBox textName;
    private Button buttonOK;
    private Button buttonCancel;

    public RenameForm() => this.InitializeComponent();

    private void RenameForm_Load(object sender, EventArgs e)
    {
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.m_bOkay = true;
      this.m_strName = this.textName.Text;
      this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.m_bOkay = false;
      this.Close();
    }

    public bool getOkay() => this.m_bOkay;

    public string getText() => this.m_strName;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.textName = new TextBox();
      this.buttonOK = new Button();
      this.buttonCancel = new Button();
      this.SuspendLayout();
      this.textName.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.textName.Location = new Point(8, 9);
      this.textName.Name = "textName";
      this.textName.Size = new Size(232, 22);
      this.textName.TabIndex = 0;
      this.buttonOK.Location = new Point(64, 34);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(85, 25);
      this.buttonOK.TabIndex = 1;
      this.buttonOK.Text = "OK";
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.buttonCancel.Location = new Point(155, 34);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(85, 25);
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.AcceptButton = (IButtonControl) this.buttonOK;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(248, 62);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.textName);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = nameof (RenameForm);
      this.Text = "Rename window";
      this.Load += new EventHandler(this.RenameForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
