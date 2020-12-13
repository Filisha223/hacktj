using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace StopWatch
{
	public partial class PopupForm : Form
	{
		public PopupForm()
		{
			InitializeComponent();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			Random rnd = new Random();
			int one = rnd.Next(0, 255);
			int two = rnd.Next(0, 255);
			int three = rnd.Next(0, 255);
			int four = rnd.Next(0, 255);

			label1.ForeColor = Color.FromArgb(one, two, three, four);
			button1.ForeColor = Color.FromArgb(four,three,two,one);
			panel1.BackColor = Color.FromArgb(three, one, four, two);
		}

		private void PopupForm_Load(object sender, EventArgs e)
		{
			timer1.Start();
			timer1.Enabled = true;

			SoundPlayer s = new SoundPlayer(@"C:\Users\Jason Dong\Documents\timer\StopWatch\alarm.wav");
			s.Play();
		}
	}
}
