using System;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

using Duality;
using DualityEditor;

namespace EditorBase
{
	public partial class LogView : DockContent
	{
		private	DateTime	clearTime	= DateTime.MinValue;
		private	List<DataLogOutput.LogEntry>	updateBuffer	= new List<DataLogOutput.LogEntry>();

		public LogView()
		{
			this.InitializeComponent();

			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

			this.toolStrip.Renderer = new DualityEditor.Controls.ToolStrip.DualitorToolStripSystemRenderer();
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			this.DockPanel.ActiveAutoHideContentChanged += this.DockPanel_ActiveAutoHideContentChanged;

			this.UpdateView();
			Log.LogData.NewEntry += this.LogData_NewEntry;
			Sandbox.Entering += this.Sandbox_Entering;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			this.DockPanel.ActiveAutoHideContentChanged -= this.DockPanel_ActiveAutoHideContentChanged;

			Log.LogData.NewEntry -= this.LogData_NewEntry;
			Sandbox.Entering -= this.Sandbox_Entering;
		}
		
		internal void SaveUserData(System.Xml.XmlElement node)
		{
			node.SetAttribute("showMessages", this.buttonMessages.Checked.ToString(CultureInfo.InvariantCulture));
			node.SetAttribute("showWarnings", this.buttonWarnings.Checked.ToString(CultureInfo.InvariantCulture));
			node.SetAttribute("showErrors", this.buttonErrors.Checked.ToString(CultureInfo.InvariantCulture));
			node.SetAttribute("showCore", this.buttonCore.Checked.ToString(CultureInfo.InvariantCulture));
			node.SetAttribute("showEditor", this.buttonEditor.Checked.ToString(CultureInfo.InvariantCulture));
			node.SetAttribute("showGame", this.buttonGame.Checked.ToString(CultureInfo.InvariantCulture));
			node.SetAttribute("autoClear", this.checkAutoClear.Checked.ToString(CultureInfo.InvariantCulture));
		}
		internal void LoadUserData(System.Xml.XmlElement node)
		{
			bool tryParseBool;

			if (bool.TryParse(node.GetAttribute("showMessages"), out tryParseBool))
				this.buttonMessages.Checked = tryParseBool;
			if (bool.TryParse(node.GetAttribute("showWarnings"), out tryParseBool))
				this.buttonWarnings.Checked = tryParseBool;
			if (bool.TryParse(node.GetAttribute("showErrors"), out tryParseBool))
				this.buttonErrors.Checked = tryParseBool;
			if (bool.TryParse(node.GetAttribute("showCore"), out tryParseBool))
				this.buttonCore.Checked = tryParseBool;
			if (bool.TryParse(node.GetAttribute("showEditor"), out tryParseBool))
				this.buttonEditor.Checked = tryParseBool;
			if (bool.TryParse(node.GetAttribute("showGame"), out tryParseBool))
				this.buttonGame.Checked = tryParseBool;
			if (bool.TryParse(node.GetAttribute("autoClear"), out tryParseBool))
				this.checkAutoClear.Checked = tryParseBool;
		}

