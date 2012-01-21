using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using CancelEventHandler = System.ComponentModel.CancelEventHandler;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;

using WeifenLuo.WinFormsUI.Docking;
using Aga.Controls.Tree;

using Duality;
using Duality.Resources;
using DualityEditor;
using DualityEditor.Controls;

namespace EditorBase
{
	public partial class LogView : DockContent
	{
		public LogView()
		{
			this.InitializeComponent();
		}
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.UpdateView();
			Log.LogData.NewEntry += this.LogData_NewEntry;
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			Log.LogData.NewEntry -= this.LogData_NewEntry;
		}
		
		internal void SaveUserData(System.Xml.XmlElement node)
		{
			node.SetAttribute("showMessages", this.buttonMessages.Checked.ToString());
			node.SetAttribute("showWarnings", this.buttonWarnings.Checked.ToString());
			node.SetAttribute("showErrors", this.buttonErrors.Checked.ToString());
			node.SetAttribute("showCore", this.buttonCore.Checked.ToString());
			node.SetAttribute("showEditor", this.buttonEditor.Checked.ToString());
			node.SetAttribute("showGame", this.buttonGame.Checked.ToString());
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
		}

		public void UpdateView()
		{
			bool updateCaret = this.textLog.SelectionStart == this.textLog.Text.Length;
			int lastSelectionStart = this.textLog.SelectionStart;
			int lastSelectionLength = this.textLog.SelectionLength;

			this.textLog.Clear();
			StringBuilder builder = new StringBuilder();
			LogMessageType lastType = LogMessageType.Message;
			foreach (DataLogOutput.LogEntry entry in Log.LogData.Data)
			{
				if (!this.AcceptsEntry(entry)) continue;
				if (entry.Type != lastType)
				{
					this.textLog.AppendText(builder.ToString());
					builder.Clear();
				}

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

				if (entry.Type != lastType)
				{
					int beginIndex = this.textLog.Text.Length;
					this.textLog.AppendText(builder.ToString());
					this.textLog.SelectionStart = beginIndex;
					this.textLog.SelectionLength = this.textLog.Text.Length - beginIndex;

					if (entry.Type == LogMessageType.Error)
					{
						this.textLog.SelectionColor = Color.FromArgb(215, 64, 64);
					}
					else if (entry.Type == LogMessageType.Warning)
					{
						this.textLog.SelectionColor = Color.FromArgb(215, 135, 0);
					}

					builder.Clear();
					lastType = entry.Type;
				}
			}
			this.textLog.AppendText(builder.ToString());
			builder.Clear();

			this.textLog.SelectionStart = lastSelectionStart;
			this.textLog.SelectionLength = lastSelectionLength;

			if (updateCaret) this.textLog.SelectionStart = this.textLog.Text.Length;
			this.textLog.ScrollToCaret();
		}

		private bool AcceptsEntry(DataLogOutput.LogEntry entry)
		{
			if (entry.Type == LogMessageType.Message && !this.buttonMessages.Checked) return false;
			if (entry.Type == LogMessageType.Warning && !this.buttonWarnings.Checked) return false;
			if (entry.Type == LogMessageType.Error && !this.buttonErrors.Checked) return false;
			if (entry.Source == Log.Core && !this.buttonCore.Checked) return false;
			if (entry.Source == Log.Editor && !this.buttonEditor.Checked) return false;
			if (entry.Source == Log.Game && !this.buttonGame.Checked) return false;
			return true;
		}

		private void buttonCore_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateView();
		}
		private void buttonEditor_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateView();
		}
		private void buttonGame_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateView();
		}
		private void buttonMessages_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateView();
		}
		private void buttonWarnings_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateView();
		}
		private void buttonErrors_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateView();
		}
		private void LogData_NewEntry(object sender, DataLogOutput.LogEntryEventArgs e)
		{
			this.UpdateView();
		}
	}
}
