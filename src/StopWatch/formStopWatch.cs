// Decompiled with JetBrains decompiler
// Type: StopWatch.formStopWatch
// Assembly: StopWatch, Version=2.0.4520.41037, Culture=neutral, PublicKeyToken=null
// MVID: 50397C4C-3761-4290-83B1-F140B4BC3638
// Assembly location: C:\Users\Jason Dong\Documents\timer\StopWatch.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Web.UI;
using System.Windows.Forms;


namespace StopWatch
{
	public class formStopWatch : Form
	{
		static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
		private Button buttonStart;
		private Button buttonStop;
		private Button buttonReset;
		private Label labelTime;
		private IContainer components;
		private long m_iStartTicks;
		private long m_iStopTicks;
		private Thread m_worker;
		private bool m_bShutdown;
		private bool m_bWorkerRunning;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem stayOnTopToolStripMenuItem;
		private ToolStripMenuItem copyToClipboardToolStripMenuItem;
		private ToolStripMenuItem renameWindowToolStripMenuItem;
		private long m_iElapsedTicks;
		private Page p;

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys.C | Keys.Control))
				Clipboard.SetDataObject((object)this.labelTime.Text);
			return base.ProcessCmdKey(ref msg, keyData);
		}

		public formStopWatch(bool bStartRunning, bool bTopMost)
		{
			this.InitializeComponent();
			if (bStartRunning)
				this.startTiming();
			this.TopMost = bTopMost;
			if (!bTopMost)
				return;
			this.stayOnTopToolStripMenuItem.Checked = true;
		}

		protected override void Dispose(bool disposing)
		{
			this.stopWorker();
			if (disposing && this.components != null)
				this.components.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = (IContainer)new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(formStopWatch));
			this.buttonStart = new Button();
			this.buttonStop = new Button();
			this.buttonReset = new Button();
			this.labelTime = new Label();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.stayOnTopToolStripMenuItem = new ToolStripMenuItem();
			this.copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
			this.renameWindowToolStripMenuItem = new ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			this.buttonStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.buttonStart.Location = new Point(8, 60);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new Size(48, 24);
			this.buttonStart.TabIndex = 0;
			this.buttonStart.Text = "Start";
			this.buttonStart.Click += new EventHandler(this.buttonStart_Click);
			this.buttonStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.buttonStop.Enabled = false;
			this.buttonStop.Location = new Point(64, 60);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new Size(48, 24);
			this.buttonStop.TabIndex = 1;
			this.buttonStop.Text = "Stop";
			this.buttonStop.Click += new EventHandler(this.buttonStop_Click);
			this.buttonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.buttonReset.Location = new Point(120, 60);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new Size(48, 24);
			this.buttonReset.TabIndex = 2;
			this.buttonReset.Text = "Reset";
			this.buttonReset.Click += new EventHandler(this.buttonReset_Click);
			this.labelTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.labelTime.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
			this.labelTime.Location = new Point(16, 16);
			this.labelTime.Name = "labelTime";
			this.labelTime.Size = new Size(144, 24);
			this.labelTime.TabIndex = 3;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[3]
			{
		(ToolStripItem) this.stayOnTopToolStripMenuItem,
		(ToolStripItem) this.copyToClipboardToolStripMenuItem,
		(ToolStripItem) this.renameWindowToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(170, 92);
			this.stayOnTopToolStripMenuItem.CheckOnClick = true;
			this.stayOnTopToolStripMenuItem.Name = "stayOnTopToolStripMenuItem";
			this.stayOnTopToolStripMenuItem.Size = new Size(169, 22);
			this.stayOnTopToolStripMenuItem.Text = "Stay on top";
			this.stayOnTopToolStripMenuItem.CheckStateChanged += new EventHandler(this.checkStayOnTop);
			this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
			this.copyToClipboardToolStripMenuItem.Size = new Size(169, 22);
			this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
			this.copyToClipboardToolStripMenuItem.Click += new EventHandler(this.copyToClipboard);
			this.renameWindowToolStripMenuItem.Name = "renameWindowToolStripMenuItem";
			this.renameWindowToolStripMenuItem.Size = new Size(169, 22);
			this.renameWindowToolStripMenuItem.Text = "Rename window";
			this.renameWindowToolStripMenuItem.Click += new EventHandler(this.renameWindow);
			this.AutoScaleBaseSize = new Size(5, 13);
			this.ClientSize = new Size(176, 94);
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.Controls.Add((System.Windows.Forms.Control)this.labelTime);
			this.Controls.Add((System.Windows.Forms.Control)this.buttonReset);
			this.Controls.Add((System.Windows.Forms.Control)this.buttonStop);
			this.Controls.Add((System.Windows.Forms.Control)this.buttonStart);
			this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			this.MaximizeBox = false;
			this.Name = nameof(formStopWatch);
			this.Text = "Stop Watch";
			this.TransparencyKey = Color.Red;
			this.Load += new EventHandler(this.formStopWatch_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		[STAThread]
		private static void Main(string[] args)
		{
			System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
			bool bStartRunning = false;
			bool bTopMost = false;
			for (int index = 0; index < args.Length; ++index)
			{
				if (args[index].ToLower().IndexOf("start") != -1)
					bStartRunning = true;
				else if (args[index].ToLower().IndexOf("top") != -1)
					bTopMost = true;
			}
			Application.Run((Form)new formStopWatch(bStartRunning, bTopMost));
		}

		private void formStopWatch_Load(object sender, EventArgs e) => this.startWorker();

		private void buttonStart_Click(object sender, EventArgs e) => this.startTiming();


		private void startTiming()
		{
			if (this.m_iStartTicks != 0L)
				return;
			this.m_iStartTicks = DateTime.Now.Ticks;
			if (m_iStartTicks != 0L)
			{
				myTimer.Tick += (o, ea) =>
			{
				PopupForm popup = new PopupForm();
				DialogResult dialogresult = popup.ShowDialog();
				if (dialogresult == DialogResult.OK)
				{
					if (this.m_iStartTicks <= 0L)
						return;
					myTimer.Stop();
					this.m_iElapsedTicks += DateTime.Now.Ticks - this.m_iStartTicks;
					this.m_iStartTicks = 0L;
					this.m_iStopTicks = 0L;
					this.buttonStop.Enabled = false;
					this.buttonStart.Enabled = true;
				}				
				popup.Dispose();
				//myTimer.Stop();
			};
			}
			myTimer.Interval = 5000; //1hour
			myTimer.Start();

			this.buttonStop.Enabled = true;
			this.buttonStart.Enabled = false;
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			if (this.m_iStartTicks <= 0L)
				return;
			myTimer.Stop();
			this.m_iElapsedTicks += DateTime.Now.Ticks - this.m_iStartTicks;
			this.m_iStartTicks = 0L;
			this.m_iStopTicks = 0L;
			this.buttonStop.Enabled = false;
			this.buttonStart.Enabled = true;
		}

		private void buttonReset_Click(object sender, EventArgs e)
		{
			if (this.m_iStartTicks > 0L)
			{
				this.m_iStartTicks = DateTime.Now.Ticks;
				this.buttonStop.Enabled = true;
				this.buttonStart.Enabled = false;
			}
			else
				this.m_iStartTicks = 0L;
			myTimer.Stop();
			this.m_iStopTicks = 0L;
			this.m_iElapsedTicks = 0L;
		}

		private void startWorker()
		{
			if (this.m_bWorkerRunning)
				return;
			this.m_bShutdown = false;
			this.m_worker = new Thread(new ThreadStart(this.workerGo));
			this.m_worker.Name = "StopWatchThread";
			this.m_worker.Priority = ThreadPriority.BelowNormal;
			this.m_worker.Start();
			this.m_bWorkerRunning = true;
		}

		private void stopWorker()
		{
			if (!this.m_bWorkerRunning || this.m_worker == null)
				return;
			this.m_bShutdown = true;
			int num;
			for (num = 0; (this.m_worker.ThreadState == System.Threading.ThreadState.Running || this.m_worker.ThreadState == System.Threading.ThreadState.WaitSleepJoin) && num < 1000; num += 50)
				Thread.Sleep(50);
			if (num >= 1000)
				this.m_worker.Abort();
			this.m_bWorkerRunning = false;
		}

		private void workerGo()
		{
			while (!this.m_bShutdown)
			{
				Thread.Sleep(20);
				long num1 = 0;
				if (this.m_iStartTicks > 0L)
					num1 = this.m_iStopTicks != 0L ? this.m_iStopTicks - this.m_iStartTicks : DateTime.Now.Ticks - this.m_iStartTicks;
				double num2 = ((double)this.m_iElapsedTicks + (double)num1) / 10000000.0;
				num2.ToString();
				int hours = TimeSpan.FromSeconds(num2).Hours;
				int minutes = TimeSpan.FromSeconds(num2).Minutes;
				int seconds = TimeSpan.FromSeconds(num2).Seconds;
				int num3 = TimeSpan.FromSeconds(num2).Milliseconds / 10;
				this.labelTime.Text = hours.ToString() + ":" + minutes.ToString("0#") + ":" + seconds.ToString("0#") + "." + num3.ToString("0#");
			}
		}

		private void checkStayOnTop(object sender, EventArgs e)
		{
			if (this.stayOnTopToolStripMenuItem.Checked)
				this.TopMost = true;
			else
				this.TopMost = false;
		}

		private void copyToClipboard(object sender, EventArgs e) => Clipboard.SetDataObject((object)this.labelTime.Text);

		private void renameWindow(object sender, EventArgs e)
		{
			this.TopMost = false;
			RenameForm renameForm = new RenameForm();
			renameForm.Owner = (Form)this;
			renameForm.StartPosition = FormStartPosition.CenterParent;
			renameForm.TopMost = this.stayOnTopToolStripMenuItem.Checked;
			int num = (int)renameForm.ShowDialog();
			if (renameForm.getOkay())
				this.Text = renameForm.getText();
			this.TopMost = this.stayOnTopToolStripMenuItem.Checked;
		}
	}
}
