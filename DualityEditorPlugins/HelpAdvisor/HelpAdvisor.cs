﻿using System;
using System.Drawing;
using System.Windows.Forms;

using DualityEditor;
using HelpAdvisor.PluginRes;

using WeifenLuo.WinFormsUI.Docking;

namespace HelpAdvisor
{
	public partial class HelpAdvisor : DockContent
	{
		private static	HelpInfo	advisorHelp = HelpInfo.FromText(HelpAdvisorRes.HelpInfo_Advisor_Topic, HelpAdvisorRes.HelpInfo_Advisor_Desc);

		private	HelpInfo	newHelp		= null;
		private	HelpInfo	currentHelp	= null;
		private	HelpInfo	lastHelp	= null;
		private	Timer		animTimer	= new Timer();
		private	Timer		commitTimer	= new Timer();
		private	DateTime	changeTime	= DateTime.Now;

		private float AnimProgress
		{
			get { return Math.Min(Math.Max((float)(DateTime.Now - this.changeTime).TotalMilliseconds / 100.0f, 0.0f), 1.0f); }
		}

		public HelpAdvisor()
		{
			this.InitializeComponent();
			this.animTimer.Enabled = false;
			this.animTimer.Interval = 15;
			this.animTimer.Tick += this.animTimer_Tick;
			this.commitTimer.Enabled = false;
			this.commitTimer.Interval = 1;
			this.commitTimer.Tick += this.commitTimer_Tick;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			HelpAdvisorPlugin.Instance.EditorForm.Help.ActiveHelpChanged += this.Help_ActiveHelpChanged;
			this.UpdateHelp();
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			HelpAdvisorPlugin.Instance.EditorForm.Help.ActiveHelpChanged -= this.Help_ActiveHelpChanged;
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			RectangleF topicRect = new RectangleF(this.labelTopic.Location.X, this.labelTopic.Location.Y, this.labelTopic.Width, this.labelTopic.Height);
			RectangleF descRect = new RectangleF(this.labelDescription.Location.X, this.labelDescription.Location.Y, this.labelDescription.Width, this.labelDescription.Height);

			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			StringFormat format = StringFormat.GenericDefault;
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;
			format.Trimming = StringTrimming.EllipsisCharacter;

			Color lastColorBase = this.lastHelp == advisorHelp ? SystemColors.GrayText : SystemColors.ControlText;
			Color curColorBase = this.currentHelp == advisorHelp ? SystemColors.GrayText : SystemColors.ControlText;
			Color lastColor = Color.FromArgb((int)((1.0f - this.AnimProgress) * 255.0f), lastColorBase);
			Color curColor = Color.FromArgb((int)(this.AnimProgress * 255.0f), curColorBase);

			e.Graphics.DrawString(this.lastHelp.Topic, this.labelTopic.Font, new SolidBrush(lastColor), topicRect, format);
			e.Graphics.DrawString(this.currentHelp.Topic, this.labelTopic.Font, new SolidBrush(curColor), topicRect, format);

			format.LineAlignment = StringAlignment.Near;
			format.FormatFlags = 0;

			e.Graphics.DrawString(this.lastHelp.Description, this.labelDescription.Font, new SolidBrush(lastColor), descRect, format);
			e.Graphics.DrawString(this.currentHelp.Description, this.labelDescription.Font, new SolidBrush(curColor), descRect, format);
		}
		private void animTimer_Tick(object sender, EventArgs e)
		{
			this.Invalidate();
			if (this.AnimProgress >= 1.0f) this.animTimer.Enabled = false;
		}
		private void commitTimer_Tick(object sender, EventArgs e)
		{
			this.CommitHelp();
			this.commitTimer.Enabled = false;
		}

		public void UpdateHelp()
		{
			this.newHelp = HelpAdvisorPlugin.Instance.EditorForm.Help.ActiveHelp ?? advisorHelp;
			this.commitTimer.Stop();
			this.commitTimer.Start();

			if (this.currentHelp == null) this.currentHelp = this.newHelp;
			if (this.lastHelp == null) this.lastHelp = this.currentHelp;
		}
		private void CommitHelp()
		{
			if (newHelp.Topic != this.currentHelp.Topic || newHelp.Description != this.currentHelp.Description)
			{
				this.lastHelp = this.currentHelp;
				this.currentHelp = newHelp;
				this.changeTime = DateTime.Now;
				this.animTimer.Enabled = true;
			}
		}

		private void Help_ActiveHelpChanged(object sender, HelpStackChangedEventArgs e)
		{
			this.UpdateHelp();
		}
	}
}