		public void UpdateView()
		{
			bool updateCaret = this.textLog.SelectionStart == this.textLog.Text.Length;
			int lastSelectionStart = this.textLog.SelectionStart;
			int lastSelectionLength = this.textLog.SelectionLength;

			this.textLog.Clear();
			StringBuilder builder = new StringBuilder();
			DataLogOutput.LogEntry[] acceptedData = Log.LogData.Data.Where(this.AcceptsEntry).ToArray();
			for (int i = 0; i < acceptedData.Length; i++)
			{
				DataLogOutput.LogEntry entry = acceptedData[i];
				DataLogOutput.LogEntry prevEntry = acceptedData[MathF.Max(i - 1, 0)];
				DataLogOutput.LogEntry nextEntry = acceptedData[MathF.Min(i + 1, acceptedData.Length - 1)];

				this.GenerateEntryText(entry, builder);

				if (entry.Type != prevEntry.Type || i == acceptedData.Length - 1 || entry.Type != nextEntry.Type)
				{
					int beginIndex = this.textLog.Text.Length;
					this.textLog.AppendText(builder.ToString());
					this.textLog.SelectionStart = beginIndex;
					this.textLog.SelectionLength = this.textLog.Text.Length - beginIndex;

					if (entry.Type == LogMessageType.Error)
						this.textLog.SelectionColor = Color.FromArgb(215, 64, 64);
					else if (entry.Type == LogMessageType.Warning)
						this.textLog.SelectionColor = Color.FromArgb(215, 135, 0);

					builder.Clear();
				}
			}

			this.textLog.SelectionStart = lastSelectionStart;
			this.textLog.SelectionLength = lastSelectionLength;

			if (updateCaret) this.textLog.SelectionStart = this.textLog.Text.Length;
			this.textLog.ScrollToCaret();
		}
		private void AddSingleEntry(DataLogOutput.LogEntry entry)
		{
			if (!this.AcceptsEntry(entry)) return;
			bool updateCaret = this.textLog.SelectionStart == this.textLog.Text.Length;
			int lastSelectionStart = this.textLog.SelectionStart;
			int lastSelectionLength = this.textLog.SelectionLength;

			StringBuilder builder = new StringBuilder();
			this.GenerateEntryText(entry, builder);

			int beginIndex = this.textLog.Text.Length;
			this.textLog.AppendText(builder.ToString());
			this.textLog.SelectionStart = beginIndex;
			this.textLog.SelectionLength = this.textLog.Text.Length - beginIndex;

			if (entry.Type == LogMessageType.Error)
				this.textLog.SelectionColor = Color.FromArgb(215, 64, 64);
			else if (entry.Type == LogMessageType.Warning)
				this.textLog.SelectionColor = Color.FromArgb(215, 135, 0);

			this.textLog.SelectionStart = lastSelectionStart;
			this.textLog.SelectionLength = lastSelectionLength;

			if (updateCaret) this.textLog.SelectionStart = this.textLog.Text.Length;
			this.textLog.ScrollToCaret();
		}
		private void GenerateEntryText(DataLogOutput.LogEntry entry, StringBuilder builder)
		{
			builder.Append(string.Format("{0:00}:{1:00}:{2:00} ", 
				entry.Timestamp.Hour, 
				entry.Timestamp.Minute, 
				entry.Timestamp.Second));
			builder.Append(entry.Source.Prefix);
			switch (entry.Type)
			{
				case LogMessageType.Error:
					builder.Append("ERROR:   ");
					break;
				case LogMessageType.Warning:
					builder.Append("Warning: ");
					break;
				default:
				case LogMessageType.Message:
					builder.Append("Info:    ");
					break;
			}
			builder.Append(' ', entry.Indent * 4);
			builder.AppendLine(entry.Message);
		}

		private bool AcceptsEntry(DataLogOutput.LogEntry entry)
		{
			if (entry.Timestamp < this.clearTime) return false;
			if (entry.Type == LogMessageType.Message && !this.buttonMessages.Checked) return false;
			if (entry.Type == LogMessageType.Warning && !this.buttonWarnings.Checked) return false;
			if (entry.Type == LogMessageType.Error && !this.buttonErrors.Checked) return false;
			if (entry.Source == Log.Core && !this.buttonCore.Checked) return false;
			if (entry.Source == Log.Editor && !this.buttonEditor.Checked) return false;
			if (entry.Source == Log.Game && !this.buttonGame.Checked) return false;
			return true;
		}
		
		private void updateTimer_Tick(object sender, EventArgs e)
		{
			while (this.updateBuffer.Count > 0)
			{
				this.AddSingleEntry(this.updateBuffer[0]);
				this.updateBuffer.RemoveAt(0);
			}
			this.updateTimer.Enabled = false;
		}
		private void buttonCore_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.Visible) return;
			this.UpdateView();
		}
		private void buttonEditor_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.Visible) return;
			this.UpdateView();
		}
		private void buttonGame_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.Visible) return;
			this.UpdateView();
		}
		private void buttonMessages_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.Visible) return;
			this.UpdateView();
		}
		private void buttonWarnings_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.Visible) return;
			this.UpdateView();
		}
		private void buttonErrors_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.Visible) return;
			this.UpdateView();
		}
		private void actionClear_ButtonClick(object sender, EventArgs e)
		{
			this.clearTime = DateTime.Now;
			this.textLog.Clear();
		}
		private void LogData_NewEntry(object sender, DataLogOutput.LogEntryEventArgs e)
		{
			if (this.DockState.IsAutoHide() && this.DockPanel.ActiveAutoHideContent != this) return;
			this.updateBuffer.Add(e.Entry);
			this.updateTimer.Enabled = true;
		}
		private void DockPanel_ActiveAutoHideContentChanged(object sender, EventArgs e)
		{
			if (!this.DockState.IsAutoHide() || this.DockPanel.ActiveAutoHideContent != this) return;
			this.UpdateView();
		}

		private void Sandbox_Entering(object sender, EventArgs e)
		{
			if (this.checkAutoClear.Checked) this.actionClear_ButtonClick(sender, e);
		}
	}
}
